using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TokensOwl.Clients.Etherscan;
using TokensOwl.Clients.Telegram;
using TokensOwl.Infrastructure;

namespace TokensOwl
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var consoleApp = CreateHostBuilder(args).Build().Services.GetRequiredService<ConsoleApp>();

            consoleApp?.Execute();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddSingleton<ConsoleApp>();
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