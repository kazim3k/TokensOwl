using System.Collections.Generic;

namespace TokensOwl.Clients.Etherscan.Model
{
    public class EtherscanTransactionResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<Result> Result { get; set; }
    }
}