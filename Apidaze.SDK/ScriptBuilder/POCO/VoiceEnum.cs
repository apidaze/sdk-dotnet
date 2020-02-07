using System.Xml.Serialization;

namespace Apidaze.SDK.ScriptBuilder.POCO
{
    public enum VoiceEnum
    {
        [XmlEnum(Name = "female-a")] FEMALE_A = 1,
        [XmlEnum(Name = "female-b")] FEMALE_B,
        [XmlEnum(Name = "female-c")] FEMALE_C,
        [XmlEnum(Name = "male-a")] MALE_A,
        [XmlEnum(Name = "male-b")] MALE_B,
        [XmlEnum(Name = "male-c")] MALE_C
    }
}