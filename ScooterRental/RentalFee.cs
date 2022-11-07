using System;
using ScooterRental.Interfaces;

namespace ScooterRental
{
    public class RentalFee : IRentalFee
    {
        public decimal RentalDecimal(RentedScooter rentalScooter)
        {
            const int maxAmountPerDay = 20;
            DateTime startTime = rentalScooter.StartDateTime;
            var endTime = rentalScooter.EndDateTime ?? DateTime.UtcNow;
            var totalDays = (endTime.Date - rentalScooter.StartDateTime.Date).TotalDays;

            if (totalDays == 0)
            {
                var dayPrice = Math.Round(((decimal)(endTime - startTime).TotalMinutes * rentalScooter.PricePerMinute),2);
                return dayPrice > 20 ? 20 : dayPrice;
            }
            var firstDayCalc = (decimal)(1440 - rentalScooter.StartDateTime.TimeOfDay.TotalMinutes) * rentalScooter.PricePerMinute;
            var lastDayCalc = (decimal)endTime.TimeOfDay.TotalMinutes * rentalScooter.PricePerMinute;
            var daysBetween = Math.Min(1440 * rentalScooter.PricePerMinute, maxAmountPerDay) * (decimal)(totalDays - 1);
            var betweenCalc = Math.Min(firstDayCalc, maxAmountPerDay) + daysBetween + Math.Min(lastDayCalc, maxAmountPerDay);
            return betweenCalc;
        }
    }
}