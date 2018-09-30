using Microsoft.AspNetCore.Mvc;
using WebStore.DomainNew.Filters;
using WebStore.Infrastructure.Interfaces;
using WebStore.DomainNew.Models;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IProductData _productData;

        public HomeController(IProductData productData)
        {
            _productData = productData;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductList()
        {
            var products = _productData.GetProducts(new ProductFilter());
            return View(products);
        }

        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _productData.DeleteProductById(id);
            return RedirectToAction(nameof(ProductList));
        }

        [Route("edit/{id?}")]
        public IActionResult Edit(int? id)
        {
            Product model;
            if (id.HasValue)
            {
                model = _productData.GetProductById(id.Value);
                if (ReferenceEquals(model, null))
                    return NotFound();// возвращаем результат 404 Not Found
            }
            else
            {
                model = new Product();
            }
            return View(model);
        }

        [HttpPost]
        [Route("edit/{id?}")]
        public IActionResult Edit(Product model)
        {
            // Проверяем модель на валидность
            if (ModelState.IsValid)
            {

                if (model.Id > 0)
                {
                    Product product = _productData.GetProductById(model.Id);
                    if (ReferenceEquals(product, null))
                        return NotFound();// возвращаем результат 404 Not Found
                    product.ImageUrl = model.ImageUrl;
                    product.Name = model.Name;
                    product.Price = model.Price;
                    product.BrandId = model.BrandId;
                    product.SectionId = model.SectionId;

                    _productData.EditProduct(product);
                }
                else
                {
                    Product product = new Product();
                    product.ImageUrl = model.ImageUrl;
                    product.Name = model.Name;
                    product.Price = model.Price;
                    product.BrandId = model.BrandId;
                    product.SectionId = model.SectionId;
                    _productData.CreateProduct(product);
                }
                return RedirectToAction(nameof(ProductList));
            }
            // Если не валидна, возвращаем её на представление
            return View(model);

        }

    }
}