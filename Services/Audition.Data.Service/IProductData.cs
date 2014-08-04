using System;
using System.Collections.Generic;
using Audition.Domain.Entities;

namespace Audition.Data.Service
{
    public interface IProductData
    {
        /// <summary>
        /// Loads product data using the given jsonProducer.
        /// </summary>
        /// <param name="jsonProducer">
        /// The function produces the JSON string to be consumed.
        /// </param>
        void LoadProductDataFromJson(Func<string> jsonProducer);

        List<Product> GetAllProducts();

        Product GetProductByStyleId(int styleId);
    }
}