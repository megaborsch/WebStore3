using System;
using System.Collections.Generic;
using System.Text;
using WebStore.DomainNew.Entities.Base;

namespace WebStore.DomainNew.Entities
{
    public class Brand : OrderedEntity
    {
        public virtual ICollection<Product> Products { get; set; }
    }
}
