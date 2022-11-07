using System;
namespace ScooterRental.Exceptions
{
    public class DuplicateScooterException : Exception
    {
        public DuplicateScooterException(string id) : 
            base($"Scooter with id {id} already exists") {}
    }
}