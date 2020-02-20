using System.Xml.Serialization;

namespace Apidaze.SDK.ScriptBuilder.POCO
{
    /// <summary>
    ///     Class Ringback.
    /// </summary>
    public class Ringback
    {
        /// <summary>
        ///     Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        [XmlText(typeof(string))]
        public string Url { get; set; }

        /// <summary>
        ///     Froms the file.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>Ringback.</returns>
        public static Ringback FromFile(string url)
        {
            return new Ringback {Url = url};
        }
    }
}