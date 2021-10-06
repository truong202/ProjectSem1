using System;
using Persistance;
using DAL;
using System.Collections.Generic;

namespace BL  
{
    public class LaptopBL
    {  
        private LaptopDAL laptopDAL = new LaptopDAL();
        public Laptop GetById(int laptopId)
        {
            return laptopDAL.GetById(laptopId);
        }
        public List<Laptop> Search(string searchValue, int offset)
        {
            return laptopDAL.Search(searchValue, offset);
        }
        public int GetCount(string searchValue)
        {
            return laptopDAL.GetCount(searchValue);
        }
    }
}
