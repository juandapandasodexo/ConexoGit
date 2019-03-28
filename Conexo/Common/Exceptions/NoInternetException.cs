using System;
namespace Common.Exceptions
{
    public class NoInternetException : Exception
    {
        public NoInternetException()
        {
        }

        public NoInternetException(string message) : base(message)
        {
        }

        public NoInternetException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
