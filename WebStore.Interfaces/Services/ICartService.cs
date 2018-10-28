using System.Collections.Generic;
using WebStore.DomainNew.Models.Cart;
using WebStore.DomainNew.Dto.Order;
namespace WebStore.Interfaces.Services
{
    public interface ICartService
    {
        void DecrementFromCart(int id);
        void RemoveFromCart(int id);
        void RemoveAll();
        void AddToCart(int id);
        CartViewModel TransformCart();
        List<OrderItemDto> TCart();
    }
}
