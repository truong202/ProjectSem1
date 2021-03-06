using System;
using System.Collections.Generic;

namespace Persistance {
    public class Laptop {
        public int ID { set; get; }
        public string Name { set; get; }
        public Category CategoryInfo { set; get; }
        public Manufactory ManufactoryInfo { set; get; }
        public string CPU { set; get; }
        public string Ram { set; get; }
        public string HardDrive { set; get; }
        public string VGA { set; get; }
        public string Display { set; get; }
        public string Battery { set; get; }
        public float Weight { set; get; }
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

        public Laptop() {
            CategoryInfo = new Category();
            ManufactoryInfo = new Manufactory();
        }
        public static List<Laptop> SplitList(List<Laptop> laptops, int index, int count) {
            if (laptops == null || laptops.Count == 0) return null;
            List<Laptop> listLaptop = new List<Laptop>();
            for (int i = index; i < index + count; i++) {
                listLaptop.Add(laptops[i]);
                if (i == laptops.Count - 1) break;
            }
            return listLaptop;
        }
        public override bool Equals(object obj) {
            if (obj is Laptop) {
                return ((Laptop)obj).ID.Equals(this.ID);
            }
            return false;
        }
        public override int GetHashCode() {
            return this.ID.GetHashCode();
        }
    }
}
