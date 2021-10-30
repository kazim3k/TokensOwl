using TokensOwl.Clients.Telegram.Model;

namespace TokensOwl.Clients.Telegram
{
    public interface ITelegramClient
    {
        void SendNotification(TelegramTransaction telegramTransactionModel);
    }
}