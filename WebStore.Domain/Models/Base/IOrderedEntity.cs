using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.Models.Base
{
    public interface IOrderedEntity : INamedEntity
    {
        /// <summary>
        /// Порядок
        /// </summary>
        int Order { get; set; }
    }
}
