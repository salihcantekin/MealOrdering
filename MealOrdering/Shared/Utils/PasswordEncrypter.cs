using System;
using System.Collections.Generic;
using System.Text;

namespace MealOrdering.Shared.Utils
{
    public class PasswordEncrypter
    {

        public static String Encrypt(String Password)
        {
            // Kendi algoritmanızı kullanabilirsiniz.
            // Ben Base64 olarak kullanıyorum


            var plainTextBytes = Encoding.UTF8.GetBytes(Password);
            return Convert.ToBase64String(plainTextBytes);
        }


    }
}
