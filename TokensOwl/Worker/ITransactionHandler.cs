namespace TokensOwl.Worker
{
    public interface ITransactionHandler
    {
        void Handle(string name, string address);
    }
}