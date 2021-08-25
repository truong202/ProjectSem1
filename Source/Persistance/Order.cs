using System;
using System.Collections.Generic;

namespace Persistance
{
    public class Order
    {
        public int? OrderId { get; set; }
        public Customer OrderCustomer { get; set; }
        public List<Laptop> Laptops { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public Decimal Price { get; set; }

        public Order()
        {
            Customer customer = new Customer();
            Laptops = new List<Laptop>();
        }
    }
}