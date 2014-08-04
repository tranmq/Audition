using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;

namespace Audition.Data.Service.UnitTests
{
    [TestClass]
    public class TestsForProductData
    {
        private Mock<IDatabaseReader> _mockDatabaseReader;
        private ProductData _productData;

        [TestInitialize]
        public void TestInit()
        {
            _mockDatabaseReader = new Mock<IDatabaseReader>();
            _productData = new ProductData(_mockDatabaseReader.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenConstructedWithNullDatabaseReader_ThrowsArgumentNullException()
        {
            _productData = new ProductData(null);
        }

        [TestMethod]
        [ExpectedException(typeof (JsonReaderException))]
        public void OnExceptionThrownFromParsingTheJson_LetTheExceptionBubbleUp()
        {
            GivenTheDatabaseReaderReturnsBadJsonString();
            _productData.GetAllProducts();
        }

        [TestMethod]
        [ExpectedException(typeof (SomeWeirdException))]
        public void WhenJsonProducerThrowsException_LetTheExceptionBubbleUp()
        {
            GivenTheDatabaseReaderThrowsAnExceptionWhenReadingTheDatabse();
            _productData.GetAllProducts();
        }

        [TestMethod]
        public void OnlyUsesTheDataReaderToReadDataFromDatabaseOnce()
        {
            GivenTheDatabaseReaderReturnsValidJsonString();
            _productData.GetAllProducts();
            _productData.GetAllProducts();
            _mockDatabaseReader.Verify(x => x.ReadAll(), Times.Exactly(1));
        }

        [TestMethod]
        public void ReturnedProductListDoesNotContainDuplicates()
        {
            GivenTheDatabaseReaderReturnsValidJsonString();
            var allProducts = _productData.GetAllProducts();
            bool hasDuplicates = allProducts.GroupBy(product => product).Any(g => g.Count() > 1);
            Assert.IsFalse(hasDuplicates);
        }

        [TestMethod]
        public void ShouldBeAbleToGetProductWithStyleId1()
        {
            GivenTheDatabaseReaderReturnsValidJsonString();
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
            GivenTheDatabaseReaderReturnsValidJsonString();
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
            GivenTheDatabaseReaderReturnsValidJsonString();
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
            GivenTheDatabaseReaderReturnsValidJsonString();
            var product = _productData.GetProductByStyleId(4);
            Assert.IsNull(product);
        }


        private void GivenTheDatabaseReaderThrowsAnExceptionWhenReadingTheDatabse()
        {
            _mockDatabaseReader.Setup(x => x.ReadAll()).Throws<SomeWeirdException>();
        }

        private void GivenTheDatabaseReaderReturnsBadJsonString()
        {
            _mockDatabaseReader.Setup(x => x.ReadAll()).Returns(TestHelper.MakeBadJsonString);
        }

        private void GivenTheDatabaseReaderReturnsValidJsonString()
        {
            _mockDatabaseReader.Setup(x => x.ReadAll()).Returns(TestHelper.MakeTestProductDataJson);
        }
    }
}
