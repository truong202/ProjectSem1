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
        private static Staff staff1 = new Staff
        {
            StaffId = 1,
            Username = "seller001",
            Password = "25d55ad283aa400af464c76d713c07ad",
            StaffName = "Nguyễn Văn A",
            Role = 1
        };
        private static Staff staff2 = new Staff
        {
            StaffId = 6,
            Username = "accountance003",
            Password = "25d55ad283aa400af464c76d713c07ad",
            StaffName = "Nguyễn Văn F",
            Role = 2
        };
        public static IEnumerable<object[]> SplitCountData
        {
            get
            {
                return new[]
                {
                new object[] { "seller001", "12345678", staff1 },
                new object[] { "accountance003", "12345678", staff2 },
                new object[] { "seller00fdsj", "12345678", null },
                new object[] { "seller002", "ddf12345678", null },
                new object[] { "SEller002", "12345678", null }
                };
            }
        }
        // [Theory]
        // [InlineData("seller00fdsj", "12345678", null)]
        // [InlineData("seller002", "ddf12345678", null)]
        // [InlineData("SEller002", "12345678", null)]
        [Theory, MemberData(nameof(SplitCountData))]
        public void LoginTest1(string username, string password, Staff expected)
        {
            Staff result = staffDAL.Login(new Staff { Username = username, Password = password });
            Assert.Equal(result, expected);
        }

        // [Fact]
        // public void LoginTest2()
        // {
        //     staff.Username = "seller001";
        //     staff.Password = "12345678";
        //     Staff expected = new Staff();
        //     expected.StaffId = 1;
        //     // expected.Username = "seller001";
        //     // expected.Password = "25d55ad283aa400af464c76d713c07ad";
        //     // expected.StaffName = "Nguyễn Văn A";
        //     // expected.Role = 1;
        //     Staff result = staffDAL.Login(staff);
        //     Assert.True(result.StaffId == expected.StaffId);

        // }
        // [Fact]
        // public void LoginTest3()
        // {
        //     staff.Username = "seller002";
        //     staff.Password = "12345678";
        //     Staff expected = new Staff();
        //     expected.StaffId = 2;
        //     // expected.Username = "seller002";
        //     // expected.Password = "25d55ad283aa400af464c76d713c07ad";
        //     // expected.StaffName = "Nguyễn Văn B";
        //     // expected.Role = 1;
        //     Staff result = staffDAL.Login(staff);
        //     Assert.True(result.StaffId == expected.StaffId);
        // }
    }
}
