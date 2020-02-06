using System.Text.RegularExpressions;

namespace Apidaze.SDK.Messages
{
    public class PhoneNumber
    {
        private const string NumberPattern = "^([1-9][0-9]+)$";

        private readonly string _number;

        public PhoneNumber(string number)
        {
            _number = IsNumber(number);
        }

        private static string IsNumber(string number)
        {
            var regNumber = new Regex(NumberPattern);
            if (regNumber.IsMatch(number))
                return number;
            throw new InvalidPhoneNumberException(number);
        }

        public override string ToString()
        {
            return _number;
        }
    }
}