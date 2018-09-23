using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.Models.Base
{
    public interface INamedEntity : IBaseEntity
    {
        /// <summary>
        /// Наименование
        /// </summary>
        string Name { get; set; }
    }
}
