 using System;
using System.Collections.Generic;
 using System.Configuration;
 using System.Web.Mvc;
 using Audition.Data.Service;
 using Audition.Domain.Entities;
 using Audition.Models;

namespace Audition.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductData _productData;

        public HomeController(IProductData productData)
        {
            if (productData == null)
            {
                throw new ArgumentNullException("productData", "Check Ioc config.");
            }
            _productData = productData;
        }

        public ViewResult Index()
        {
            LoadProductData();
            List<Product> productList = _productData.GetAllProducts();
            foreach (var product in productList)
            {
                product.ImageUrl = string.Format(ConfigurationManager.AppSettings["ImageThumbnailPartialUrl"],
                                                 product.ImageUrl);
            }
            var model = new SearchResultModel(productList);
            return View(model);
        }

        public ViewResult ProductDetail(int styleId)
        {
            LoadProductData();
            var product = _productData.GetProductByStyleId(styleId);
            product.ImageUrl = string.Format(ConfigurationManager.AppSettings["ProductDetailImagePartialUrl"],
                                             product.ImageUrl);

            return View(product);
        }

        private void LoadProductData()
        {
            _productData.LoadProductDataFromJson(ReadJsonDatabase);
        }

        private string ReadJsonDatabase()
        {
            var databasePath = HttpContext.Server.MapPath("~/App_Data/Database.json");
            return System.IO.File.ReadAllText(databasePath);
        }
    }
}