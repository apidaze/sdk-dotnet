using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Apidaze.SDK.ScriptBuilder.POCO
{
    public class Conference
    {
        [XmlText(typeof(string))] public string Name { get; set; }
    }
}
