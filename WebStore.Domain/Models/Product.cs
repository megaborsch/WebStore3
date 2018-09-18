using WebStore.Domain.Models.Base;

namespace WebStore.Domain.Models
{
    /// <inheritdoc cref="NamedEntity" />
    /// <summary>
    /// Сущность продукт
    /// </summary>
    public class Product : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        /// <summary>
        /// Секция к которой принадлежит товар
        /// </summary>
        public int SectionId { get; set; }

        /// <summary>
        /// Бренд товара
        /// </summary>
        public int? BrandId { get; set; }

        /// <summary>
        /// Ссылка на картинку
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }
    }
}
