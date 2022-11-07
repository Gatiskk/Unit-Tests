using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScooterRental.Exceptions;
using ScooterRental.Interfaces;

namespace ScooterRental
{
    public class RentalCompany : IRentalCompany
    {
        public IScooterService ScooterService { get; }
        public string Name { get; }
        private readonly List<RentedScooter> _rentedScooterList;
        private readonly IRentalFee _rentalFee;

        public RentalCompany(string name, IScooterService scooterService, List<RentedScooter> rentedScooterList, IRentalFee rentalFee)
        {
            Name = name;
            ScooterService = scooterService;
            _rentedScooterList = rentedScooterList;
            _rentalFee = rentalFee;
        }

        public void StartRent(string id)
        {
            var scooter = ScooterService.GetScooterById(id);
            scooter.IsRented = true;
            _rentedScooterList.Add(new RentedScooter(scooter.Id, DateTime.UtcNow, scooter.PricePerMinute));
        }

        public decimal EndRent(string id)
        {
            var scooter = ScooterService.GetScooterById(id);
            var rentedScooter = _rentedScooterList.FirstOrDefault(s => s.Id == id && !s.EndDateTime.HasValue);
            scooter.IsRented = false;
            if (rentedScooter == null)
            {
                throw new ScooterDoesNotExists(rentedScooter.Id);
            }

            rentedScooter.EndDateTime = DateTime.UtcNow;
            return _rentalFee.RentalDecimal(rentedScooter);
        }

        public decimal CalculateIncome(int? reportYear, bool includeNotCompletedRentals)
        {
            decimal totalYearIncome = 0;
            var rentedScooterList = new List<RentedScooter>();

            if (!reportYear.HasValue && includeNotCompletedRentals == false)
            {
                rentedScooterList = _rentedScooterList.
                    Where(s => s.EndDateTime.HasValue).ToList();
            }

            if (reportYear.HasValue && includeNotCompletedRentals == false)
            {
                rentedScooterList = _rentedScooterList.
                    Where(s => s.EndDateTime.HasValue && s.EndDateTime.Value.Year == reportYear).ToList();
            }

            if (reportYear == null && includeNotCompletedRentals)
            {
                rentedScooterList = _rentedScooterList.ToList();
            }

            if (reportYear.HasValue && includeNotCompletedRentals)
            {
                rentedScooterList = _rentedScooterList.
                    Where(s => s.EndDateTime ==null || s.EndDateTime.Value.Year == (int)reportYear).ToList();
            }

            foreach (var scooter in rentedScooterList)
            {
                totalYearIncome += _rentalFee.RentalDecimal(scooter);
            }
            return totalYearIncome;
        }
    }
}