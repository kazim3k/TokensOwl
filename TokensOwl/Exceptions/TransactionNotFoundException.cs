using System;
using System.Runtime.Serialization;

namespace TokensOwl.Exceptions
{
    [Serializable]
    public class TransactionNotFoundException : Exception
    {
        public TransactionNotFoundException(string message)
            : base(message)
        {
        }

        protected TransactionNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}