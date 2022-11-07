using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScooterRental.Exceptions;
using ScooterRental.Interfaces;

namespace ScooterRental.Tests
{
    [TestClass]
    public class RentalFeeTests
    {
        private IRentalFee _rentalFee;
        private RentedScooter _rentedScooter;
        private DateTime _time;

        [TestInitialize]
        public void SetUp()
        {
            _rentalFee = new RentalFee();
            _time = new DateTime(2021, 12, 31, 23, 30, 0);
            _rentedScooter = new RentedScooter("1", _time, 0.2m);
        }


        [TestMethod]
        public void RentalFeeTestSameDay()
        {
            
            var scooter = new RentedScooter("2", DateTime.UtcNow, 0.2m);
            scooter.StartDateTime = new DateTime(2021, 9, 12, 23, 30, 0);
            scooter.EndDateTime = new DateTime(2021, 9, 12, 23, 40, 0);

            
            _rentalFee.RentalDecimal(scooter).Should().Be(2.00m);
        }

        [TestMethod]
        public void RentalFeeTestAfterMidnight()
        {
            
            var scooter = new RentedScooter("2", DateTime.UtcNow, 1m);
            scooter.StartDateTime = new DateTime(2021, 9, 12, 23, 59, 0);
            scooter.EndDateTime = new DateTime(2021, 9, 13, 00, 01, 0);

           
            _rentalFee.RentalDecimal(scooter).Should().Be(2.00m);
        }

        [TestMethod]
        public void RentalFeeTestAfterOtherDay()
        {
            
            var scooter = new RentedScooter("2", DateTime.UtcNow, 1m);
            scooter.StartDateTime = new DateTime(2021, 9, 12, 23, 59, 0);
            scooter.EndDateTime = new DateTime(2021, 9, 13, 01, 01, 0);

            
            _rentalFee.RentalDecimal(scooter).Should().Be(21.00m);
        }

        [TestMethod]
        public void RentalFeeTestAfterOneCompleteYear()
        {
            
            var scooter = new RentedScooter("2", DateTime.UtcNow, 1m);
            scooter.StartDateTime = new DateTime(2021, 9, 12, 23, 59, 0);
            scooter.EndDateTime = new DateTime(2022, 9, 12, 23, 59, 0);
            
           
            _rentalFee.RentalDecimal(scooter).Should().Be(7301.00m);
        }
    }
}