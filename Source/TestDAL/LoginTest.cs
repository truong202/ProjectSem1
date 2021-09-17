using System;
using Xunit;
using DAL;
using Persistance;
using System.Collections.Generic;

namespace TestDAL
{
    public class LoginTest
    {
        private StaffDAL staffDAL = new StaffDAL();

        [Theory]
        [InlineData("seller001", "12345678")]
        [InlineData("accountance003", "12345678")]
        public void LoginTest1(string userName, string password)
        {
            Staff result = staffDAL.Login(new Staff { Username = userName, Password = password });
            Assert.True(result != null);
            Assert.True(result.Username == userName);
        }

        [Theory]
        [InlineData("seller00fdsj", "12345678")]
        [InlineData("seller002", "ddf12345678")]
        [InlineData("SEller002", "12345678")]
        public void TestLogin2(string userName, string password)
        {
            Staff result = staffDAL.Login(new Staff { Username = userName, Password = password });
            Assert.True(result == null);
        }
    }
}
