using Newtonsoft.Json.Converters;

namespace Apidaze.SDK.Calls
{
    public class DateFormatConverter : IsoDateTimeConverter
    {
        public DateFormatConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}