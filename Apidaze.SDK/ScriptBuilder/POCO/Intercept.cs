using System;
using System.Xml.Serialization;

namespace Apidaze.SDK.ScriptBuilder.POCO
{
    /// <summary>
    /// Class Intercept.
    /// </summary>
    public class Intercept
    {
        /// <summary>
        /// Gets or sets the UUID.
        /// </summary>
        /// <value>The UUID.</value>
        [XmlText(typeof(Guid))] public Guid Uuid { get; set; }
    }
}