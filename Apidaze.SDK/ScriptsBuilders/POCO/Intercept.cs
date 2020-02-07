using System;
using System.Xml.Serialization;

namespace Apidaze.SDK.ScriptsBuilders.POCO
{
    public class Intercept
    {
        [XmlText(typeof(Guid))] public Guid Uuid { get; set; }
    }
}