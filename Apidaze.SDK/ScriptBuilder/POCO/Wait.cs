using System.Xml.Serialization;

namespace APIdaze.SDK.ScriptsBuilders.POCO
{
    public class Wait
    {
        [XmlText(typeof(double))] public double Value { get; set; }
    }
}