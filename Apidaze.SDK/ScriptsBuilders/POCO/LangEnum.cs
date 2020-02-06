using System.Xml.Serialization;

namespace Apidaze.SDK.ScriptsBuilders.POCO
{
    public enum LangEnum
    {
        [XmlEnum(Name = "en-US")] ENGLISH_US = 1,
        [XmlEnum(Name = "da-DK")] DANISH, 
        [XmlEnum(Name = "nl-NL")] DUTCH,
        [XmlEnum(Name = "it-IT")] ITALIAN,
        [XmlEnum(Name = "ja-JP")] JAPANESE,
        [XmlEnum(Name = "nb-NO")] NORWEGIAN,
        [XmlEnum(Name = "pt-BR")] PORTUGESE_BRAZIL,
        [XmlEnum(Name = "pt-PT")] PORTUGESE_PORTUGAL,
        [XmlEnum(Name = "sk-SK")] SLOVAK,
        [XmlEnum(Name = "es-ES")] SPANISH,
        [XmlEnum(Name = "sv-SE")] SWEDISH,
        [XmlEnum(Name = "uk-UA")] UKRANIAN,
        [XmlEnum(Name = "en-AU")] ENGLISH_AUSTRALIA,
        [XmlEnum(Name = "en-GB")] ENGLISH_UK,
        [XmlEnum(Name = "fr-CA")] FRENCH_CANADA,
        [XmlEnum(Name = "fr-FR")] FRENCH_FRANCE,
        [XmlEnum(Name = "de-DE")] GERMAN,
        [XmlEnum(Name = "ko-KR")] KOREAN,
        [XmlEnum(Name = "po-PL")] POLISH,
        [XmlEnum(Name = "ru-RU")] RUSSIAN,
        [XmlEnum(Name = "tr-TR")] TURKISH,
    }
}