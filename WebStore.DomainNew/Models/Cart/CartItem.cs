using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.DomainNew.Models.Cart
{
    public class CartItem
    {
        public int ProductID { get; set; }

        public int Quantity { get; set; }
    }
}
