using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.DomainNew.Models.Base
{
    public class OrderedEntity : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

    }
}
