using System;
using System.Text.RegularExpressions;

namespace Persistance
{
    public class Customer
    {
        public int? CustomerId { set; get; }
        public string CustomerName { set; get; }
        public string Phone { set; get; }
        public string Address { set; get; }
        public static void CheckPhone(string phone)
        {
            if (phone.Trim() == "")
                throw new Exception("Phone cannot be empty!");
            if (!Regex.IsMatch(phone, @"^([09|03|07|08|05|]{2})+([0-9]{8})$"))
            {
                throw new Exception("Invalid phone numbers!");
            }
        }
        public static void CheckName(string name)
        {
            if (name.Trim() == "")
                throw new Exception("Customer name cannot be empty!");
            if (!Regex.IsMatch(name, @"^([A-Za-z - ÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂưăạ
                                    ảấầẩẫậắằẳẵặẹẻẽềềểỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ])+$"))
                throw new Exception("Invalid name!");
        }
    }
}
