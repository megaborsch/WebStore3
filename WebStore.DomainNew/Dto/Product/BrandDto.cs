using WebStore.DomainNew.Entities.Base;

namespace WebStore.DomainNew.Dto.Product
{
    public class BrandDto : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
    }
}
