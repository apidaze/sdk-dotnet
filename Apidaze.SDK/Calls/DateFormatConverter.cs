using Newtonsoft.Json.Converters;

namespace Apidaze.SDK.Calls
{
    /// <summary>
    /// Class DateFormatConverter.
    /// Implements the <see cref="Newtonsoft.Json.Converters.IsoDateTimeConverter" />
    /// </summary>
    /// <seealso cref="Newtonsoft.Json.Converters.IsoDateTimeConverter" />
    public class DateFormatConverter : IsoDateTimeConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateFormatConverter" /> class.
        /// </summary>
        /// <param name="format">The format.</param>
        public DateFormatConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}