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
    public class ScooterServiceTests
    {
        private IScooterService _scooterService;
        private List<Scooter> _inventory;

        [TestInitialize]
        public void Setup()
        {
            _inventory = new List<Scooter>();
            _scooterService = new ScooterService(_inventory);
        }

        [TestMethod]
        public void AddScooter_AddValidScooter_ScooterValid()
        {
            
            _scooterService.AddScooter("1", 0.2m);
            _inventory.Count.Should().Be(1);
        }

        [TestMethod]
        public void AddScooter_AddScooterTwice_ThrowsDuplicateScooterException()
        {
            _scooterService.AddScooter("1", 0.2m);

            Action act = () => _scooterService.AddScooter("1", 0.2m);
            act.Should().Throw<DuplicateScooterException>().
                WithMessage($"Scooter with id 1 already exists");
        }

        [TestMethod]
        public void AddScooter_AddScooterWithPriceZeroOrLess_ThrowsInvalidPriceException()
        {
            _scooterService.AddScooter("1", 0.2m);

            Action act = () => _scooterService.AddScooter("1", -0.2m);
            act.Should().Throw<InvalidPriceException>().
                WithMessage($"Given price -0.2 is not valid");
        }

        [TestMethod]
        public void AddScooter_AddScooterNullOrEmptyID_ThrowsInvalidIDException()
        {
            _scooterService.AddScooter("1", 0.2m);

            Action act = () => _scooterService.AddScooter("", 0.2m);
            act.Should().Throw<InvalidIDException>().
                WithMessage($"ID can not be null or empty");
        }

        [TestMethod]
        public void RemoveScooter_ScooterExists_ScooterIsRemoved()
        {
           
            _inventory.Add(new Scooter("1", 0.2m));
            _scooterService.RemoveScooter("1");

            
            _inventory.Any(Scooter => Scooter.Id =="1").Should().BeFalse();
            _inventory.Count.Should().Be(0);
        }

        [TestMethod]
        public void RemoveScooter_ScooterDoesNotExists_ThrowsScooterDoesNotExistsException()
        {
            Action act = () => _scooterService.RemoveScooter("1");
            act.Should().Throw<ScooterDoesNotExists>().
                WithMessage(("Scooter with this id 1 does not exists"));
        }

        [TestMethod]
        public void RemoveScooter_NullOrEmptyIDGiven_ThrowsInvalidIDException()
        {
            Action act = () => _scooterService.RemoveScooter("");
            act.Should().Throw<InvalidIDException>().
                WithMessage($"ID can not be null or empty");
        }

        [TestMethod]
        public void GetScooters_ListNullOrEmptyException_ThrowEmptyListException()
        {
            Action act = () => _scooterService.GetScooters();
            act.Should().Throw<EmptyListException>().
                WithMessage($"The list can not be null or empty");
        }

        [TestMethod]
        public void GetScooter_ReturnListOfScooters()
        {
            
            _inventory.Add(new Scooter("1", 0.2m));
            _inventory.Add(new Scooter("2", 0.2m));
            _inventory.Add(new Scooter("3", 0.2m));

            
            var scooterList = _scooterService.GetScooters();

            
            scooterList.Should().HaveCount(3);
        }

        [TestMethod]
        public void GetScooterById_ReturnWithSameId()
        {   
            
            _inventory.Add(new Scooter("1", 0.2m));
            _inventory.Add(new Scooter("2", 0.2m));
            _inventory.Add(new Scooter("3", 0.2m));

           
            _scooterService.GetScooterById("2").Should().NotBeNull().
                And.BeEquivalentTo(new Scooter("2", 0.2m));
        }

        [TestMethod]
        public void GetScootersById_ListNullOrEmptyException_ThrowEmptyListException()
        {
            Action act = () => _scooterService.GetScooterById("");
            act.Should().Throw<InvalidIDException>().
                WithMessage($"ID can not be null or empty");
        }

        [TestMethod]
        public void GetScootersById_ThrowsScooterDoesNotExistsException()
        {
            
            _inventory.Add(new Scooter("2", 0.2m));
            _inventory.Add(new Scooter("3", 0.2m));

            Action act = () => _scooterService.GetScooterById("1");
            act.Should().Throw<ScooterDoesNotExists>().
                WithMessage(("Scooter with this id 1 does not exists"));
        }
    }
}