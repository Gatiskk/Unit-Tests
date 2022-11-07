using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using VendingMachine.Exceptions;
using VendingMachine.Interfaces;

namespace VendingMachine.Tests
{
    [TestClass]
    public class VendingMachineTest
    {
        private Money _money;
        private IVendingMachine _vendingMachine;
        private Product _product;

        [TestInitialize]
        public void SetUp()
        {
            _money = new Money(1, 00);
            _vendingMachine = new VendingMachine("Frozen Treats", 30,
                new List<string> { "0.10", "0.20", "0.50", "1.00", "2.00" });
            _vendingMachine.AddProduct("Polo", new Money(1, 20), 4);
        }

        [TestMethod]
        public void CreatingValidVendingMachine()

        {
            
            _vendingMachine.Manufacturer.Should().Be("Frozen Treats");
        }

        [TestMethod]
        public void VendingMachineNameCanNotBeNullOrEmpty()
        {
            Action act = () =>
                _vendingMachine =
                    new VendingMachine(null, 30, new List<string> { "0.10", "0.20", "0.50", "1.00", "2.00" });
            act.Should().Throw<NameIsNullException>()
                .WithMessage("name can not be null");
        }

        [TestMethod]
        public void VendingMachineCapacityCanNotBeNegative()
        {
            Action act = () =>
                _vendingMachine =
                    new VendingMachine("Frozen Treats", 0, new List<string> { "0.10", "0.20", "0.50", "1.00", "2.00" });
            act.Should().Throw<NegativeOrZeroAmountException>()
                .WithMessage("Amount can not be zero or negative");
        }

        [TestMethod]
        public void InsertCoin_ValidCoins_ReturnsInsertedAmount()
        {
            
            _vendingMachine.InsertCoin(new Money(0, 20));
            _vendingMachine.InsertCoin(new Money(0, 50));
            _vendingMachine.InsertCoin(new Money(2, 00));

            _vendingMachine.Amount.Should().Be(new Money(2,70));
        }

        [TestMethod]
        public void InsertCoin_InvalidCoinException()
        {
            var money = new Money(0, 70);
            Action act = () =>
                _vendingMachine.InsertCoin(money);
            act.Should().Throw<InvalidCoinException>()
                .WithMessage("Cant insert this coin");
        }

        [TestMethod]
        public void ReturnMoney_IsChangeToReturnReturnsChange()
        {
            
            _vendingMachine.InsertCoin(_money);

            _vendingMachine.ReturnMoney().Should().Be(new Money(0, 0));
        }

        [TestMethod]
        public void AddProduct_ValidProductAddsProduct()
        {
            
            var product = new Product("BenJerry", _money, 6);

            _vendingMachine.AddProduct("BenJerry", _money, 6).Should().BeTrue();
            _vendingMachine.Products.Should().Contain(product);
        }

        [TestMethod]
        public void UpdateProduct_ValidProductUpdatesProduct()
        {
            
            _vendingMachine.AddProduct("ICE", _money, 8);

            _vendingMachine.UpdateProduct(1, "Polo", _money, 3).Should().BeTrue();
            var product = _vendingMachine.Products[0];
            product.Name.Should().Be("Polo");
            product.Price.Should().Be(_money);
            product.Available.Should().Be(3);
        }

        [TestMethod]
        public void AddProduct_NotEnoughEmptySpotsThrowsNotEnoughEmptySpotsException()
        {
            _vendingMachine.AddProduct("ICE", _money, 25);

            _vendingMachine.Invoking(machine => machine.AddProduct("ICEBERRY", _money, 7))
                .Should().Throw<NotEnoughEmptySlotsException>()
                .WithMessage("Machine is already full!");
        }

        [TestMethod]
        public void UpdateProduct_ProductNotFound_ThrowsProductNotFoundException()
        {
            _vendingMachine.AddProduct("Ice2", _money, 8);
            _vendingMachine.AddProduct("Ice3", _money, 4);

            _vendingMachine.Invoking(machine => machine.UpdateProduct(1, "Ice2", _money, 9))
                .Should().Throw<ProductWithThisNameAlreadyExistsException>()
                .WithMessage($"Product already exists");
        }
    }
}