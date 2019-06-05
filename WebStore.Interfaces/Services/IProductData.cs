using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DomainNew.Dto;
using WebStore.DomainNew.Dto.Product;
using WebStore.DomainNew.Filters;
using WebStore.DomainNew.Entities;
namespace WebStore.Interfaces.Services
{
    /// <summary>
    /// Интерфейс для работы с товарами
    /// </summary>
    public interface IProductData
    {
        /// <summary>
        /// Список секций
        /// </summary>
        /// <returns></returns>
        IEnumerable<Section> GetSections();

        /// <summary>
        /// Cекция по Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        Section GetSectionById(int id);

        /// <summary>
        /// Список брендов
        /// </summary>
        /// <returns></returns>
        IEnumerable<Brand> GetBrands();

        /// <summary>
        /// Бренд по Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        Brand GetBrandById(int id);

        /// <summary>
        /// Список товаров с постраничным разбиением
        /// </summary>
        /// <param name="filter">Фильтр товаров</param>
        /// <returns></returns>
        PagedProductDto GetProducts(ProductFilter filter);

        /// <summary>
        /// Продукт
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Сущность Product, если нашёл, иначе null</returns>
        ProductDto GetProductById(int id);

        /// <summary>
        /// Создать продукт
        /// </summary>
        /// <param name="product">Сущность Product</param>
        /// <returns></returns>
        SaveResult CreateProduct(ProductDto product);

        /// <summary>
        /// Обновить продукт
        /// </summary>
        /// <param name="product">Сущность Product</param>
        /// <returns></returns>
        SaveResult UpdateProduct(ProductDto product);

        /// <summary>
        /// Удалить продукт
        /// </summary>
        /// <param name="productId">Id продукта</param>
        /// <returns></returns>
        SaveResult DeleteProduct(int productId);

    }
}
