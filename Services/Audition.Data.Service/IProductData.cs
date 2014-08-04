using System;
using System.Collections.Generic;
using Audition.Domain.Entities;

namespace Audition.Data.Service
{
    public interface IProductData
    {
        List<Product> GetAllProducts();

        Product GetProductByStyleId(int styleId);
    }
}