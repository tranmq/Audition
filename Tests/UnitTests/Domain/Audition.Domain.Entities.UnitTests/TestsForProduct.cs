﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Audition.Domain.Entities.UnitTests
{
    [TestClass]
    public class TestsForProduct
    {
        [TestMethod]
        public void CopyConstructorWithNullObjectBehavesAsDefaultConstructor()
        {
            var product = new Product(null);
            Assert.AreEqual(default(string), product.Brand);
            Assert.AreEqual(default(string), product.FormattedRegularPrice);
            Assert.AreEqual(default(string), product.ImageUrl);
            Assert.AreEqual(default(string), product.Name);
            Assert.AreEqual(default(int), product.StyleId);
        }

        [TestMethod]
        public void CopyConstructorCreatesNewProductWithSameValues()
        {
            var product = new Product { StyleId = 1, Name = "name", Brand = "brand", FormattedRegularPrice = "$1.00", ImageUrl = "somewhere" };
            var newProduct = new Product(product);
            Assert.AreEqual(product.Brand, newProduct.Brand);
            Assert.AreEqual(product.FormattedRegularPrice, newProduct.FormattedRegularPrice);
            Assert.AreEqual(product.ImageUrl, newProduct.ImageUrl);
            Assert.AreEqual(product.Name, newProduct.Name);
            Assert.AreEqual(product.StyleId, newProduct.StyleId);
        }

        [TestMethod]
        public void ProductsWithSameStyleIdAreEqual()
        {
            var product1 = new Product { StyleId = 1, Name = "prod1", Brand = "prod1", FormattedRegularPrice = "$1.00", ImageUrl = "somewhere" };
            var product2 = new Product { StyleId = 1, Name = "prodI", Brand = "prodI", FormattedRegularPrice = "$1.00", ImageUrl = "somewhere" };

            Assert.AreEqual(product1, product2);
        }

        [TestMethod]
        public void ProductsWithDifferentStyleIdAreNotEqual()
        {
            var product1 = new Product { StyleId = 1, Name = "prod1", Brand = "prod1", FormattedRegularPrice = "$1.00", ImageUrl = "somewhere" };
            var product2 = new Product { StyleId = 2, Name = "prod2", Brand = "prod2", FormattedRegularPrice = "$1.00", ImageUrl = "somewhere" };

            Assert.AreNotEqual(product1, product2);
        }

        [TestMethod]
        public void ToStringShouldReturnPropertiesOfTheProduct()
        {
            var product = new Product { StyleId = 1, Name = "name", Brand = "brand", FormattedRegularPrice = "$1.00", ImageUrl = "somewhere" };
            const string expected = "StyleId: 1, Brand: brand, Name: name, Price: $1.00, ImageUrl: somewhere";
            
            Assert.AreEqual(expected, product.ToString());
        }
    }
}
