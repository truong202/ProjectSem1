using System;
using System.Text.RegularExpressions;

namespace Utilities {
    public static class Utility {
        public static void CheckName(string name) {
            if (!Regex.IsMatch(name, @"^([A-Za-z - ÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂưăạ
                                    ảấầẩẫậắằẳẵặẹẻẽềềểỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ])+$"))
                throw new Exception("Invalid name!");
            if (name.Trim() == "")
                throw new Exception("Name cannot be empty!");
        }
        public static void CheckPhone(string phone) {
            if (!Regex.IsMatch(phone, @"^([09|03|07|08|05|]{2})+([0-9]{8})$")) {
                throw new Exception("Invalid phone numbers!");
            }
        }
        public static void CheckUsername(string username) {
            if (username == null || username.Trim() == "")
                throw new Exception("Password cannot be empty!");
            if (!Regex.IsMatch(username, @"^([A-Za-z-0-9])+$"))
                throw new Exception("Only letters (a-Z), numbers (0-9) are allowed!");
            if (username.Length < 6)
                throw new Exception("Username must be at least 6 characters long!");
        }
        public static void CheckPassword(string password) {
            if (password == null || password.Trim() == "")
                throw new Exception("Password cannot be empty!");
            if (!Regex.IsMatch(password, @"^([A-Za-z0-9])+$"))
                throw new Exception("Only letters (a-Z), numbers (0-9) are allowed!");
            if (password.Length < 8)
                throw new Exception("Password must be at least 8 characters long!");
        }
        public static string Standardize(string value) {
            value = value.Trim();
            if (value.Length == 0) return "";
            string CapitalizeLetter = @"ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴÉÈẸẺẼÊẾỀỆỂỄÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠÚÙỤỦŨƯỨỪỰỬỮÍÌỊỈĨĐÝỲỴỶỸ";
            string LowercaseLetter = @"áàạảãâấầậẩẫăắằặẳẵéèẹẻẽêếềệểễóòọỏõôốồộổỗơớờợởỡúùụủũưứừựửữíìịỉĩđýỳỵỷỹ";
            int i = 0;
            while ((i = value.IndexOf("  ", i)) != -1) value = value.Remove(i, 1);
            char[] arr = value.ToCharArray();
            for (int index = 1; index < value.Length; index++) {
                if (arr[index - 1] == ' ') {
                    if (Char.IsLower(arr[index])) arr[index] = Char.ToUpper(arr[index]);
                    else {
                        int found = LowercaseLetter.IndexOf(arr[index]);
                        if (found != -1) arr[index] = CapitalizeLetter[found];
                    }
                }
                else if (arr[index - 1] != ' ') {
                    if (char.IsUpper(arr[index])) arr[index] = char.ToLower(arr[index]);
                    else {
                        int found = CapitalizeLetter.IndexOf(arr[index]);
                        if (found != -1) arr[index] = LowercaseLetter[found];
                    }
                }
            }
            if (Char.IsLower(arr[0])) arr[0] = Char.ToUpper(arr[0]);
            else {
                int found = LowercaseLetter.IndexOf(arr[0]);
                if (found != -1) arr[0] = CapitalizeLetter[found];
            }
            value = new string(arr);
            return value;
        }
    }
}