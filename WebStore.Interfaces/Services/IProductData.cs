using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DomainNew.Dto.Product;
using WebStore.DomainNew.Filters;

namespace WebStore.Interfaces.Services
{
    public interface IProductData
    {
        IEnumerable<SectionDto> GetSections();
        IEnumerable<BrandDto> GetBrands();
        IEnumerable<ProductDto> GetProducts(ProductFilter filter);
        ProductDto GetProductById(int id);
        //ProductDto EditProduct(ProductDto Product);
        //ProductDto CreateProduct(ProductDto Product);
        //void DeleteProductById(int id);
    }
}
