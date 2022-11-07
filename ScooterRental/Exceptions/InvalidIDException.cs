using System;

namespace ScooterRental.Exceptions
{
    public class InvalidIDException : Exception
    {
        public InvalidIDException(string id) :
            base($"ID can not be null or empty") {}
    }
}
