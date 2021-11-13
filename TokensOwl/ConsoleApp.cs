using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TokensOwl
{
    public class ConsoleApp
    {
        private readonly ILogger<ConsoleApp> logger;

        private readonly ITransactionHandler transactionHandler;

        public ConsoleApp(ILogger<ConsoleApp> logger, ITransactionHandler transactionHandler)
        {
            this.logger = logger;
            this.transactionHandler = transactionHandler;
        }

        public void Execute()
        {
            this.logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            // TODO GET LIST OD ADDRESSES FROM DB
            var addressesWithNames = new Dictionary<string, string>
            {
                ["0xA4e65Af4878291324FaBa8456E5945E234682740"] = "Czeko",
                ["0x5303f549b819f87b413D6B3f3705184102695Bf8"] = "JPBanks",
                ["0xde8531C4FDf2cE3014527bAF57F8f788E240746e"] = "Lukso whale 218k\\%24 \\(18,20\\)",
                ["0x5Aed79C6d7c6cBa7CE00626100649728649E23C7"] = "Lukso whale 1m\\%24 \\(23,06\\)",
            };
            foreach (var (address, name) in addressesWithNames)
            {
                this.transactionHandler.Handle(name, address);
                Task.Delay((int) TimeSpan.FromSeconds(5).TotalMilliseconds);
            }
        }
    }

  
}