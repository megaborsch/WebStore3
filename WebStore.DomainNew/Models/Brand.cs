using System;
using System.Collections.Generic;
using System.Text;
using WebStore.DomainNew.Models.Base;

namespace WebStore.DomainNew.Models
{
    public class Brand : OrderedEntity
    {
        public virtual ICollection<Product> Products { get; set; }
    }
}
