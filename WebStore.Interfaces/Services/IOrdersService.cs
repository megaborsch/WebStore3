using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DomainNew.Entities;
using WebStore.DomainNew.Dto.Order;
using WebStore.DomainNew.Dto.Product;
using WebStore.DomainNew.Models.Cart;
using WebStore.DomainNew.Models.Order;

namespace WebStore.Interfaces.Services
{
    //public interface IOrdersService
    //{
    //    IEnumerable<Order> GetUserOrders(string userName);

    //    Order GetOrderById(int id);

    //    Order CreateOrder(OrderViewModel orderModel, CartViewModel transformCart, string userName);
    //}

    public interface IOrdersService
    {
        IEnumerable<OrderDto> GetUserOrders(string userName);
        OrderDto GetOrderById(int id);
        OrderDto CreateOrder(CreateOrderModel orderModel, string userName);
    }
}
