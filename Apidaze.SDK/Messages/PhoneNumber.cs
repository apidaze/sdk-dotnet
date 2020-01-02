using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Apidaze.SDK.Messages
{
    public class PhoneNumber
    {
        private static readonly string NumberPattern = "@^([1-9][0-9]+)$";

        [JsonProperty]
        string number;

        public PhoneNumber(string number)
        {
            this.number = number;
        }

        public static PhoneNumber IsNumber(string number)
        {
            Regex regNumber = new Regex(NumberPattern);
            if (regNumber.IsMatch(number))
            {
                return new PhoneNumber(number);
            } else
            {
                //todo
                return null;
            }
        }
    }
}