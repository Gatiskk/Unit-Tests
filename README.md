# Tests

## You should create tests for these exercises:
### Classes and Objects
- Exercise 1
- Exercise 5
- Exercise 10
- Exercise 11
- Exercise 12
- Exercise 13

### Polymorphism
- Exercise 1
- Exercise 6
- Exercise 7
### Scooter Rental
 - (Scooter rental)[https://github.com/codelex-io/home-assignments/tree/main/assignments/if/if-scooters]
 
When creating tests, each test method should be in the form UsedMethod_WhatIsGiven_WhatIsExpected. Test example:
```
	    [TestMethod]
        public void IsNumber15_InputIsNot15_ReturnsFalse()
        {
            // Arrange
            int a = 1;
            int b = 1;

            // Act
            var result = _test.IsNumber15(a, b);

            // Assert
            Assert.AreEqual(false, result);
        }
```

If you want to create similar tests with different given parameters. you can use 
```
[DataTestMethod]
[DataRow(12,3,4)]
[DataRow(12,2,6)]
[DataRow(12,4,3)]
public void DivideTest_PossibleToDivide_ExpectedOutputIsReturned(int n, int d, int q)
{
	// Act
	var result = _calculation.Divide(a,b)

	// Assert
	Assert.AreEqual(result, q);
}
```
