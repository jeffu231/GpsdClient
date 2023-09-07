using System;

namespace GpsdClient.Exceptions
{
    public class InvalidDataTypeException : Exception
    {
        public InvalidDataTypeException() : base("You provided an invalid datatype for this type of client!")
        {
            
        }
    }
}
