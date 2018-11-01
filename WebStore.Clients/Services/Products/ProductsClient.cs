﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.DomainNew.Dto.Product;
using WebStore.DomainNew.Filters;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Services.Products
{
    public class ProductsClient : BaseClient, IProductData
    {
        public ProductsClient(IConfiguration configuration) : base(configuration)
        {
            ServiceAddress = "api/products";
        }

        protected sealed override string ServiceAddress { get; set; }

        public IEnumerable<SectionDto> GetSections()
        {
            var url = $"{ServiceAddress}/sections";
            var result = Get<List<SectionDto>>(url);
            return result;
        }

        public SectionDto GetSectionById(int id)
        {
            var url = $"{ServiceAddress}/sections/{id}";
            var result = Get<SectionDto>(url);
            return result;
        }

        public IEnumerable<BrandDto> GetBrands()
        {
            var url = $"{ServiceAddress}/brands";
            var result = Get<List<BrandDto>>(url);
            return result;

        }

        public BrandDto GetBrandById(int id)
        {
            var url = $"{ServiceAddress}/brands/{id}";
            var result = Get<BrandDto>(url);
            return result;
        }

        public PagedProductDto GetProducts(ProductFilter filter)
        {
            var url = $"{ServiceAddress}";
            var response = Post(url, filter);
            var result = response.Content.ReadAsAsync<PagedProductDto>().Result;
            return result;
        }

        public ProductDto GetProductById(int id)
        {
            var url = $"{ServiceAddress}/{id}";
            var result = Get<ProductDto>(url);
            return result;
        }
    }
}
