using WebStore.DomainNew.Models.Cart;

namespace WebStore.Interfaces.Services
{
    public interface ICartStore
    {
        Cart Cart { get; set; }
    }
}
