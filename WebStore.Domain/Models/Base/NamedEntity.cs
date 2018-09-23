using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.Models.Base
{
    public class NamedEntity : BaseEntity, INamedEntity
    {
        public string Name { get; set; }
    }
}
