using Newtonsoft.Json.Converters;

namespace APIdaze.SDK.Calls
{
    public class DateFormatConverter : IsoDateTimeConverter
    {
        public DateFormatConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}