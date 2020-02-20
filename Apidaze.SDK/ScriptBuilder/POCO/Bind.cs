using System.Xml.Serialization;

namespace Apidaze.SDK.ScriptBuilder.POCO
{
    /// <summary>
    ///     Class Bind.
    /// </summary>
    public class Bind
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Bind" /> class.
        /// </summary>
        public Bind()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Bind" /> class.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="value">The value.</param>
        public Bind(string action, string value)
        {
            Action = action;
            Value = value;
        }

        /// <summary>
        ///     Gets or sets the action.
        /// </summary>
        /// <value>The action.</value>
        [XmlAttribute("action")]
        public string Action { get; set; }

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [XmlText(typeof(string))]
        public string Value { get; set; }
    }
}