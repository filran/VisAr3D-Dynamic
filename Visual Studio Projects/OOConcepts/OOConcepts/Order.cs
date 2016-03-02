using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOConcepts
{
    class Order
    {
        public Product[] products;

        public Order()
        {
            this.products = new Product[1];
            this.products[0] = new Product();
            this.products[0].testProduct();
        }
    }
}
