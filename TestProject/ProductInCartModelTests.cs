using NUnit.Framework;
using SnackBar.Core.Models;
using SnackBar.Infrastructure.Data.Entities;

namespace TestProject
{
    public class ProductInCartModelTests
    {
        [Test]
        public void ProductInCartModel_ToString_ReturnsExpectedString()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product 1", Price = 10.0M };
            var orderedAmount = 5;
            var productInCartModel = new ProductInCartModel
            {
                product = product,
                orderedAmount = orderedAmount
            };

            // Expected string
            var expectedString = product.ToString() + " Ordered amount: " + orderedAmount;

            // Act
            var result = productInCartModel.ToString();

            // Assert
            Assert.AreEqual(expectedString, result);
        }
    }
}
