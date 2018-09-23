using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.Models.Base
{
    public class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
    }
}
