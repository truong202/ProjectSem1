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
            {
                Assert.True(
                    laptop.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                    laptop.ManufactoryInfo.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                    laptop.CategoryInfo.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)
                );
            }
        }
        [Theory]
        [InlineData("a # desc")]
        [InlineData("office # desc")]
        [InlineData("asus # desc")]
        public void SearchLaptopsTest2(string searchValue)
        {
            var filters = searchValue.Split("#");
            List<Laptop> result = laptopDAL.Search(searchValue);
            Assert.True(result != null);
            foreach (var laptop in result)
            {
                Assert.True(
                    laptop.Name.Contains(filters[0].Trim(), StringComparison.OrdinalIgnoreCase) ||
                    laptop.ManufactoryInfo.Name.Contains(filters[0].Trim(), StringComparison.OrdinalIgnoreCase) ||
                    laptop.CategoryInfo.Name.Contains(filters[0].Trim(), StringComparison.OrdinalIgnoreCase)
                );
            }
            for (int i = 0; i < result.Count - 1; i++)
            {
                Assert.True(result[i].Price >= result[i + 1].Price);
            }
        }

        [Theory]
        [InlineData("a # asc")]
        [InlineData("office # asc")]
        [InlineData("asus # asc")]
        public void SearchLaptopsTest3(string searchValue)
        {
            var filters = searchValue.Split("#");
            List<Laptop> result = laptopDAL.Search(searchValue);
            Assert.True(result != null);
            foreach (var laptop in result)
            {
                Assert.True(
                    laptop.Name.Contains(filters[0].Trim(), StringComparison.OrdinalIgnoreCase) ||
                    laptop.ManufactoryInfo.Name.Contains(filters[0].Trim(), StringComparison.OrdinalIgnoreCase) ||
                    laptop.CategoryInfo.Name.Contains(filters[0].Trim(), StringComparison.OrdinalIgnoreCase)
                );
            }
            for (int i = 0; i < result.Count - 1; i++)
            {
                Assert.True(result[i].Price <= result[i + 1].Price);
            }
        }
        [Theory]
        [InlineData("asus # gaming")]
        [InlineData("gaming # acer")]
        [InlineData("apple # Multimedia")]
        public void SearchLaptopsTest4(string searchValue)
        {
            var filters = searchValue.Split("#");
            List<Laptop> result = laptopDAL.Search(searchValue);
            Assert.True(result != null);
            foreach (var laptop in result)
            {
                Assert.True(
                    (laptop.ManufactoryInfo.Name.Contains(filters[0].Trim(), StringComparison.OrdinalIgnoreCase) &&
                    laptop.CategoryInfo.Name.Contains(filters[1].Trim(), StringComparison.OrdinalIgnoreCase)) ||
                    (laptop.ManufactoryInfo.Name.Contains(filters[1].Trim(), StringComparison.OrdinalIgnoreCase) &&
                    laptop.CategoryInfo.Name.Contains(filters[0].Trim(), StringComparison.OrdinalIgnoreCase))
                );
            }
        }

        [Theory]
        [InlineData("asus # gaming # desc")]
        [InlineData("gaming # acer # desc")]
        [InlineData("apple # Multimedia # desc")]
        public void SearchLaptopsTest5(string searchValue)
        {
            var filters = searchValue.Split("#");
            List<Laptop> result = laptopDAL.Search(searchValue);
            Assert.True(result != null);
            foreach (var laptop in result)
            {
                Assert.True(
                    (laptop.ManufactoryInfo.Name.Contains(filters[0].Trim(), StringComparison.OrdinalIgnoreCase) &&
                    laptop.CategoryInfo.Name.Contains(filters[1].Trim(), StringComparison.OrdinalIgnoreCase)) ||
                    (laptop.ManufactoryInfo.Name.Contains(filters[1].Trim(), StringComparison.OrdinalIgnoreCase) &&
                    laptop.CategoryInfo.Name.Contains(filters[0].Trim(), StringComparison.OrdinalIgnoreCase))
                );
            }
            for (int i = 0; i < result.Count - 1; i++)
            {
                Assert.True(result[i].Price >= result[i + 1].Price);
            }
        }

         [Theory]
        [InlineData("asus # gaming # asc")]
        [InlineData("gaming # acer # asc")]
        [InlineData("apple # Multimedia # asc")]
        public void SearchLaptopsTest6(string searchValue)
        {
            var filters = searchValue.Split("#");
            List<Laptop> result = laptopDAL.Search(searchValue);
            Assert.True(result != null);
            foreach (var laptop in result)
            {
                Assert.True(
                    (laptop.ManufactoryInfo.Name.Contains(filters[0].Trim(), StringComparison.OrdinalIgnoreCase) &&
                    laptop.CategoryInfo.Name.Contains(filters[1].Trim(), StringComparison.OrdinalIgnoreCase)) ||
                    (laptop.ManufactoryInfo.Name.Contains(filters[1].Trim(), StringComparison.OrdinalIgnoreCase) &&
                    laptop.CategoryInfo.Name.Contains(filters[0].Trim(), StringComparison.OrdinalIgnoreCase))
                );
            }
            for (int i = 0; i < result.Count - 1; i++)
            {
                Assert.True(result[i].Price <= result[i + 1].Price);
            }
        }
        [Theory]
        [InlineData("sdfasjhfj")]
        [InlineData("asus # dell # ")]
        [InlineData("a # # asus")]
        [InlineData("dell # mac # # desc")]
        public void SearchLaptopsTest7(string searchValue)
        {
            List<Laptop> result = laptopDAL.Search(searchValue);
            Assert.True(result == null);
        }
    }
}