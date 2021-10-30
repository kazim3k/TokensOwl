using System;
using System.Runtime.Serialization;

namespace TokensOwl.Exceptions
{
    [Serializable]
    public class NotificationNotSentException : Exception
    {
        public NotificationNotSentException(string message)
            : base(message)
        {
        }

        protected NotificationNotSentException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}