using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Hierarchy.Exceptions;

namespace Hierarchy.Tests
{
    [TestClass]
    public class ZebraTest
    {
        private Zebra _zebra;
        private StringWriter _stringWriter;

        [TestInitialize]
        public void Setup()
        {
            _zebra = new Zebra("ZEBRA", "Mountain Zebra", 55, "Africa");
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);
        }

        [TestMethod]
        public void ZebraMakeSound()
        {
            
            string expectedSound = "*zebra sound*";

            
            _zebra.MakeSound();

            
            var expectedString = _stringWriter.ToString().Trim();
            expectedSound.Should().Be(expectedString);
        }

        [TestMethod]
        public void ZebraWeightShouldNotBeNegativeNumberException()
        {
            Action act = () => new Zebra("ZEBRA", "Mountain Zebra", -55, "Africa"); ;
            act.Should().Throw<NegativeWeightException>().
                WithMessage($"Can not input negative or zero weight -55");
        }

        [TestMethod]
        public void ZebraWeightShouldNotBeNullException()
        {
            Action act = () => new Zebra("ZEBRA", "Mountain Zebra", 0, "Africa"); ;
            act.Should().Throw<NegativeWeightException>().
                WithMessage($"Can not input negative or zero weight 0");
        }

        [TestMethod]
        public void ZebraFoodShouldNotBeNegativeException()
        {
            Action act = () => _zebra.Eat(new Vegetable(-2));
            act.Should().Throw<NegativeFoodException>().
                WithMessage($"Can not input negative food");
        }

        [TestMethod]
        public void ZebraNotFoodEaten()
        {
            
            _zebra.Eat(new Meat(5));

            
            _zebra.ToString().Should().Be("Mountain Zebra [ZEBRA, 55, Africa, 0]");
        }

        [TestMethod]
        public void ZebraFoodEaten()
        {
            
            _zebra.Eat(new Vegetable(5));

            
            _zebra.ToString().Should().Be("Mountain Zebra [ZEBRA, 55, Africa, 5]");
        }
    }
}