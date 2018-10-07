using System;
using System.Collections.Generic;
using System.Text;
using WebStore.DomainNew.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.DomainNew.Entities
{
    public class Section: OrderedEntity
    {
        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual Section ParentSection { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
