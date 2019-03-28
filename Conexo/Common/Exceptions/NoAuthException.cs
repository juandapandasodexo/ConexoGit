using System;
namespace Common.Exceptions
{
    public class NoAuthException : Exception
    {
        public NoAuthException()
        {
        }

        public NoAuthException(string message) : base(message)
        {
        }

        public NoAuthException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}
