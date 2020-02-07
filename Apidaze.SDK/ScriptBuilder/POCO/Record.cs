using System.Xml.Serialization;

namespace APIdaze.SDK.ScriptsBuilders.POCO
{
    public class Record
    {
        [XmlAttribute("name")] public string Name { get; set; }

        [XmlAttribute("on-answered")] public bool OnAnswered { get; set; }

        [XmlAttribute("aleg")] public bool ALeg { get; set; }

        [XmlAttribute("bleg")] public bool BLeg { get; set; }
    }
}