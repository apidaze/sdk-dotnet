using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Apidaze.SDK.ScriptBuilder.POCO
{
    /// <summary>
    /// Class Speak.
    /// </summary>
    public class Speak
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        [XmlText(typeof(string))] public string Text { get; set; }

        /// <summary>
        /// Gets or sets the binds.
        /// </summary>
        /// <value>The binds.</value>
        [XmlElement("bind", typeof(Bind))]
        public List<object> Binds { get; set; }

        /// <summary>
        /// Gets or sets the language enum.
        /// </summary>
        /// <value>The language enum.</value>
        [XmlAttribute("lang")] public LangEnum LangEnum { get; set; }

        /// <summary>
        /// Gets or sets the voice.
        /// </summary>
        /// <value>The voice.</value>
        [XmlAttribute("voice")] public VoiceEnum Voice { get; set; }

        /// <summary>
        /// Gets or sets the input timeout millis.
        /// </summary>
        /// <value>The input timeout millis.</value>
        [XmlAttribute("input-timeout")] public double InputTimeoutMillis { get; set; }

        [XmlAttribute("digit-timeout")] public double DigitTimeoutMillis { get; set; }

        public Speak()
        {
            Binds = new List<object>();
        }

        /// <summary>
        /// Shoulds the serialize input timeout millis.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool ShouldSerializeInputTimeoutMillis()
        {
            return Math.Abs(InputTimeoutMillis) > 0;
        }

        public bool ShouldSerializeDigitTimeoutMillis()
        {
            return Math.Abs(DigitTimeoutMillis) > 0;
        }

        public bool ShouldSerializeLangEnum()
        {
            return LangEnum != default;
        }

        /// <summary>
        /// Shoulds the serialize voice.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool ShouldSerializeVoice()
        {
            return Voice != default;
        }

        /// <summary>
        /// Withes the text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>Speak.</returns>
        public static Speak WithText(string text)
        {
            return new Speak {Text = text};
        }
    }
}