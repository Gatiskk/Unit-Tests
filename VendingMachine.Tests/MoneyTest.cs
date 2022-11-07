using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using VendingMachine.Exceptions;
namespace VendingMachine.Tests
{
    [TestClass]
    public class MoneyTest
    {
        private Money _money;

        [TestInitialize]
        public void SetUp()
        {
            _money = new Money(1, 20);
        }

        [TestMethod]
        public void Money_IsSameAmount()
        {
           
            _money.Euros.Should().Be(1);
            _money.Cents.Should().Be(20);
        }

        [TestMethod]
        public void ToString_MoneyMethodIsWorking()
        {
            
            var money = new Money(1, 20);
            money.ToString().Should().Be("1.20");
        }

        [TestMethod]
        public void GetValue_MethodIsWorking()
        {
            
            var money = new Money(1, 20);

            
            money.GetValue().Should().Be(1.20);
        }

        [TestMethod]
        public void Money_NotAcceptNegativeValues_ThrowsNotAcceptNegativeValuesException()
        {
            Action act = () => new Money(-1, 20);
            act.Should().Throw<MoneyCantHaveNegativeValuesException>()
                .WithMessage("Vending machine don't accept negative value");

            Action act1 = () => new Money(1, -20);
            act1.Should().Throw<MoneyCantHaveNegativeValuesException>()
                .WithMessage("Vending machine don't accept negative value");

            Action act2 = () => new Money(-1, -20);
            act2.Should().Throw<MoneyCantHaveNegativeValuesException>()
                .WithMessage("Vending machine don't accept negative value");
        }

        [TestMethod]
        public void MoneyCentsHaveToBeLimitException()
        {
            Action act = () => new Money(1, 190);
            act.Should().Throw<MoneyCentsHaveToBeLimitException>()
                .WithMessage("Cents must be from 0 - 99");
        }
    }
}