namespace TokensOwl.Infrastructure.Models
{
    public class CashedTransaction
    {
        public CashedTransaction(string address, string hash, string blockNumber, string timeStamp, string nonce, string blockHash,
            string fromAddress, string contractAddress, string toAddress, string value, string tokenName, string tokenSymbol,
            string tokenDecimal, string transactionIndex, string gas, string gasPrice, string gasUsed,
            string cumulativeGasUsed, string input, string confirmations)
        {
            this.Address = address;
            this.Hash = hash;
            this.BlockNumber = blockNumber;
            this.TimeStamp = timeStamp;
            this.Nonce = nonce;
            this.BlockHash = blockHash;
            this.FromAddress = fromAddress;
            this.ContractAddress = contractAddress;
            this.ToAddress = toAddress;
            this.Value = value;
            this.TokenName = tokenName;
            this.TokenSymbol = tokenSymbol;
            this.TokenDecimal = tokenDecimal;
            this.TransactionIndex = transactionIndex;
            this.Gas = gas;
            this.GasPrice = gasPrice;
            this.GasUsed = gasUsed;
            this.CumulativeGasUsed = cumulativeGasUsed;
            this.Input = input;
            this.Confirmations = confirmations;
        }

        public string Address { get; private set; }
        public string Hash { get; private set; }
        public string BlockNumber { get; private set; }
        public string TimeStamp { get; private set; }
        public string Nonce { get; private set; }
        public string BlockHash { get; private set; }
        public string FromAddress { get; private set; }
        public string ContractAddress { get; private set; }
        public string ToAddress { get; private set; }
        public string Value { get; private set; }
        public string TokenName { get; private set; }
        public string TokenSymbol { get; private set; }
        public string TokenDecimal { get; private set; }
        public string TransactionIndex { get; private set; }
        public string Gas { get; private set; }
        public string GasPrice { get; private set; }
        public string GasUsed { get; private set; }
        public string CumulativeGasUsed { get; private set; }
        public string Input { get; private set; }
        public string Confirmations { get; private set; }
    }
}