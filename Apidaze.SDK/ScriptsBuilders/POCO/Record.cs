using System.ComponentModel;
using System.Xml.Serialization;

namespace Apidaze.SDK.ScriptsBuilders.POCO
{
    public class Record
    {
        [XmlAttribute("name")] public string Name { get; set; }

        [XmlAttribute("on-answered")] public bool OnAnswered { get; set; } = false;

        [XmlAttribute("aleg")] public bool ALeg { get; set; } = true;

        [XmlAttribute("bleg")] public bool BLeg { get; set; } = true;

        public bool ShouldSerializeOnAnswered()
        {
            return OnAnswered;
        }

        public bool ShouldSerializeALeg()
        {
            return !ALeg;
        }

        public bool ShouldSerializeBLeg()
        {
            return !BLeg;
        }
    }
}