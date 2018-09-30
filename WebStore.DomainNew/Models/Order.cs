using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WebStore.DomainNew.Models;
using WebStore.DomainNew.Models.Base;

namespace WebStore.DomainNew.Models
{
    public class Order:NamedEntity
    {
        public virtual User User { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public DateTime Date { get; set; }

        public virtual Collection<OrderItem> OrderItems { get; set; }
    }
}
