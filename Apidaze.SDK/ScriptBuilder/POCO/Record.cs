using System.Xml.Serialization;

namespace Apidaze.SDK.ScriptBuilder.POCO
{
    /// <summary>
    ///     Class Record.
    /// </summary>
    public class Record
    {
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [on answered].
        /// </summary>
        /// <value><c>true</c> if [on answered]; otherwise, <c>false</c>.</value>
        [XmlAttribute("on-answered")]
        public bool OnAnswered { get; set; } = false;

        /// <summary>
        ///     Gets or sets a value indicating whether [a leg].
        /// </summary>
        /// <value><c>true</c> if [a leg]; otherwise, <c>false</c>.</value>
        [XmlAttribute("aleg")]
        public bool ALeg { get; set; } = true;

        /// <summary>
        ///     Gets or sets a value indicating whether [b leg].
        /// </summary>
        /// <value><c>true</c> if [b leg]; otherwise, <c>false</c>.</value>
        [XmlAttribute("bleg")]
        public bool BLeg { get; set; } = true;

        /// <summary>
        ///     Serialize a OnAnswered conditionally.
        ///     The result of the method determines whether the property is serialized. If the method returns true then the
        ///     property will be serialized,
        ///     if it returns false then the property will be skipped.
        /// </summary>
        /// <returns><c>true</c> if value true <c>false</c> otherwise.</returns>
        public bool ShouldSerializeOnAnswered()
        {
            return OnAnswered;
        }

        /// <summary>
        ///     Serialize a ALeg conditionally.
        ///     The result of the method determines whether the property is serialized. If the method returns true then the
        ///     property will be serialized,
        ///     if it returns false then the property will be skipped.
        /// </summary>
        /// <returns><c>true</c> if value not true <c>false</c> otherwise.</returns>
        public bool ShouldSerializeALeg()
        {
            return !ALeg;
        }

        /// <summary>
        ///     Serialize a BLeg conditionally.
        ///     The result of the method determines whether the property is serialized. If the method returns true then the
        ///     property will be serialized,
        ///     if it returns false then the property will be skipped.
        /// </summary>
        /// <returns><c>true</c> if value not true <c>false</c> otherwise.</returns>
        public bool ShouldSerializeBLeg()
        {
            return !BLeg;
        }
    }
}