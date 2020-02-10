using System.Xml.Serialization;

namespace Apidaze.SDK.ScriptBuilder.POCO
{
    public class Echo
    {
        [XmlText(typeof(double))] public double Delay { get; set; }

        public static Echo SetDuration(double delay)
        {
            return new Echo { Delay = delay };
        }
    }
}