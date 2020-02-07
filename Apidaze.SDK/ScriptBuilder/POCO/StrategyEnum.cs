using System.Xml.Serialization;

namespace Apidaze.SDK.ScriptBuilder.POCO
{
    public enum StrategyEnum
    {
        [XmlEnum(Name = "simultaneous")] SIMULTANEOUS = 0,
        [XmlEnum(Name = "sequence")] SEQUENCE = 1
    }
}