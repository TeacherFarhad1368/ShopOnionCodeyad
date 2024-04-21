using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Application.Validations
{
    public static class Email_Mobile_Validation
    {
        public static bool IsEmail(this string email)
        {
            if (string.IsNullOrEmpty(email)) return false;
            if (email.Contains("@") == false) return false;
            return email.Contains(".");
        }
        public static bool IsMobile(this string mobile)
        {
            if (mobile.StartsWith('0') == false) return false;
            if (mobile.Length != 11) return false;
            try
            {
                var x = Convert.ToInt64(mobile);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
