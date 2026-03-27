using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DVLD___Driving_License_Management.Global_Classes
{
    internal class clsValidation
    {
        public static bool validateEmail(string email)
        {
            var pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            var regex = new Regex(pattern);
            return regex.IsMatch(email);
        }

        public static bool validatePhone(string phone)
        {
            var pattern = @"^\d{10}$";

            var regex = new Regex(pattern);
            return regex.IsMatch(phone);
        }

        public static bool ValidateInteger(string Number)
        {
            var pattern = @"^[0-9]*$";

            var regex = new Regex(pattern);

            return regex.IsMatch(Number);
        }

        public static bool ValidateFloat(string Nummber)
        {
            var pattern = @"^[0-9]+(?:\.[0-9]+)?$";

            var regex = new Regex(pattern);
            return regex.IsMatch(Nummber);
        }
        public static bool IsNumber(string Number)
        {
            return ValidateInteger(Number) || ValidateFloat(Number);
        }
    }
}
