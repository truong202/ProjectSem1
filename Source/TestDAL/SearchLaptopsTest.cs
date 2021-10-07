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
                Assert.True(laptop.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                laptop.ManufactoryInfo.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                laptop.CategoryInfo.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase));
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