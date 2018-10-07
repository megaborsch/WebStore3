using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.DomainNew.Filters;
using WebStore.DomainNew.Entities;
using WebStore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WebStore.Infrastructure.Implementations.Sql
{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreContext _context;

        public SqlProductData(WebStoreContext context)
        {
            _context = context;
        }

        public IEnumerable<Section> GetSections()
        {
            return _context.Sections.ToList();
        }

        public IEnumerable<Brand> GetBrands()
        {
            return _context.Brands.ToList();
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
        public IEnumerable<Product> GetProducts(ProductFilter filter)
        {
            var query = _context.Products.Include("Brand").Include("Section").AsQueryable();
            if (filter.Ids !=null && filter.Ids.Count > 0)
            {
                query = query.Where(c => filter.Ids.Contains(c.Id));
            }
            if (filter.BrandId.HasValue)
                query = query.Where(c => c.BrandId.HasValue &&
                c.BrandId.Value.Equals(filter.BrandId.Value));
            if (filter.SectionId.HasValue)
                query = query.Where(c => c.SectionId.Equals(filter.SectionId.Value));
            return query.ToList();
        }
        public Product GetProductById(int id)
        {
            return _context.Products.Include("Brand").Include("Section").FirstOrDefault(p => p.Id.Equals(id));
        }

        public void DeleteProductById(int id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Products.Remove(GetProductById(id));
                _context.SaveChanges();
                transaction.Commit();
                
            }

        }

        public Product CreateProduct(Product product)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                if (_context.Products.Last() != null)
                { product.Order = _context.Products.Last().Order + 1; }
                else
                {
                    product.Order = 1;
                }
                _context.Products.Add(product);
            
            _context.SaveChanges();
            transaction.Commit();
            return product;
            }
        }

        public Product EditProduct(Product product)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                
                _context.Products.Update(product);

                _context.SaveChanges();
                transaction.Commit();
                return product;
            }
        }

    }
}
