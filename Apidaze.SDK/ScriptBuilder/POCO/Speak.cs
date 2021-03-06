﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Apidaze.SDK.ScriptBuilder.POCO
{
    /// <summary>
    ///     Class Speak.
    /// </summary>
    public class Speak
    {
        public Speak()
        {
            Binds = new List<object>();
        }

        /// <summary>
        ///     Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        [XmlText(typeof(string))]
        public string Text { get; set; }

        /// <summary>
        ///     Gets or sets the binds.
        /// </summary>
        /// <value>The binds.</value>
        [XmlElement("bind", typeof(Bind))]
        public List<object> Binds { get; set; }

        /// <summary>
        ///     Gets or sets the language enum.
        /// </summary>
        /// <value>The language enum.</value>
        [XmlAttribute("lang")]
        public LangEnum LangEnum { get; set; }

        /// <summary>
        ///     Gets or sets the voice.
        /// </summary>
        /// <value>The voice.</value>
        [XmlAttribute("voice")]
        public VoiceEnum Voice { get; set; }

        /// <summary>
        ///     Gets or sets the input timeout millis.
        /// </summary>
        /// <value>The input timeout millis.</value>
        [XmlAttribute("input-timeout")]
        public double InputTimeoutMillis { get; set; }

        [XmlAttribute("digit-timeout")] public double DigitTimeoutMillis { get; set; }

        /// <summary>
        ///     Serialize a InputTimeoutMillis conditionally.
        ///     The result of the method determines whether the property is serialized. If the method returns true then the
        ///     property will be serialized,
        ///     if it returns false then the property will be skipped.
        /// </summary>
        /// <returns><c>true</c> if value greater than 0, <c>false</c> otherwise.</returns>
        public bool ShouldSerializeInputTimeoutMillis()
        {
            return Math.Abs(InputTimeoutMillis) > 0;
        }

        /// <summary>
        ///     Serialize a DigitTimeoutMillis conditionally.
        ///     The result of the method determines whether the property is serialized. If the method returns true then the
        ///     property will be serialized,
        ///     if it returns false then the property will be skipped.
        /// </summary>
        /// <returns><c>true</c> if value greater than 0, <c>false</c> otherwise.</returns>
        public bool ShouldSerializeDigitTimeoutMillis()
        {
            return Math.Abs(DigitTimeoutMillis) > 0;
        }

        /// <summary>
        ///     Serialize a LangEnum conditionally.
        ///     The result of the method determines whether the property is serialized. If the method returns true then the
        ///     property will be serialized,
        ///     if it returns false then the property will be skipped.
        /// </summary>
        /// <returns><c>true</c> if value not equal default <c>false</c> otherwise.</returns>
        public bool ShouldSerializeLangEnum()
        {
            return LangEnum != default;
        }

        /// <summary>
        ///     Serialize a Voice conditionally.
        ///     The result of the method determines whether the property is serialized. If the method returns true then the
        ///     property will be serialized,
        ///     if it returns false then the property will be skipped.
        /// </summary>
        /// <returns><c>true</c> if value not equal default <c>false</c> otherwise.</returns>
        public bool ShouldSerializeVoice()
        {
            return Voice != default;
        }

        /// <summary>
        ///     Withes the text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>Speak.</returns>
        public static Speak WithText(string text)
        {
            return new Speak {Text = text};
        }
    }
}