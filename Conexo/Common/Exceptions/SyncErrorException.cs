using System;

namespace Common.Exceptions
{
    public class SyncErrorException : Exception
    {

        public SyncErrorException()
        {
        }

        public SyncErrorException(string message) : base(message)
        {
        }

    }
}
