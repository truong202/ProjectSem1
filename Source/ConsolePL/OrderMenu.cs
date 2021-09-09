using System;
using System.Collections.Generic;
using BL;
using Persistance;

namespace ConsolePL
{
    public class OrderMenu
    {
        private Order order = new Order();
        private OrderBL orderBL = new OrderBL();
        private LaptopBL laptopBL = new LaptopBL();
        public void CreateOrder(Staff staff)
        {
            order.Seller.StaffId = staff.StaffId;
            Laptop laptop;
            int id;
            if (order.Laptops.Count == 0)
                do
                {
                    Console.WriteLine();
                    Console.WriteLine(" * Input List Laptop");
                    Console.Write(" → Input ID(input 0 to cannel): ");
                    id = Utility.GetNumber();
                    if (id == 0) break;
                    laptop = laptopBL.GetById(id);
                    if (laptop == null)
                    {
                        Console.WriteLine(" Laptop not found!");
                    }
                    else
                    {
                        Console.Write(" → Input quantity: ");
                        laptop.Quantity = Utility.GetQuantity();
                        AddLaptopToOrder(laptop);
                        Console.WriteLine(" Press any key to continue...");
                        Console.ReadKey(true);
                    }
                } while (id != 0);
            if (order.Laptops.Count > 0)
            {
                order.CustomerInfo = GetCustomer();
                bool result = orderBL.CreateOrder(order);
                Console.ForegroundColor = result ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine(" Create order " + (result ? "completed!" : "not complete!"));
                order = new Order();
                Console.ResetColor();
                Console.CursorVisible = false;
                Console.Write(" Press any key to back..."); Console.ReadKey(true);
            }
        }
        private Customer GetCustomer()
        {
            Console.WriteLine("\n * Customer information");
            Customer customer = new Customer();
            Console.CursorVisible = true;
            Console.Write(" → Phone: ");
            customer.Phone = Utility.GetPhone();
            var cus = new CustomerBL().GetByPhone(customer.Phone);
            if (cus != null)
            {
                customer = cus;
                Console.WriteLine(" → Customer name: " + customer.CustomerName);
                Console.WriteLine(" → Address: " + customer.Address);
            }
            else
            {
                Console.Write(" → Customer name: ");
                customer.CustomerName = Utility.Standardize(Utility.GetName());
                Console.Write(" → Address: ");
                customer.Address = Console.ReadLine();
            }
            return customer;
        }
        public bool AddLaptopToOrder(Laptop laptop)
        {
            int count = laptopBL.GetById(laptop.LaptopId).Quantity;
            int quantity = 0;
            int index = order.Laptops.IndexOf(laptop);
            if (index != -1)
            {
                quantity = laptop.Quantity + order.Laptops[index].Quantity;
                if (quantity > count) return false;
                order.Laptops[index].Quantity += laptop.Quantity;
            }
            else
            {
                if (laptop.Quantity > count) return false;
                order.Laptops.Add(laptop);
            }
            return true;
        }
    }
}