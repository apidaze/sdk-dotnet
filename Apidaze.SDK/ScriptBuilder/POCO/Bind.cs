using System.Xml.Serialization;

namespace Apidaze.SDK.ScriptBuilder
{
    public class Bind
    {
        [XmlAttribute("action")] public string Action { get; set; }

        [XmlText(typeof(string))] public string Value { get; set; }
    }
}