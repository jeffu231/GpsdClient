using System;

namespace GpsdClient.Exceptions
{
    public class ConnectionLostException : Exception
    {
        public ConnectionLostException() : base("The connection is lost.")
        {

        }
    }
}
