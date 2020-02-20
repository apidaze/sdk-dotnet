using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Apidaze.SDK.ScriptBuilder.POCO
{
    /// <summary>
    ///     Class Dial.
    /// </summary>
    public class Dial
    {
        /// <summary>
        ///     Gets or sets the number.
        /// </summary>
        /// <value>The number.</value>
        [XmlElement("number")]
        public List<Number> Number { get; set; }


        /// <summary>
        ///     Gets or sets the sipaccount.
        /// </summary>
        /// <value>The sipaccount.</value>
        [XmlElement("sipaccount")]
        public List<SipAccount> Sipaccount { get; set; }


        /// <summary>
        ///     Gets or sets the sip URI.
        /// </summary>
        /// <value>The sip URI.</value>
        [XmlElement("sipuri")]
        public List<SipUri> SipUri { get; set; }

        /// <summary>
        ///     Gets or sets the timeout.
        /// </summary>
        /// <value>The timeout.</value>
        [XmlAttribute("timeout")]
        public double Timeout { get; set; }

        /// <summary>
        ///     Gets or sets the maximum duration of the call.
        /// </summary>
        /// <value>The maximum duration of the call.</value>
        [XmlAttribute("max-call-duration")]
        public double MaxCallDuration { get; set; }

        /// <summary>
        ///     Gets or sets the strategy.
        /// </summary>
        /// <value>The strategy.</value>
        [XmlAttribute("strategy")]
        public StrategyEnum Strategy { get; set; }

        /// <summary>
        ///     Gets or sets the action.
        /// </summary>
        /// <value>The action.</value>
        [XmlAttribute("action")]
        public string Action { get; set; }

        /// <summary>
        ///     Gets or sets the answer URL.
        /// </summary>
        /// <value>The answer URL.</value>
        [XmlAttribute("answer-url")]
        public string AnswerUrl { get; set; }

        /// <summary>
        ///     Gets or sets the caller hangup URL.
        /// </summary>
        /// <value>The caller hangup URL.</value>
        [XmlAttribute("caller-hangup-url")]
        public string CallerHangupUrl { get; set; }

        /// <summary>
        ///     Serialize a Timeout conditionally.
        ///     The result of the method determines whether the property is serialized. If the method returns true then the
        ///     property will be serialized,
        ///     if it returns false then the property will be skipped.
        /// </summary>
        /// <returns><c>true</c> if value greater than 0, <c>false</c> otherwise.</returns>
        public bool ShouldSerializeTimeout()
        {
            return Math.Abs(Timeout) > 0;
        }

        /// <summary>
        ///     Serialize a MaxCallDuration conditionally.
        ///     The result of the method determines whether the property is serialized. If the method returns true then the
        ///     property will be serialized,
        ///     if it returns false then the property will be skipped.
        /// </summary>
        /// <returns><c>true</c> if value greater than 0, <c>false</c> otherwise.</returns>
        public bool ShouldSerializeMaxCallDuration()
        {
            return Math.Abs(MaxCallDuration) > 0;
        }

        /// <summary>
        ///     Serialize a Strategy conditionally.
        ///     The result of the method determines whether the property is serialized. If the method returns true then the
        ///     property will be serialized,
        ///     if it returns false then the property will be skipped.
        /// </summary>
        /// <returns><c>true</c> if value not equal default, <c>false</c> otherwise.</returns>
        public bool ShouldSerializeStrategy()
        {
            return Strategy != default;
        }
    }
}