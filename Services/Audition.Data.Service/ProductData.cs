using System;
using System.Collections.Generic;
using System.Linq;
using Audition.Domain.Entities;
using Newtonsoft.Json;

namespace Audition.Data.Service
{
    public class ProductData : IProductData
    {
        private readonly IDatabaseReader _databaseReader;
        private List<Product> _distinctProductList = new List<Product>();
        private Dictionary<int, Product> _productTable = new Dictionary<int, Product>(); // key is StyleId
        private readonly object _sync = new object();
        private bool _dataLoaded;

        public ProductData(IDatabaseReader databaseReader)
        {
            if (databaseReader == null)
            {
                throw new ArgumentNullException("databaseReader");
            }
            _databaseReader = databaseReader;
        }

        public List<Product> GetAllProducts()
        {
            CheckAndLoadProductDataIfNeedTo();
            return _distinctProductList.Select(x => new Product(x)).ToList();
        }

        public Product GetProductByStyleId(int styleId)
        {
            CheckAndLoadProductDataIfNeedTo();
            return _productTable.ContainsKey(styleId) ? new Product(_productTable[styleId]) : null;
        }

        private void CheckAndLoadProductDataIfNeedTo()
        {
            lock (_sync)
            {
                if (_dataLoaded)
                {
                    return;
                }
                LoadProductData();
                _dataLoaded = true;
            }
        }

        private void LoadProductData()
        {
            var jsonData = _databaseReader.ReadAll();
            var productList = JsonConvert.DeserializeObject<List<Product>>(jsonData);
            _distinctProductList = productList.Distinct().ToList();
            _productTable = _distinctProductList.ToDictionary(x => x.StyleId, x => x);
        }
    }
}