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
        [InlineData("fhdasfj", 0)]
        [InlineData("87dah", 0)]
        public void SearchLaptopsTest1(string searchValue, int offset)
        {
            List<Laptop> result = laptopDAL.Search(searchValue, offset);
            Assert.True(result == null);
        }

        [Fact]
        public void SearchLaptopsTest2()
        {
            List<Laptop> result = laptopDAL.Search("Dell", 0);
            Assert.True(result[0].LaptopId == 23);
            Assert.True(result[1].LaptopId == 24);
        }

        [Fact]
        public void SearchLaptopsTest3()
        {
            List<Laptop> result = laptopDAL.Search("10", 0);
            Assert.True(result[0].LaptopId == 10);
            Assert.True(result[1].LaptopId == 15);
            Assert.True(result[2].LaptopId == 16);
            Assert.True(result[3].LaptopId == 23);

        }

        [Fact]
        public void SearchLaptopsTest4()
        {
            List<Laptop> result = laptopDAL.Search("Asus ROG Strix G15 G513QC HN015T", 0);
            Assert.True(result[0].LaptopId == 3);
        }

        [Fact]
        public void SearchLaptopsTest5()
        {
            List<Laptop> result = laptopDAL.Search("Office", 0);
            Assert.True(result[0].LaptopId == 5);
            Assert.True(result[1].LaptopId == 13);
            Assert.True(result[2].LaptopId == 18);
            Assert.True(result[3].LaptopId == 24);

        }

        [Theory]
        [InlineData("Dell", 0)]
        [InlineData("10", 0)]
        [InlineData("Asus ROG Strix G15 G513QC HN015T", 0)]
        [InlineData("Gaming", 0)]
        public void Test2(string searchValue, int offset)
        {
            List<Laptop> result = laptopDAL.Search(searchValue, offset);
            Assert.True(result != null);
        }
    }
}