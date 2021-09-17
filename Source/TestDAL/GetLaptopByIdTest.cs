using System;
using Xunit;
using DAL;
using Persistance;
using System.Collections.Generic;


namespace TestDAL
{
    public class GetLaptopByIdTest
    {
        private LaptopDAL laptopDAL = new LaptopDAL();
        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(27)]
        public void GetLaptopByIdTest1(int laptopId)
        {
            Laptop result = laptopDAL.GetById(laptopId);
            Assert.True(result != null);
            Assert.True(result.LaptopId == laptopId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(30)]
        public void GetLaptopByIdTest2(int laptopId)
        {
            Laptop result = laptopDAL.GetById(laptopId);
            Assert.True(result == null);
        }
    }
}