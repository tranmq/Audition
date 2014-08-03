using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Audition.Domain.Entities.UnitTests
{
    [TestClass]
    public class TestsForProduct
    {
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
