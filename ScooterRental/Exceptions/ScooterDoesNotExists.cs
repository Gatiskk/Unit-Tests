using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterRental.Exceptions
{
    public class ScooterDoesNotExists : Exception
    {
        public ScooterDoesNotExists(string id) : 
            base($"Scooter with this id {id} does not exists") {}
    }
}
