using System.Xml.Serialization;

namespace Apidaze.SDK.ScriptBuilder.POCO
{
    /// <summary>
    /// Enum StrategyEnum
    /// </summary>
    public enum StrategyEnum
    {
        /// <summary>
        /// The none
        /// </summary>
        [XmlEnum] NONE = 0,
        /// <summary>
        /// The simultaneous
        /// </summary>
        [XmlEnum(Name = "simultaneous")] SIMULTANEOUS,
        /// <summary>
        /// The sequence
        /// </summary>
        [XmlEnum(Name = "sequence")] SEQUENCE
    }
}