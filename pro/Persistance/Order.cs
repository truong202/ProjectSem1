using System;
using System.Collections.Generic;

namespace Persistance
{
    public class Order
    {
        public int OrderId { get; set; }
        public Customer CustomerInfo { get; set; }
        public Staff Seller { get; set; }
        public Staff Accountance { get; set; }
        public List<Laptop> Laptops { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public const int UNPAID = 1;
        public const int PROCESSING = 2;
        public const int PAID = 3;
        public const int CANCEL = 4;

        public Order()
        {
            CustomerInfo = new Customer();
            Seller = new Staff();
            Accountance = new Staff();
            Laptops = new List<Laptop>();
        }
        public static List<Order> Split(List<Order> listOrder, int index, int count)
        {
            if (listOrder == null || listOrder.Count == 0) return null;
            List<Order> orders = new List<Order>();
            for (int i = index; i < index + count; i++)
            {
                orders.Add(listOrder[i]);
                if (i == listOrder.Count - 1) break;
            }
            return orders;
        }
    }
}