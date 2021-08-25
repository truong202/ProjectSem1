using System;
using Xunit;
using DAL;
using Persistance;


namespace TestDAL
{
    public class StaffLoginTest
    {
        private StaffDAL staffDAL = new StaffDAL();
        private Staff staff = new Staff();
        [Theory]
        [InlineData("seller00fdsj", "12345678", null)]
        [InlineData("seller002", "ddf12345678", null)]
        [InlineData("SEller002", "12345678", null)]
        public void LoginTest1(string username, string password, Staff expected)
        {
            staff.Username = username;
            staff.Password = password;
            Staff result = staffDAL.Login(staff);
            Assert.True(result == expected);
        }

        // [Theory]
        // [InlineData("seller001", "12345678")]
        // [InlineData("seller002", "12345678")]
        // public void LoginTest2(string username, string password)
        // {
        //     staff.Username = username;
        //     staff.Password = password;
        //     Staff result = staffDAL.Login(staff);
        //     Assert.False(result == null);
        // }
        [Fact]
        public void LoginTest2()
        {
            staff.Username = "seller001";
            staff.Password = "12345678";
            Staff expected = new Staff();
            expected.StaffId = 1;
            // expected.Username = "seller001";
            // expected.Password = "25d55ad283aa400af464c76d713c07ad";
            // expected.StaffName = "Nguyễn Văn A";
            // expected.Role = 1;
            Staff result = staffDAL.Login(staff);
            Assert.True(result.StaffId == expected.StaffId);

        }
        [Fact]
        public void LoginTest3()
        {
            staff.Username = "seller002";
            staff.Password = "12345678";
            Staff expected = new Staff();
            expected.StaffId = 2;
            // expected.Username = "seller002";
            // expected.Password = "25d55ad283aa400af464c76d713c07ad";
            // expected.StaffName = "Nguyễn Văn B";
            // expected.Role = 1;
            Staff result = staffDAL.Login(staff);
            Assert.True(result.StaffId == expected.StaffId);
        }
    }
}
