using System;
using System.Collections.Generic;
using System.Text;
using WebStore.DomainNew.Entities.Base;

namespace WebStore.DomainNew.Entities.Base
{
    public class NamedEntity : BaseEntity, INamedEntity
    {
        public string Name { get; set; }
    }
}
