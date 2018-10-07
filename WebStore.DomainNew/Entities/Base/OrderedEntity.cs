using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.DomainNew.Entities.Base
{
    public class OrderedEntity : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

    }
}
