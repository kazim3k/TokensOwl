using System;
using System.Globalization;

namespace TokensOwl.Clients.Telegram.Model
{
    public class TelegramTransaction
    {
        // TODO create mapper
        public TelegramTransaction(string hash, string name, string address, string fromAddress, string value, string tokenSymbol, string tokenDecimal, string timestamp, string contractAddress)
        {
            this.Hash = hash;
            this.Name = name;
            this.Value = (double.Parse(value) / Math.Pow(10, int.Parse(tokenDecimal))).ToString(CultureInfo.InvariantCulture).Replace(".",",");
            this.NumberValue = double.Parse(value) / Math.Pow(10, int.Parse(tokenDecimal));
            this.TokenSymbol = tokenSymbol;
            this.ValuePositivityToken = string.Equals(address, fromAddress, StringComparison.CurrentCultureIgnoreCase) ? "\\-" : "\\%2B";
            this.Date = DateTimeOffset.FromUnixTimeSeconds(long.Parse(timestamp)).DateTime;
            this.ContractAddress = contractAddress;
        }
        
        public string Hash { get; private set; }

        public string Name { get; private set; }

        public string Value { get; set; }

        public double NumberValue { get; set; }

        public string TokenSymbol { get; set; }
        
        public string ValuePositivityToken { get; set; }

        public DateTime Date { get; set; }

        public string ContractAddress { get; set; }
    }
}