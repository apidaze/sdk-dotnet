using System.Xml.Serialization;

namespace Apidaze.SDK.ScriptsBuilders.POCO
{
    public class Wait
    {
        [XmlText(typeof(double))] public double Value { get; set; }

        public static Wait SetDuration(double value)
        {
            return new Wait { Value = value };
        }
    }
}