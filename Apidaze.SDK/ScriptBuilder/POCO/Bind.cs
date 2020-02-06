using System.Xml.Serialization;

namespace APIdaze.SDK.ScriptBuilder
{
    public class Bind
    {
        [XmlAttribute("action")] public string Action { get; set; }

        [XmlText(typeof(string))] public string Value { get; set; }
    }
}