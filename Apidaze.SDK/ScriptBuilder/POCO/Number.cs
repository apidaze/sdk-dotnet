using System;
using System.Xml.Serialization;

namespace Apidaze.SDK.ScriptBuilder.POCO
{
    /// <summary>
    /// Class Number.
    /// Implements the <see cref="Object" />
    /// </summary>
    /// <seealso cref="Object" />
    public class Number
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> class.
        /// </summary>
        public Number()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="timeout">The timeout.</param>
        public Number(string value, double timeout = default)
        {
            Timeout = timeout;
            Value = value;
        }

        /// <summary>
        /// Gets or sets the timeout.
        /// </summary>
        /// <value>The timeout.</value>
        [XmlAttribute("timeout")] public double Timeout { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [XmlText(typeof(string))] public string Value { get; set; }

        /// <summary>
        /// Serialize a Timeout conditionally.
        /// The result of the method determines whether the property is serialized. If the method returns true then the
        /// property will be serialized,
        /// if it returns false then the property will be skipped.
        /// </summary>
        /// <returns><c>true</c> if value greater than 0, <c>false</c> otherwise.</returns>
        public bool ShouldSerializeTimeout()
        {
            return Math.Abs(Timeout) > 0;
        }
    }
}