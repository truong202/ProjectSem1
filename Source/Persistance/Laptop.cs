﻿using System;

namespace Persistance
{
    public class Laptop
    {
        public int? LaptopId { set; get; }
        public string LaptopName { set; get; }
        public Category CategoryInfo { set; get; }
        public Manufactory ManufactoryInfo { set; get; }
        public string CPU { set; get; }
        public string Ram { set; get; }
        public string HardDrive { set; get; }
        public string VGA { set; get; }
        public string Display { set; get; }
        public string Battery { set; get; }
        public string Weight { set; get; }
        public string Materials { set; get; }
        public string Ports { set; get; }
        public string NetworkAndConnection { set; get; }
        public string Security { set; get; }
        public string Keyboard { set; get; }
        public string Audio { set; get; }
        public string Size { set; get; }
        public string OS { set; get; }
        public int Quantity { set; get; }
        public Decimal Price { set; get; }
        public string WarrantyPeriod { set; get; }

        public Laptop()
        {
            CategoryInfo = new Category();
            ManufactoryInfo = new Manufactory();
        }
        public override bool Equals(object obj)
        {
            if (obj is Laptop)
            {
                Laptop laptop = (Laptop)obj;
                return this.LaptopId.Equals(laptop.LaptopId);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return this.LaptopId.GetHashCode();
        }
    }
}
