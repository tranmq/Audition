using System;
using System.Collections.Generic;
using System.Linq;
using Audition.Domain.Entities;
using Newtonsoft.Json;

namespace Audition.Data.Service
{
    public class ProductData : IProductData
    {
        private List<Product> _distinctProductList = new List<Product>();
        private Dictionary<int, Product> _productTable = new Dictionary<int, Product>(); // key is StyleId
        /// <summary>
        /// Loads product data using the given jsonProducer.
        /// </summary>
        /// <param name="jsonProducer">
        /// The function that reads data from a source and returns the JSON string.
        /// </param>
        public void LoadProductDataFromJson(Func<string> jsonProducer)
        {
            if (jsonProducer == null)
            {
                throw new ArgumentNullException();
            }

            var productList = JsonConvert.DeserializeObject<List<Product>>(jsonProducer());
            _distinctProductList = productList.Distinct().ToList();
            _productTable = _distinctProductList.ToDictionary(x => x.StyleId, x => x);
        }

        public List<Product> GetAllProducts()
        {
            return _distinctProductList;
        }

        public Product GetProductByStyleId(int styleId)
        {
            return _productTable.ContainsKey(styleId) ? _productTable[styleId] : null;
        }
    }
}