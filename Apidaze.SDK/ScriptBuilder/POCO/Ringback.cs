using System.Xml.Serialization;

namespace APIdaze.SDK.ScriptsBuilders.POCO
{
    public class Ringback
    {
        public Ringback(string fromFile = "")
        {
            Url = fromFile;
        }

        [XmlText(typeof(string))] public string Url { get; set; }
    }
}