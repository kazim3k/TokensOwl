namespace TokensOwl
{
    public interface ITransactionHandler
    {
        void Handle(string name, string address);
    }
}