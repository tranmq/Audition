using System.Collections.Generic;
using System.Collections.ObjectModel;
using Audition.Domain.Entities;

namespace Audition.Models
{
    public class SearchResultModel
    {
        public SearchResultModel(List<Product> productList)
        {
            if (productList != null)
            {
                ProductList = productList.AsReadOnly();
            }
        }
        public ReadOnlyCollection<Product> ProductList { get; private set; }
    }
}