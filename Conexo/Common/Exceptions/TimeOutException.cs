using System;
namespace Common.Exceptions
{
    public class TimeOutException : Exception
    {
        public TimeOutException() : base(@"Tiempo de espera agotado, verifique su conexión e intente nuevamente")
        {
        }
        public TimeOutException(string message) : base(@"Tiempo de espera agotado, vuelve a intentarlo nuevamente")
        {
        }

        public TimeOutException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}
