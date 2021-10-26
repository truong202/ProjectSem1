using DAL;
using Xunit;

namespace TestDAL {
    public class ChangeOrderStatusTest {
        private OrderDAL orderDAL = new OrderDAL();

        [Theory]
        [InlineData(1, 1, 5)]
        [InlineData(3, 2, 4)]
        [InlineData(4, 3, 6)]
        public void ChangeOrderStatusTest1(int status, int orderId, int staffId) {
            var result = orderDAL.ChangeStatus(status, orderId, staffId);
            Assert.True(result == true);
            var order = orderDAL.GetById(orderId);
            Assert.True(order.Status == status);
        }

        [Theory]
        [InlineData(1, 1, 100)]
        [InlineData(3, 2, 0)]
        [InlineData(4, 1000, 6)]
        public void ChangeOrderStatusTest2(int status, int orderId, int staffId) {
            var result = orderDAL.ChangeStatus(status, orderId, staffId);
            Assert.True(result == false);
        }
    }
}