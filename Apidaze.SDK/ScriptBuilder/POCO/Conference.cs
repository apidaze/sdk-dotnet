using System.Xml.Serialization;

namespace Apidaze.SDK.ScriptBuilder.POCO
{
    /// <summary>
    ///     Class Conference.
    /// </summary>
    public class Conference
    {
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [XmlText(typeof(string))]
        public string Name { get; set; }
    }
}