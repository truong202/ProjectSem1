using DAL;
using Persistance;
using Xunit;

namespace TestDAL {
    public class GetCustomerByPhoneTest {
        CustomerDAL cusDAL = new CustomerDAL();

        [Theory]
        [InlineData("0836984311")]
        [InlineData("0836984312")]
        [InlineData("0836984313")]

        public void GetCustomerByPhoneTest1(string phone) {
            Customer result = cusDAL.GetByPhone(phone);
            Assert.True(result.Phone == phone);
        }

        [Theory]
        [InlineData("0836984310")]
        [InlineData("")]
        public void GetCustomerByPhoneTest2(string phone) {
            Customer result = cusDAL.GetByPhone(phone);
            Assert.True(result == null);
        }
    }
}