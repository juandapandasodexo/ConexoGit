using System;
namespace Common.Exceptions
{
    public class AppException:Exception
    {
        public AppException()
        {
        }

        public AppException(string message) : base(message)
        {
        }
    }
}
