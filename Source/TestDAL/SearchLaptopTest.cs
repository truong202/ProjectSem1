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
        public void Test1(string searchValue, int offset)
        {
            List<Laptop> result = laptopDAL.Search(searchValue, offset);
            Assert.True(result.Count == 0);
        }
        [Theory]
        [InlineData("Dell", 0)]
        [InlineData("10", 0)]
        [InlineData("Asus ROG Strix G15 G513QC HN015T", 0)]
        [InlineData("Gaming", 0)]
        public void Test2(string searchValue, int offset)
        {
            List<Laptop> result = laptopDAL.Search(searchValue, offset);
            Assert.False(result.Count == 0);
        }
    }
}