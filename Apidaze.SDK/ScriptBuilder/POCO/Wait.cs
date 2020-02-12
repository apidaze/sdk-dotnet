using System.Xml.Serialization;

namespace Apidaze.SDK.ScriptBuilder.POCO
{
    /// <summary>
    /// Class Wait.
    /// </summary>
    public class Wait
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [XmlText(typeof(double))] public double Value { get; set; }

        /// <summary>
        /// Sets the duration.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Wait.</returns>
        public static Wait SetDuration(double value)
        {
            return new Wait { Value = value };
        }
    }
}