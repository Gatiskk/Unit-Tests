using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScooterRental.Interfaces;

namespace ScooterRental.Tests
{
    [TestClass]
    public class RentalCompanyTests
    {
        private IScooterService _scooterService;
        private IRentalCompany _company;
        private List<Scooter> _inventory;
        private List<RentedScooter> _rentedScooter;
        private IRentalFee _rentalFee;

        [TestInitialize]

        public void Setup()
        {
            _inventory = new List<Scooter>();
            _rentedScooter = new List<RentedScooter>();
            _scooterService = new ScooterService(_inventory);
            _rentalFee = new RentalFee();
            _company = new RentalCompany("BOLT", _scooterService, _rentedScooter, _rentalFee);
            _scooterService.AddScooter("1", 0.2m);
            _scooterService.AddScooter("2", 0.2m);
            _scooterService.AddScooter("3", 0.2m);
            _scooterService.AddScooter("4", 0.2m);
        }

        [TestMethod]
        public void StartRent_StartRentScooter_ScooterIsRented()
        {
          
            _company.StartRent("2");

           
            _scooterService.GetScooterById("2").IsRented.Should().BeTrue();
        }

        [TestMethod]
        public void EndRent_EndRentingOfScooter_ScooterIsNotRented()
        {
            
            var scooter = _scooterService.GetScooterById("2");
            var rentedScooter = new RentedScooter("2", DateTime.UtcNow,  0.2m);

            
            scooter.IsRented = true;
            _rentedScooter.Add(rentedScooter);
            _company.EndRent("2");

            
            scooter.IsRented.Should().BeFalse();
            rentedScooter.EndDateTime.HasValue.Should().BeTrue();
        }

        [TestMethod]
        public void EndRent_RentScooterForShortTime()
        {
            
            var scooter = _scooterService.GetScooterById("2");

            
            _rentedScooter.Add(new RentedScooter("2", DateTime.UtcNow.AddMinutes(-60), 0.2m));

            
            _company.EndRent("2").Should().Be(12.00m);
            scooter.IsRented = true;
        }

        [TestMethod]
        public void EndRent_RentScooterForLongTime()
        {
            
            var scooter = _scooterService.GetScooterById("2");

            
            _rentedScooter.Add(new RentedScooter("2", DateTime.UtcNow.AddDays(-5).AddMinutes(-2880), 0.2m));
            scooter.IsRented = true;

            
            _company.EndRent("2").Should().Be(160.00m);
        }

        [TestMethod]
        public void EndRent_RentScooterFor10Minutes()
        {
            
            var scooter = _scooterService.GetScooterById("2");

            
            _rentedScooter.Add(new RentedScooter("2", DateTime.UtcNow.AddMinutes(-10), 0.2m));
            scooter.IsRented = true;

            
            _company.EndRent("2").Should().Be(2.00m);
        }

        [TestMethod]
        public void CalculateIncome_PerYear()
        {
            
            var scooter = _scooterService.GetScooterById("2");

            
            _rentedScooter.Add(new RentedScooter("1", DateTime.UtcNow, 0.2m));
            _rentedScooter[0].StartDateTime = DateTime.UtcNow.AddMinutes(-10);
            scooter.IsRented = true;

            
            _company.EndRent("1").Should().Be(2.00m);
            _company.CalculateIncome(2022, false).Should().Be(2.00m);
        }

        [TestMethod]
        public void CalculateIncome_YearIsNull()
        {
            
            _rentedScooter.Add(new RentedScooter("1", DateTime.UtcNow, 0.2m));
            _rentedScooter.Add(new RentedScooter("2", DateTime.UtcNow, 0.2m));
            var scooter21 = _rentedScooter[0];
            var scooter22 = _rentedScooter[1];

            
            scooter21.StartDateTime = new DateTime(2021, 9,12,0,0,0);
            scooter21.EndDateTime = new DateTime(2021, 9, 12, 0, 10, 0);
            scooter22.StartDateTime = DateTime.UtcNow.AddMinutes(-10);
            scooter22.EndDateTime = DateTime.UtcNow;

            
            _company.CalculateIncome(null, false).Should().Be(4.00m);
        }

        [TestMethod]
        public void CalculateIncome_YearIsNullAndScooterIsBusy()
        {
            
            _rentedScooter.Add(new RentedScooter("1", DateTime.UtcNow, 0.2m));
            _rentedScooter.Add(new RentedScooter("2", DateTime.UtcNow, 0.2m));
            var scooter21 = _rentedScooter[0];
            var scooter22 = _rentedScooter[1];

            
            scooter21.StartDateTime = new DateTime(2021, 9, 12, 0, 0, 0);
            scooter21.EndDateTime = new DateTime(2021, 9, 12, 0, 10, 0);
            scooter22.StartDateTime = DateTime.UtcNow.AddMinutes(-10);
            scooter22.EndDateTime = null;

            
            _company.CalculateIncome(null, true).Should().Be(4.00m);
        }

        [TestMethod]
        public void CalculateIncome_HasYearAndIsTrue()
        {
            
            _rentedScooter.Add(new RentedScooter("1", DateTime.UtcNow, 0.2m));
            _rentedScooter.Add(new RentedScooter("2", DateTime.UtcNow, 0.2m));
            var scooter21 = _rentedScooter[0];
            var scooter22 = _rentedScooter[1];

            
            scooter21.StartDateTime = new DateTime(2021, 9, 12, 0, 0, 0);
            scooter21.EndDateTime = new DateTime(2021, 9, 12, 0, 10, 0);
            scooter22.StartDateTime = DateTime.UtcNow.AddMinutes(-10);
            scooter22.EndDateTime = null;

            
            _company.CalculateIncome(2021, true).Should().Be(4.00m);
        }

        [TestMethod]
        public void CalculateIncome_HasYearAndIsFalse()
        {
            
            _rentedScooter.Add(new RentedScooter("1", DateTime.UtcNow, 0.2m));
            _rentedScooter.Add(new RentedScooter("2", DateTime.UtcNow, 0.2m));
            var scooter21 = _rentedScooter[0];
            var scooter22 = _rentedScooter[1];

            
            scooter21.StartDateTime = new DateTime(2021, 9, 12, 0, 0, 0);
            scooter21.EndDateTime = new DateTime(2021, 9, 12, 0, 10, 0);
            scooter22.StartDateTime = DateTime.UtcNow.AddMinutes(-10);
            scooter22.EndDateTime = null;

            
            _company.CalculateIncome(2021, false).Should().Be(2.00m);
        }

        [TestMethod]
        public void CalculateIncome_HasYear2022AndIsTrue()
        {
            
            _rentedScooter.Add(new RentedScooter("1", DateTime.UtcNow, 0.2m));
            _rentedScooter.Add(new RentedScooter("2", DateTime.UtcNow, 0.2m));
            var scooter21 = _rentedScooter[0];
            var scooter22 = _rentedScooter[1];

            
            scooter21.StartDateTime = new DateTime(2021, 9, 12, 0, 0, 0);
            scooter21.EndDateTime = new DateTime(2021, 9, 12, 0, 10, 0);
            scooter22.StartDateTime = DateTime.UtcNow.AddMinutes(-10);
            scooter22.EndDateTime = null;

            
            _company.CalculateIncome(2022, true).Should().Be(2.00m);
        }
    }
}