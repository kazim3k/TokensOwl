using System;
using System.Net.Http;
using System.Net.Http.Json;
using TokensOwl.Clients.Etherscan.Model;
using TokensOwl.Exceptions;

namespace TokensOwl.Clients.Etherscan
{
    public class EtherscanClient : IEtherscanClient
    {
        private readonly string apiKey = Environment.GetEnvironmentVariable("ETHER_SCAN_API_KEY");
        private readonly HttpClient httpClient;

        public EtherscanClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public EtherscanTransactionResponse GetTransaction(string address)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"https://api.etherscan.io/api?module=account&action=tokentx&address={address}&page=1&offset=1&startblock=0&endblock=999999999&sort=desc&apikey={this.apiKey}");
            var response = this.httpClient.Send(request);

            if (response == null || !response.IsSuccessStatusCode)
            {
                throw new TransactionNotFoundException(
                    $"Failed getting last transaction for address: {address}, response code: {response?.StatusCode}");
            }

            return (EtherscanTransactionResponse) response.Content
                .ReadFromJsonAsync(typeof(EtherscanTransactionResponse)).Result;
        }
    }
}