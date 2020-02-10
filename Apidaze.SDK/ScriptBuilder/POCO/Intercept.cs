using System;
using System.Xml.Serialization;

namespace Apidaze.SDK.ScriptBuilder.POCO
{
    public class Intercept
    {
        [XmlText(typeof(Guid))] public Guid Uuid { get; set; }
    }
}