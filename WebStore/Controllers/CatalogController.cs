using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.DomainNew.Filters;
using WebStore.Interfaces.Services;
using WebStore.DomainNew.Models;
using WebStore.DomainNew.Models.Product;
using Microsoft.Extensions.Configuration;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _productData;
        private readonly IConfiguration _configuration;

        public CatalogController(IProductData productData, IConfiguration configuration)
        {
            _productData = productData;
            _configuration = configuration;
        }

        public IActionResult Shop(int? sectionId, int? brandId, int page = 1)
        {
            int pageSize = 6;
            int.TryParse(_configuration["PageSize"], out pageSize);
            var products = _productData.GetProducts(new ProductFilter
            {
                BrandId = brandId,
                SectionId = sectionId,
                Page = page,
                PageSize = pageSize
            });
            var model = new CatalogViewModel()
            {
                BrandId = brandId,
                SectionId = sectionId,
                Products = products.Products.Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    Name = p.Name,
                    Order = p.Order,
                    Price = p.Price,
                    Brand = p.Brand != null ? p.Brand.Name : string.Empty
                }).OrderBy(p => p.Order).ToList(),
                PageViewModel = new PageViewModel
                {
                    PageSize = int.Parse(_configuration["PageSize"]),
                    PageNumber = page,
                    TotalItems = products.TotalCount
                }
            };

            return View(model);
        }

        public IActionResult Details(int id)
        {
            var product = _productData.GetProductById(id);
            if (product == null)
                return NotFound();
            return View(new ProductViewModel
            {
                Id = product.Id,
                ImageUrl = product.ImageUrl,
                Name = product.Name,
                Order = product.Order,
                Price = product.Price,
                Brand = product.Brand != null ? product.Brand.Name : string.Empty
            });

        }
    }
}