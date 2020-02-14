using System;
using System.Xml.Serialization;

namespace Apidaze.SDK.ScriptBuilder.POCO
{
    public class SipAccount
    {
        [XmlAttribute("timeout")] public double Timeout { get; set; }

        [XmlText(typeof(string))] public string Value { get; set; }

        public SipAccount()
        {
        }

        public SipAccount(string value, double timeout = default)
        {
            Timeout = timeout;
            Value = value;
        }

        /// <summary>
        /// Serialize a Timeout conditionally.
        /// The result of the method determines whether the property is serialized. If the method returns true then the property will be serialized,
        /// if it returns false then the property will be skipped.
        /// </summary>
        /// <returns><c>true</c> if value greater than 0, <c>false</c> otherwise.</returns>
        public bool ShouldSerializeTimeout()
        {
            return Math.Abs(Timeout) > 0;
        }
    }
}