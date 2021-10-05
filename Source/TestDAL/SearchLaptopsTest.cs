using System;
using Xunit;
using DAL;
using Persistance;
using System.Collections.Generic;


namespace TestDAL
{
    public class SearchLaptopTest
    {
        LaptopDAL laptopDAL = new LaptopDAL();

        [Theory]
        [InlineData("a")]
        [InlineData("office")]
        [InlineData("macbook")]
        [InlineData("asus")]
        public void SearchLaptopsTest1(string searchValue)
        {
            List<Laptop> result = laptopDAL.Search(searchValue);
            Assert.True(result != null);
            foreach (var laptop in result)
                Assert.True(laptop.LaptopName.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                laptop.ManufactoryInfo.ManufactoryName.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                laptop.CategoryInfo.CategoryName.Contains(searchValue, StringComparison.OrdinalIgnoreCase));
        }

        [Theory]
        [InlineData("sdfasjhfj")]
        public void SearchLaptopsTest2(string searchValue)
        {
            List<Laptop> result = laptopDAL.Search(searchValue);
            Assert.True(result == null);
        }
    }
}