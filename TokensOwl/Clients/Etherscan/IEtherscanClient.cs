using TokensOwl.Clients.Etherscan.Model;

namespace TokensOwl.Clients.Etherscan
{
    public interface IEtherscanClient
    {
        EtherscanTransactionResponse GetTransaction(string address);
    }
}