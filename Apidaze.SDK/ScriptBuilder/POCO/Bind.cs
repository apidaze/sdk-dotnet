using System.Xml.Serialization;

namespace Apidaze.SDK.ScriptBuilder.POCO
{
    public class Bind
    {
        [XmlAttribute("action")] public string Action { get; set; }

        [XmlText(typeof(string))] public string Value { get; set; }

        public Bind()
        {

        }
        public Bind(string action, string value)
        {
            Action = action;
            Value = value;
        }
    }
}