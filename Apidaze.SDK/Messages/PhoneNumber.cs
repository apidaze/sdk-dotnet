using System.Text.RegularExpressions;
using Apidaze.SDK.Exception;

namespace Apidaze.SDK.Messages
{
    /// <summary>
    /// Class PhoneNumber.
    /// </summary>
    public class PhoneNumber
    {
        /// <summary>
        /// The number pattern
        /// </summary>
        private const string NumberPattern = "^([1-9][0-9]+)$";

        /// <summary>
        /// The number
        /// </summary>
        private readonly string _number;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneNumber"/> class.
        /// </summary>
        /// <param name="number">The number.</param>
        public PhoneNumber(string number)
        {
            _number = IsNumber(number);
        }

        /// <summary>
        /// Determines whether the specified number is number.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="APIdaze.SDK.Exception.InvalidPhoneNumberException"></exception>
        private static string IsNumber(string number)
        {
            var regNumber = new Regex(NumberPattern);
            if (regNumber.IsMatch(number))
                return number;
            throw new InvalidPhoneNumberException(number);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return _number;
        }
    }
}