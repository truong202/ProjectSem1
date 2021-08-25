﻿using System;

namespace Persistance
{
    public class Laptop
    {
        public int? LaptopId { set; get; }
        public string LaptopName { set; get; }
        public Category CategoryInfor { set; get; }
        public Manufactory ManufactoryInfor { set; get; }
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

        public Laptop()
        {
            CategoryInfor = new Category();
            ManufactoryInfor = new Manufactory();
        }
    }
}