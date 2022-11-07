using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using VendingMachine.Exceptions;
namespace VendingMachine.Tests
{
    [TestClass]
    public class ProductTest
    {
        private Product _product;

        [TestInitialize]
        public void SetUp()
        {
            new Money(1, 20);
            _product = new Product("Polo", new Money(0,50),4);
        }

        [TestMethod]
        public void Product_GetProductNamePriceAndAmount()
        {
            
            _product.Name.Should().Be("Polo");
            _product.Price.Should().Be(new Money(0, 50));
            _product.Available.Should().Be(4);
        }

        [TestMethod]
        public void Product_ProductNameIsNullException()
        {
            Action act = () => new Product(null, new Money(0, 50), 4);
            act.Should().Throw<NameIsNullException>()
                .WithMessage($"name can not be null");
        }

        [TestMethod]
        public void ToString_ReturnsObjectInformationInSetFormat()
        {
            
            var product = new Product("Polo", new Money(0, 50), 4);

            
            product.ToString().Should().Be("Name: Polo  | Price: 0.50  |     Amount available: 4");
        }

        [TestMethod]
        public void CreateProduct_AmountIsNegativeOrZero_ShouldThrowInvalidAmountException()
        {
            Action act = () => new Product("Polo", new Money(0, 50), 0);
            act.Should().Throw<NegativeOrZeroAmountException>()
                .WithMessage($"Amount can not be zero or negative");
        }
    }
}