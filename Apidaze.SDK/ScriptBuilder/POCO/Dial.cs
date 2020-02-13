using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Apidaze.SDK.ScriptBuilder.POCO
{
    public  class Dial
    {
        [XmlElement("number")] public List<Number> Number { get; set; }

        [XmlElement("sipaccount")] public string Sipaccount { get; set; }

        [XmlElement("sipuri")] public string SipUri { get; set; }

        [XmlAttribute("timeout")] public double Timeout { get; set; }

        [XmlAttribute("max-call-duration")] public double MaxCallDuration { get; set; }
        [XmlAttribute("strategy")] public StrategyEnum Strategy { get; set; }

        [XmlAttribute("action")] public string Action { get; set; }

        [XmlAttribute("answer-url")] public string AnswerUrl { get; set; }

        [XmlAttribute("caller-hangup-url")] public string CallerHangupUrl { get; set; }

        public bool ShouldSerializeTimeout()
        {
            return Math.Abs(Timeout) > 0;
        }

        public bool ShouldSerializeMaxCallDuration()
        {
            return Math.Abs(MaxCallDuration) > 0;
        }

        public bool ShouldSerializeStrategy()
        {
            return Strategy != default;
        }
    }

    public class Number
    {
        [XmlAttribute("timeout")] public string Timeout { get; set; }

        [XmlText(typeof(string))] public string Value { get; set; }

        public Number()
        {
        }

        public Number(string timeout, string value)
        {
            Timeout = timeout;
            Value = value;
        }
    }
}
