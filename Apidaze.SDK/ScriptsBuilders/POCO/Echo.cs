using System.Xml.Serialization;

namespace APIdaze.SDK.ScriptsBuilders.POCO
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