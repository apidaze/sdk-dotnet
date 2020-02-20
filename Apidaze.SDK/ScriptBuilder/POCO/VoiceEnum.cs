using System.Xml.Serialization;

namespace Apidaze.SDK.ScriptBuilder.POCO
{
    /// <summary>
    ///     Enum VoiceEnum
    /// </summary>
    public enum VoiceEnum
    {
        [XmlEnum] NONE = 0,
        [XmlEnum(Name = "female-a")] FEMALE_A,
        [XmlEnum(Name = "female-b")] FEMALE_B,

        /// <summary>
        ///     The female c
        /// </summary>
        [XmlEnum(Name = "female-c")] FEMALE_C,

        /// <summary>
        ///     The male a
        /// </summary>
        [XmlEnum(Name = "male-a")] MALE_A,

        /// <summary>
        ///     The male b
        /// </summary>
        [XmlEnum(Name = "male-b")] MALE_B,

        /// <summary>
        ///     The male c
        /// </summary>
        [XmlEnum(Name = "male-c")] MALE_C
    }
}