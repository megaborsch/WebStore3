using System;
using System.Collections.Generic;
using System.Text;
using WebStore.DomainNew.Models.Base;

namespace WebStore.DomainNew.Models.Base
{
    public class NamedEntity : BaseEntity, INamedEntity
    {
        public string Name { get; set; }
    }
}
