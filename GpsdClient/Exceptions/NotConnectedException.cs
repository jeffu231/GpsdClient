using System;

namespace GpsdClient.Exceptions
{
    public class NotConnectedException : Exception
    {
        public NotConnectedException() : base("The connection is not open. Plz connect first!")
        {

        }
    }
}
