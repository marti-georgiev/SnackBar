using NUnit.Framework;
using SnackBar.Core.Models;
using SnackBar.Infrastructure.Data.Entities;
using System.Collections.Generic;

namespace TestProject
{
    public class CartModelTests
    {
        [Test]
        public void CartModel_CanBeCreated()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 10.0M }, // Add 'M' suffix
                new Product { Id = 2, Name = "Product 2", Price = 15.0M } // Add 'M' suffix
            };

            var key = "cart_key";

            // Act
            var cartModel = new CartModel
            {
                Products = products,
                Key = key
            };

            // Assert
            Assert.IsNotNull(cartModel);
            Assert.That(cartModel.Products, Is.EqualTo(products));
            Assert.AreEqual(key, cartModel.Key);
        }
    }
}
