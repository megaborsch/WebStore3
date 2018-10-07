﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DomainNew.Entities.Base;

namespace WebStore.DomainNew.Models.Product
{
    public class BrandViewModel : INamedEntity, IOrderedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Количество товаров бренда
        /// </summary>
        public int ProductsCount { get; set; }

        public int Order { get; set; }
    }
}
