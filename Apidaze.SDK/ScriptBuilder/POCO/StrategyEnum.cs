using System.Xml.Serialization;

namespace Apidaze.SDK.ScriptBuilder.POCO
{
    public enum StrategyEnum
    {
        [XmlEnum] NONE = 0,
        [XmlEnum(Name = "simultaneous")] SIMULTANEOUS,
        [XmlEnum(Name = "sequence")] SEQUENCE
    }
}