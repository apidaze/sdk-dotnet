using System.Xml.Serialization;

namespace Apidaze.SDK.ScriptBuilder.POCO
{
    /// <summary>
    ///     Class Echo.
    /// </summary>
    public class Echo
    {
        /// <summary>
        ///     Gets or sets the delay.
        /// </summary>
        /// <value>The delay.</value>
        [XmlText(typeof(double))]
        public double Delay { get; set; }

        /// <summary>
        ///     Sets the duration.
        /// </summary>
        /// <param name="delay">The delay.</param>
        /// <returns>Echo.</returns>
        public static Echo SetDuration(double delay)
        {
            return new Echo {Delay = delay};
        }
    }
}