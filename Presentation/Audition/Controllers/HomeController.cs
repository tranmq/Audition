 using System;
using System.Collections.Generic;
 using System.Configuration;
 using System.Linq;
 using System.Web.Mvc;
 using Audition.Data.Service;
 using Audition.Domain.Entities;
 using Audition.Models;
 using Audition.Resources;

namespace Audition.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductData _productData;

        public HomeController(IProductData productData)
        {
            if (productData == null)
            {
                throw new ArgumentNullException("productData", StringResource.IoCError);
            }
            _productData = productData;
        }

        public ViewResult Index()
        {
            List<Product> productList = _productData.GetAllProducts().Select(x => new Product(x)).ToList();
            foreach (var product in productList)
            {
                product.ImageUrl = String.Format(ConfigurationManager.AppSettings["ImageThumbnailPartialUrl"], product.ImageUrl);
            }
            var model = new SearchResultModel(productList);
            return View(model);
        }

        public ViewResult ProductDetail(int styleId)
        {
            var product = new Product(_productData.GetProductByStyleId(styleId));
            product.ImageUrl = string.Format(ConfigurationManager.AppSettings["ProductDetailImagePartialUrl"],
                                             product.ImageUrl);

            return View(product);
        }
    }
}