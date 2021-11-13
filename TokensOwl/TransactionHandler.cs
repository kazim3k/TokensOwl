using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using TokensOwl.Clients.Etherscan;
using TokensOwl.Clients.Telegram;
using TokensOwl.Clients.Telegram.Model;
using TokensOwl.Exceptions;
using TokensOwl.Infrastructure;
using TokensOwl.Infrastructure.Models;

namespace TokensOwl
{
    public class TransactionHandler : ITransactionHandler
    {
        private readonly IEtherscanClient etherscanClient;

        private readonly ICashedTransactionRepository cashedTransactionRepository;

        private readonly ITelegramClient telegramClient;

        private readonly ILogger<TransactionHandler> logger;

        public TransactionHandler(IEtherscanClient etherscanClient,
            ICashedTransactionRepository cashedTransactionRepository, ITelegramClient telegramClient,
            ILogger<TransactionHandler> logger)
        {
            this.etherscanClient = etherscanClient;
            this.cashedTransactionRepository = cashedTransactionRepository;
            this.telegramClient = telegramClient;
            this.logger = logger;
        }

        public void Handle(string name, string address)
        {
            try
            {
                var etherscanTransactionResponse = this.etherscanClient.GetTransaction(address);

                if (etherscanTransactionResponse.Result?.First() == null)
                {
                    throw new TransactionNotFoundException($"No transaction results found for address: {address}");
                }

                var cashedTransaction =
                    this.cashedTransactionRepository.Get(address, etherscanTransactionResponse.Result.First().Hash);

                if (cashedTransaction != null)
                {
                    return;
                }

                var etherscanTransaction = etherscanTransactionResponse.Result.First();

                var transactionToCash = new CashedTransaction(
                    name,
                    address,
                    etherscanTransaction.Hash,
                    etherscanTransaction.BlockNumber,
                    etherscanTransaction.TimeStamp,
                    etherscanTransaction.Nonce,
                    etherscanTransaction.BlockHash,
                    etherscanTransaction.From,
                    etherscanTransaction.ContractAddress,
                    etherscanTransaction.To,
                    etherscanTransaction.Value,
                    etherscanTransaction.TokenName,
                    etherscanTransaction.TokenSymbol,
                    etherscanTransaction.TokenDecimal,
                    etherscanTransaction.TransactionIndex,
                    etherscanTransaction.Gas,
                    etherscanTransaction.GasPrice,
                    etherscanTransaction.GasUsed,
                    etherscanTransaction.CumulativeGasUsed,
                    etherscanTransaction.Input,
                    etherscanTransaction.Confirmations);


                this.cashedTransactionRepository.Save(transactionToCash);

                var telegramTransaction = new TelegramTransaction(transactionToCash.Hash, name,
                    transactionToCash.Address, transactionToCash.FromAddress, transactionToCash.Value,
                    transactionToCash.TokenSymbol, transactionToCash.TokenDecimal, transactionToCash.TimeStamp, transactionToCash.ContractAddress);
                this.telegramClient.SendNotification(telegramTransaction);
                this.logger.LogInformation(
                    $"Successfully pushed notification, address: {transactionToCash.Address}, Name: {telegramTransaction.Name}");
            }
            catch (Exception e)
            {
                this.logger.LogError($"Something went wrong during while handling address: {address}, Message: {e.Message}");
            }
        }
    }
}