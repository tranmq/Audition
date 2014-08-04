using System;
using System.Collections.Generic;
using Audition.Controllers;
using Audition.Data.Service;
using Audition.Domain.Entities;
using Audition.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Audition.UnitTests
{
    [TestClass]
    public class TestsForHomeController
    {
        private readonly Mock<IProductData> _mockProductData = new Mock<IProductData>();
        private readonly HomeController _homeController;
        private List<Product> _testProductList;
        private Product _testProduct;

        public TestsForHomeController()
        {
            _homeController = new HomeController(_mockProductData.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenConstructedWithNullProductData_ShouldThrowException()
        {
            new HomeController(null);
        }

        [TestMethod]
        public void IndexActionUsesANewCollectionInstanceForProducts()
        {
            GivenProductDataReturnsTheListOfTestProductsOnGetAllProducts();
            var viewResult = _homeController.Index();
            var model = viewResult.Model as SearchResultModel;
            Assert.IsNotNull(model);
            Assert.AreNotSame(_testProductList, model.ProductList);
        }


        [TestMethod]
        public void IndexViewModelContainsExpectedCollectionOfProducts()
        {
            GivenProductDataReturnsTheListOfTestProductsOnGetAllProducts();
            var viewResult = _homeController.Index();
            var model = viewResult.Model as SearchResultModel;
            Assert.IsNotNull(model);
            CollectionAssert.AreEquivalent(_testProductList, model.ProductList);
        }

        [TestMethod]
        public void IndexViewModelContainsCorrectUrlsForProductsImages()
        {
            GivenProductDataReturnsTheListOfTestProductsOnGetAllProducts();
            var viewResult = _homeController.Index();
            var model = viewResult.Model as SearchResultModel;
            Assert.IsNotNull(model);
            var expectedResults = new Dictionary<int, string>
                                  {
                                      {1, "/thumbnail/ia.jpg"},
                                      {2, "/thumbnail/ib.jpg"},
                                      {3, "/thumbnail/ic.jpg"}
                                  };
            foreach (var product in model.ProductList)
            {
                Assert.AreEqual(expectedResults[product.StyleId], product.ImageUrl);
            }
        }

        [TestMethod]
        public void ProductDetailActionShouldUseNewProductInstanceForTheModel()
        {
            GivenProductDataReturnsAProductOnGetProductByStyleId();
            var viewResult = _homeController.ProductDetail(1);
            var model = viewResult.Model as Product;
            Assert.IsNotNull(model);
            Assert.AreNotSame(_testProduct, model);
        }

        [TestMethod]
        public void ProductDetailViewModelContainsCorrectData()
        {
            GivenProductDataReturnsAProductOnGetProductByStyleId();
            var viewResult = _homeController.ProductDetail(1);
            var model = viewResult.Model as Product;
            Assert.IsNotNull(model);
            Assert.AreEqual(_testProduct, model);
        }

        [TestMethod]
        public void ProductDetailViewModelContainsCorrectUrlForDetailImage()
        {
            const string expectedImageUrl = "/detail/ia.jpg";

            GivenProductDataReturnsAProductOnGetProductByStyleId();
            var viewResult = _homeController.ProductDetail(1);
            var model = viewResult.Model as Product;
            Assert.IsNotNull(model);
            Assert.AreEqual(expectedImageUrl, model.ImageUrl);
        }

        private void GivenProductDataReturnsTheListOfTestProductsOnGetAllProducts()
        {
            _testProductList = new List<Product>
                               {
                                   new Product {Brand = "A", FormattedRegularPrice = "$1.00", ImageUrl = "ia", Name = "NA", StyleId = 1},
                                   new Product {Brand = "B", FormattedRegularPrice = "$2.00", ImageUrl = "ib", Name = "NB", StyleId = 2},
                                   new Product {Brand = "C", FormattedRegularPrice = "$3.00", ImageUrl = "ic", Name = "NC", StyleId = 3},
                               };
            _mockProductData.Setup(x => x.GetAllProducts()).Returns(_testProductList);
        }

        private void GivenProductDataReturnsAProductOnGetProductByStyleId()
        {
            _testProduct = new Product { Brand = "A",FormattedRegularPrice = "$1.00", ImageUrl = "ia", Name = "NA", StyleId = 1 };
            _mockProductData.Setup(x => x.GetProductByStyleId(1)).Returns(_testProduct);
        }
    }
}
