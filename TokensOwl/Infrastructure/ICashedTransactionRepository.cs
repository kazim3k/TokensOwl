using TokensOwl.Infrastructure.Models;

namespace TokensOwl.Infrastructure
{
    public interface ICashedTransactionRepository
    {
        CashedTransaction Get(string address, string hash);

        void Save(CashedTransaction cashedTransaction);
    }
}