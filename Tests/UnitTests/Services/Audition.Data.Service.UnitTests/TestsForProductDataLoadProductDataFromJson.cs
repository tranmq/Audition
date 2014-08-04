using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Audition.Data.Service.UnitTests
{
    [TestClass]
    public class TestsForProductDataLoadProductDataFromJson
    {
        private readonly ProductData _productData = new ProductData();

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenTheJsonProducerIsNull_ThrowsArgumentNullException()
        {
            _productData.LoadProductDataFromJson(null);
        }

        [TestMethod]
        [ExpectedException(typeof (JsonReaderException))]
        public void OnExceptionThrownFromParsingTheJson_LetTheExceptionBubbleUp()
        {
            _productData.LoadProductDataFromJson(TestHelper.MakeBadJsonString);
        }

        [TestMethod]
        [ExpectedException(typeof (SomeWeirdException))]
        public void WhenJsonProducerThrowsException_LetTheExceptionBubbleUp()
        {
            _productData.LoadProductDataFromJson(TestHelper.JsonProducerThatThrowsException);
        }

        [TestMethod]
        public void WhenJsonDataIsGood_ShouldSucceed()
        {
            _productData.LoadProductDataFromJson(TestHelper.MakeTestProductDataJson);
        }
    }
}
