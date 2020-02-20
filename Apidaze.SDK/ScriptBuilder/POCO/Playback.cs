using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Apidaze.SDK.ScriptBuilder.POCO
{
    /// <summary>
    ///     Class Playback.
    /// </summary>
    public class Playback
    {
        /// <summary>
        ///     Gets or sets the input timeout millis.
        /// </summary>
        /// <value>The input timeout millis.</value>
        [XmlAttribute("input-timeout")]
        public double InputTimeoutMillis { get; set; }

        /// <summary>
        ///     Gets or sets the file.
        /// </summary>
        /// <value>The file.</value>
        [XmlAttribute("file")]
        public string File { get; set; }

        /// <summary>
        ///     Gets or sets the binds.
        /// </summary>
        /// <value>The binds.</value>
        [XmlElement("bind")]
        public List<Bind> Binds { get; set; }

        /// <summary>
        ///     Froms the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>Playback.</returns>
        public static Playback FromFile(string file)
        {
            return new Playback {File = file};
        }

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
    }
}