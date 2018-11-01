using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.DomainNew.Filters;
using WebStore.DomainNew.Dto.Product;
using WebStore.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace WebStore.Services.Sql
{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreContext _context;

        public SqlProductData(WebStoreContext context)
        {
            _context = context;
        }

        public IEnumerable<SectionDto> GetSections()
        {
            return _context.Sections.Select(s => new SectionDto()
            {
                Id = s.Id,
                Name = s.Name,
                Order = s.Order,
                ParentId = s.ParentId
            }).ToList();
        }

        public SectionDto GetSectionById(int id)
        {
            var section = _context.Sections.FirstOrDefault(c => c.Id == id);
            if (section != null)
            {
                return new SectionDto()
                {
                    Id = section.Id,
                    Name = section.Name,
                    ParentId = section.ParentId,
                    Order = section.Order
                };
            }
            return null;
        }

        public IEnumerable<BrandDto> GetBrands()
        {
            return _context.Brands.Select(b => new BrandDto()
            {
                Id = b.Id,
                Name = b.Name,
                Order = b.Order
            }).ToList();
        }

        public BrandDto GetBrandById(int id)
        {
            var brand = _context.Brands.FirstOrDefault(b => b.Id == id);

            if (brand != null)
            {
                return new BrandDto()
                {
                    Id = brand.Id,
                    Name = brand.Name,
                    Order = brand.Order
                };
            }
            return null;
        }
        //public IEnumerable<Product> GetProducts(ProductFilter filter)
        //{
        //    var query = _context.Products.AsQueryable();

        //    if (filter.BrandId.HasValue)
        //        query = query.Where(c => c.BrandId.HasValue && c.BrandId.Value.Equals(filter.BrandId.Value));
        //    if (filter.SectionId.HasValue)
        //        query = query.Where(c => c.SectionId.Equals(filter.SectionId.Value));

        //    return query.ToList();
        //}
        public PagedProductDto GetProducts(ProductFilter filter)
        {
            var query = _context.Products.Include("Brand").Include("Section").AsQueryable();
            if (filter.Ids !=null && filter.Ids.Count > 0)
            {
                query = query.Where(c => filter.Ids.Contains(c.Id));
            }
            

            if (filter.BrandId.HasValue)
                query = query.Where(c => c.BrandId.HasValue && c.BrandId.Value.Equals(filter.BrandId.Value));
            if (filter.SectionId.HasValue)
                query = query.Where(c => c.SectionId.Equals(filter.SectionId.Value));

            var model = new PagedProductDto
            {
                TotalCount = query.Count()
            };
            if (filter.PageSize.HasValue)
            {
                model.Products = query.OrderBy(c => c.Order).Skip((filter.Page - 1) * filter.PageSize.Value).Take(filter.PageSize.Value)
                    .Select(p =>
                        new ProductDto
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Order = p.Order,
                            Price = p.Price,
                            ImageUrl = p.ImageUrl,
                            Brand = p.BrandId.HasValue ? new BrandDto() { Id = p.Brand.Id, Name = p.Brand.Name } : null,
                            Section = new SectionDto() { Id = p.SectionId, Name = p.Section.Name }
                        }).ToList();
            }
            else
            {
                model.Products = query.OrderBy(c => c.Order).Select(p =>
                    new ProductDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Order = p.Order,
                        Price = p.Price,
                        ImageUrl = p.ImageUrl,
                        Brand = p.BrandId.HasValue ? new BrandDto() { Id = p.Brand.Id, Name = p.Brand.Name } : null,
                        Section = new SectionDto() { Id = p.SectionId, Name = p.Section.Name }
                    }).ToList();
            }
            return model;
        }
        public ProductDto GetProductById(int id)
        {
            var product = _context.Products.Include("Brand").Include("Section").FirstOrDefault(p => p.Id.Equals(id));
            if (product == null) return null;

            var dto = new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                ImageUrl = product.ImageUrl,
                Order = product.Order,
                Price = product.Price,
                Section = new SectionDto() { Id = product.SectionId, Name = product.Section.Name, ParentId = product.Section.ParentId, Order = product.Section.Order }
            };
            if (product.Brand != null)
                dto.Brand = new BrandDto()
                {
                    Id = product.Brand.Id,
                    Name = product.Brand.Name,
                    Order = product.Brand.Order
                };
            return dto;
        }

    //    public void DeleteProductById(int id)
    //    {
    //        using (var transaction = _context.Database.BeginTransaction())
    //        {
    //            _context.Products.Remove(GetProductById(id));
    //            _context.SaveChanges();
    //            transaction.Commit();
                
    //        }

    //    }

    //    public ProductDto CreateProduct(ProductDto product)
    //    {
    //        using (var transaction = _context.Database.BeginTransaction())
    //        {
    //            if (_context.Products.Last() != null)
    //            { product.Order = _context.Products.Last().Order + 1; }
    //            else
    //            {
    //                product.Order = 1;
    //            }
    //            _context.Products.Add(product);
            
    //        _context.SaveChanges();
    //        transaction.Commit();
    //        return product;
    //        }
    //    }

    //    public Product EditProduct(Product product)
    //    {
    //        using (var transaction = _context.Database.BeginTransaction())
    //        {
                
    //            _context.Products.Update(product);

    //            _context.SaveChanges();
    //            transaction.Commit();
    //            return product;
    //        }
    //    }

    }
}
