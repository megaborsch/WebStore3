using WebStore.DomainNew.Entities.Base;

namespace WebStore.DomainNew.Dto.Product
{
    public class SectionDto : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public int? ParentId { get; set; }
    }
}
