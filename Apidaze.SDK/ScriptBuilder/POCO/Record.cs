using System.ComponentModel;
using System.Xml.Serialization;

namespace Apidaze.SDK.ScriptBuilder.POCO
{
    /// <summary>
    /// Class Record.
    /// </summary>
    public class Record
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [XmlAttribute("name")] public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [on answered].
        /// </summary>
        /// <value><c>true</c> if [on answered]; otherwise, <c>false</c>.</value>
        [XmlAttribute("on-answered")] public bool OnAnswered { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether [a leg].
        /// </summary>
        /// <value><c>true</c> if [a leg]; otherwise, <c>false</c>.</value>
        [XmlAttribute("aleg")] public bool ALeg { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether [b leg].
        /// </summary>
        /// <value><c>true</c> if [b leg]; otherwise, <c>false</c>.</value>
        [XmlAttribute("bleg")] public bool BLeg { get; set; } = true;

        /// <summary>
        /// Shoulds the serialize on answered.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool ShouldSerializeOnAnswered()
        {
            return OnAnswered;
        }

        /// <summary>
        /// Shoulds the serialize a leg.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool ShouldSerializeALeg()
        {
            return !ALeg;
        }

        /// <summary>
        /// Shoulds the serialize b leg.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool ShouldSerializeBLeg()
        {
            return !BLeg;
        }
    }
}