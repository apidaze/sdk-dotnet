using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace APIdaze.SDK.Messages
{
    public class PhoneNumber
    {
        private static readonly string NumberPattern = "@^([1-9][0-9]+)$";

        [JsonProperty] private string number;

        public PhoneNumber(string number)
        {
            this.number = number;
        }

        public static PhoneNumber IsNumber(string number)
        {
            var regNumber = new Regex(NumberPattern);
            if (regNumber.IsMatch(number))
                return new PhoneNumber(number);
            return null;
        }
    }
}