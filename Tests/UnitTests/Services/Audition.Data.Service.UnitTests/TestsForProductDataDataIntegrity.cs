using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Audition.Data.Service.UnitTests
{
    [TestClass]
    public class TestsForProductDataDataIntegrity
    {
        private ProductData _productData;
        private readonly Mock<IDatabaseReader> _mockDatabaseReader = new Mock<IDatabaseReader>();

        [TestInitialize]
        public void TestInit()
        {
            _productData = new ProductData(_mockDatabaseReader.Object);
        }

        [TestMethod]
        public void ProductsDoNotContainDuplicates()
        {
            var allProducts = _productData.GetAllProducts();
            bool hasDuplicates = allProducts.GroupBy(product => product).Any(g => g.Count() > 1);

            Assert.IsFalse(hasDuplicates);
        }

        [TestMethod]
        public void ShouldBeAbleToGetProductWithStyleId1()
        {
            var product = _productData.GetProductByStyleId(1);
            Assert.IsNotNull(product);
            Assert.AreEqual("A", product.Brand);
            Assert.AreEqual("$1.00", product.FormattedRegularPrice);
            Assert.AreEqual("ia", product.ImageUrl);
            Assert.AreEqual("na", product.Name);
        }

        [TestMethod]
        public void ShouldBeAbleToGetProductWithStyleId2()
        {
            var product = _productData.GetProductByStyleId(2);
            Assert.IsNotNull(product);
            Assert.AreEqual("B", product.Brand);
            Assert.AreEqual("$2.00", product.FormattedRegularPrice);
            Assert.AreEqual("ib", product.ImageUrl);
            Assert.AreEqual("nb", product.Name);
        }

        [TestMethod]
        public void ShouldBeAbleToGetProductWithStyleId3()
        {
            var product = _productData.GetProductByStyleId(3);
            Assert.IsNotNull(product);
            Assert.AreEqual("C", product.Brand);
            Assert.AreEqual("$3.00", product.FormattedRegularPrice);
            Assert.AreEqual("ic", product.ImageUrl);
            Assert.AreEqual("nc", product.Name);
        }

        [TestMethod]
        public void ThereIsNoProductWithStyleId4()
        {
            var product = _productData.GetProductByStyleId(4);
            Assert.IsNull(product);
        }
    }
}
