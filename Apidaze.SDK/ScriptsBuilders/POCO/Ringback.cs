using System.Xml.Serialization;

namespace APIdaze.SDK.ScriptsBuilders.POCO
{
    public class Ringback
    {
        [XmlText(typeof(string))] public string Url { get; set; }

        public static Ringback FromFile(string url)
        {
            return new Ringback { Url = url };
        }
    }
}