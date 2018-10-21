using WebStore.DomainNew.Entities.Base;

namespace WebStore.DomainNew.Dto.Product
{
    public class ProductDto : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public BrandDto Brand { get; set; }
    }
}
