using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using TokensOwl.Clients.Telegram.Model;
using TokensOwl.Exceptions;

namespace TokensOwl.Clients.Telegram
{
    public class TelegramClient : ITelegramClient
    {
        private readonly string apiKey = Environment.GetEnvironmentVariable("TELEGRAM_API_KEY");
        private readonly string getUsdApiKey = Environment.GetEnvironmentVariable("GET_USD_RATE_API_KEY");
        private const string ChatIdMichal = "1733373186";
        private const string ChatIdBartek = "332725732";

        private readonly HttpClient httpClient;

        public TelegramClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        // TODO: move creation of message to builder, there should be only a call made
        public void SendNotification(TelegramTransaction telegramTransactionModel)
        {
            var getUsdValueRequest = new HttpRequestMessage(HttpMethod.Get,
                $"https://rest.coinapi.io/v1/exchangerate/{telegramTransactionModel.TokenSymbol.ToUpper()}/USD");
            getUsdValueRequest.Headers.Add("X-CoinAPI-Key", this.getUsdApiKey);
            
            var getUsdRateResponse = (GetUsdRateResponse) this.httpClient.Send(getUsdValueRequest).Content
                .ReadFromJsonAsync(typeof(GetUsdRateResponse)).Result;
            var usdValue =
                Math.Round(getUsdRateResponse?.Rate != null
                    ? (decimal)(getUsdRateResponse.Rate * telegramTransactionModel.NumberValue)
                    : 0M, 2);
            var usdValueString = usdValue == 0
                ? $"Couldn't find {telegramTransactionModel.TokenSymbol} rating, on coinapi\\.io"
                : $"\\%24{usdValue.ToString(CultureInfo.InvariantCulture).Replace(".", ",")}";
            var text =
                $"*Wallet notification*%0A%0A_Data_: {telegramTransactionModel.Date}%0A_Użytkownik_: *{telegramTransactionModel.Name}*%0A{telegramTransactionModel.ValuePositivityToken}{telegramTransactionModel.Value} {telegramTransactionModel.TokenSymbol} \\({usdValueString}\\)%0A[view on Etherscan](https://etherscan.io/tx/{telegramTransactionModel.Hash}/)";
            var requestMichal = new HttpRequestMessage(HttpMethod.Get,
                $"https://api.telegram.org/bot{this.apiKey}/sendMessage?disable_notification=false&disable_web_page_preview=true&chat_id={ChatIdMichal}&parse_mode=MarkdownV2&text={text}");

            var requestBartek = new HttpRequestMessage(HttpMethod.Get,
                $"https://api.telegram.org/bot{this.apiKey}/sendMessage?disable_web_page_preview=true&chat_id={ChatIdBartek}&parse_mode=MarkdownV2&text={text}");

            var responseMichal = this.httpClient.Send(requestMichal);
            if (!responseMichal.IsSuccessStatusCode)
            {
                throw new NotificationNotSentException(
                    $"Couldn't send notification to Michal telegram for transaction: {telegramTransactionModel.Hash} response: {responseMichal.Content.ReadAsStreamAsync().Result}");
            }
            
            var responseBartek = this.httpClient.Send(requestBartek);

            if (!responseBartek.IsSuccessStatusCode)
            {
                throw new NotificationNotSentException(
                    $"Couldn't send notification to Bartek telegram for transaction: {telegramTransactionModel.Hash} response: {responseBartek.Content.ReadAsStreamAsync().Result}");
            }
        }
    }
}