using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TokensOwl.Clients.Etherscan;
using TokensOwl.Clients.Telegram;
using TokensOwl.Infrastructure;
using TokensOwl.Worker;

namespace TokensOwl
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker.Worker>();
                    var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING") ?? "";

                    services.AddDbContext<TransactionsDbContext>(
                        options => options.UseNpgsql(connectionString)
                            .UseSnakeCaseNamingConvention());
                    services.AddSingleton<ICashedTransactionRepository, CashedTransactionRepository>();
                    services.AddSingleton<ITransactionHandler, TransactionHandler>();
                    services.AddHttpClient<IEtherscanClient, EtherscanClient>(
                        client => { client.Timeout = TimeSpan.FromSeconds(30); });
                    services.AddHttpClient<ITelegramClient, TelegramClient>(
                        client => { client.Timeout = TimeSpan.FromSeconds(30); });
                });
    }
}