using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.DomainNew.Dto.Product;
using WebStore.Interfaces.Services;
using WebStore.DomainNew.Filters;

namespace WebStore.ServicesHosting.Controllers
{
    [Produces("application/json")]
    [Route("api/products")]
    public class ProductApiController : Controller, IProductData
    {
        private readonly IProductData _productData;

        public ProductApiController(IProductData productData)
        {
            _productData = productData;
        }

        [HttpGet("sections")]//GET api/products/sections
        public IEnumerable<SectionDto> GetSections()
        {
            return _productData.GetSections();
        }

        [HttpGet("sections/{id}")]
        public SectionDto GetSectionById(int id)
        {
            return _productData.GetSectionById(id);
        }

        [HttpGet("brands")]//GET api/products/brands
        public IEnumerable<BrandDto> GetBrands()
        {
            return _productData.GetBrands();
        }

        [HttpGet("brands/{id}")]
        public BrandDto GetBrandById(int id)
        {
            return _productData.GetBrandById(id);
        }

        [HttpPost]//POST api/products
        public IEnumerable<ProductDto> GetProducts([FromBody]ProductFilter filter)
        {
            return _productData.GetProducts(filter);
        }

        [HttpGet("{id}")]//GET api/products/{id}
        public ProductDto GetProductById(int id)
        {
            return _productData.GetProductById(id);
        }
        //[HttpGet("{id}")]//GET api/products/delete/{id}
        //public void DeleteProductById(int id)
        //{
        //    _productData.DeleteProductById(id);
        //}
    }
}