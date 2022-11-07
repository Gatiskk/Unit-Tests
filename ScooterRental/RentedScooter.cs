using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScooterRental.Exceptions;

namespace ScooterRental
{
    public class RentedScooter
    {
        public string Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public decimal PricePerMinute { get; set; }

        public RentedScooter(string id, DateTime startDateTime, decimal pricePerMinute)
        {
            Id = id;
            StartDateTime = startDateTime;
            PricePerMinute = pricePerMinute;
            EndDateTime = null;
        }
    }
}
