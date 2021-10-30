using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using TokensOwl.Infrastructure.Models;

namespace TokensOwl.Infrastructure
{
    public class CashedTransactionRepository : ICashedTransactionRepository
    {
        private readonly IServiceScopeFactory scopeFactory;


        public CashedTransactionRepository(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        public CashedTransaction Get(string address, string hash)
        {
            try
            {
                using var scope = this.scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<TransactionsDbContext>();
                return dbContext.CashedTransactions.SingleOrDefault(t => t.Address == address && t.Hash == hash);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Save(CashedTransaction cashedTransaction)
        {
            try
            {
                using var scope = this.scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<TransactionsDbContext>();

                dbContext.CashedTransactions.Add(cashedTransaction);
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}