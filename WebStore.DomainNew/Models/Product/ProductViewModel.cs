using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DomainNew.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace WebStore.DomainNew.Models.Product
{
    public class ProductViewModel : INamedEntity, IOrderedEntity
    {
        
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Название является обязательным")]
        [Display(Name = "Название")]
        public string Name { get; set; }
        public int Order { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
    }
}
