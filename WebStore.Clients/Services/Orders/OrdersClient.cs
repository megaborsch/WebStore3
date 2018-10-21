﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WebStore.Clients.Base;
using WebStore.DomainNew.Dto.Order;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Services.Orders
{
    public class OrdersClient : BaseClient, IOrdersService
    {
        public OrdersClient(IConfiguration configuration) : base(configuration)
        {
            ServiceAddress = "api/orders";
        }

        protected sealed override string ServiceAddress { get; set; }

        public IEnumerable<OrderDto> GetUserOrders(string userName)
        {
            if (userName == null)
            {
                throw new ArgumentNullException(nameof(userName));
            }

            var url = $"{ServiceAddress}/user/{userName} ";
            var result = Get<List<OrderDto>>(url);
            return result;
        }

        public OrderDto GetOrderById(int id)
        {
            var url = $"{ServiceAddress}/{id} ";
            var result = Get<OrderDto>(url);
            return result;
        }

        public OrderDto CreateOrder(CreateOrderModel orderModel, string userName)
        {
            if (orderModel == null)
            {
                throw new ArgumentNullException(nameof(orderModel));
            }

            if (userName == null)
            {
                throw new ArgumentNullException(nameof(userName));
            }

            var url = $"{ServiceAddress}/{userName} ";
            var response = Post(url, orderModel);
            var result = response.Content.ReadAsAsync<OrderDto>().Result;
            return result;
        }
    }
}
