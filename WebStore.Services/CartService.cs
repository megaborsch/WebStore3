﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebStore.DomainNew.Filters;
using WebStore.Interfaces.Services;
using WebStore.DomainNew.Models.Cart;
using WebStore.DomainNew.Models.Product;
using WebStore.DomainNew.Dto.Order;

namespace WebStore.Infrastructure.Implementations
{
    public class CartService : ICartService

    {
        private readonly IProductData _productData;
        private readonly ICartStore _cartStore;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly string _cartName;
        //private Cart Cart
        //{
        //    get
        //    {
        //        var cookie = _httpContextAccessor.HttpContext.Request.Cookies[_cartName];
        //        string json = string.Empty;
        //        Cart cart = null;
        //        if (cookie == null)
        //        {
        //            cart = new Cart { Items = new List<CartItem>() };
        //            json = JsonConvert.SerializeObject(cart);
        //            _httpContextAccessor.HttpContext.Response.Cookies.Append(_cartName, json, new
        //                CookieOptions()
        //            {
        //                Expires = DateTime.Now.AddDays(1)
        //            });
        //            return cart;
        //        }
        //        json = cookie;
        //        cart = JsonConvert.DeserializeObject<Cart>(json);
        //        _httpContextAccessor.HttpContext.Response.Cookies.Delete(_cartName);
        //        _httpContextAccessor.HttpContext.Response.Cookies.Append(_cartName, json, new
        //        CookieOptions()
        //        {
        //            Expires = DateTime.Now.AddDays(1)
        //        });
        //        return cart;
        //    }
        //    set
        //    {
        //        var json = JsonConvert.SerializeObject(value);
        //        _httpContextAccessor.HttpContext.Response.Cookies.Delete(_cartName);
        //        _httpContextAccessor.HttpContext.Response.Cookies.Append(_cartName, json, new
        //        CookieOptions()
        //        {
        //            Expires = DateTime.Now.AddDays(1)
        //        });
        //    }
        //}
        public CartService(IProductData productData, ICartStore cartStore) //, IHttpContextAccessor httpContextAccessor
        {
            _productData = productData;
            _cartStore = cartStore;
            //_httpContextAccessor = httpContextAccessor;
            //_cartName = "cart" + (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated ?
            //_httpContextAccessor.HttpContext.User.Identity.Name : "");
        }
        public void DecrementFromCart(int id)
        {
            var cart = _cartStore.Cart;
            var item = cart.Items.FirstOrDefault(x => x.ProductID == id);
            if (item != null)
            {
                if (item.Quantity > 0)
                    item.Quantity--;
                if (item.Quantity == 0)
                    cart.Items.Remove(item);
            }
            _cartStore.Cart = cart;
        }
        public void RemoveFromCart(int id)
        {
            var cart = _cartStore.Cart;
            var item = cart.Items.FirstOrDefault(x => x.ProductID == id);
            if (item != null)
            {
                cart.Items.Remove(item);
            }
            _cartStore.Cart = cart;
        }
        public void RemoveAll()
        {
            _cartStore.Cart.Items.Clear();
        }
        public void AddToCart(int id)
        {
            var cart = _cartStore.Cart;
            var item = cart.Items.FirstOrDefault(x => x.ProductID == id);
            if (item != null)
                item.Quantity++;
            else
                cart.Items.Add(new CartItem { ProductID = id, Quantity = 1 });
            _cartStore.Cart = cart;
        }
        public CartViewModel TransformCart()
        {
            var products = _productData.GetProducts(new ProductFilter()
            {
                Ids = _cartStore.Cart.Items.Select(i => i.ProductID).ToList()
            }).Select(p => new ProductViewModel()
            {
                Id = p.Id,
                ImageUrl = p.ImageUrl,
                Name = p.Name,
                Order = p.Order,
                Price = p.Price,
                Brand = p.Brand != null ? p.Brand.Name : string.Empty
            }).ToList();
            var r = new CartViewModel
            {
                Items = _cartStore.Cart.Items.ToDictionary(x => products.First(y => y.Id == x.ProductID), x => x.Quantity)
            };
            return r;
        }
        public List<OrderItemDto> TCart()
        {
            var orderItems = _productData.GetProducts(new ProductFilter()
            {
                Ids = _cartStore.Cart.Items.Select(i => i.ProductID).ToList()
            }).Select(p => new OrderItemDto()
            {
                Id = p.Id,
                Price = p.Price,
                Quantity = _cartStore.Cart.Items.First(i => i.ProductID == p.Id).Quantity
            }).ToList();

            return orderItems;
        }
    }
}
