﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebStore.DomainNew.Dto.Order;
using WebStore.DomainNew.Dto.Product;
using WebStore.Interfaces.Services;

namespace WebStore.ServicesHosting.Controllers
{
    [Produces("application/json")]
    [Route("api/orders")]
    public class OrdersApiController : Controller, IOrdersService
    {
        private readonly IOrdersService _ordersService;
        public OrdersApiController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }
        [HttpGet("user/{userName}")]
        public IEnumerable<OrderDto> GetUserOrders(string userName)
        {
            return _ordersService.GetUserOrders(userName);
        }
        [HttpGet("{id}"), ActionName("Get")]
        public OrderDto GetOrderById(int id)
        {
            return _ordersService.GetOrderById(id);
        }
        [HttpPost("{userName?}")]
        public OrderDto CreateOrder([FromBody]CreateOrderModel orderModel,
        string userName)
        {
            return _ordersService.CreateOrder(orderModel, userName);
        }
    }
}