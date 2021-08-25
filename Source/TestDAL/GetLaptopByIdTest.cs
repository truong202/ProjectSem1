using System;
using Xunit;
using Persistance;
using DAL;

namespace TestDAL
{
    public class GetLaptopByIdTest
    {
        private LaptopDAL laptopDAL = new LaptopDAL();
        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void Test1(int laptopId)
        {
            Laptop result = laptopDAL.GetById(laptopId);
            Assert.False(result == null);
        }

        [Theory]
        [InlineData(1000)]
        [InlineData(0)]
        public void Test2(int laptopId)
        {
            Laptop result = laptopDAL.GetById(laptopId);
            Assert.True(result == null);
        }
    }
}