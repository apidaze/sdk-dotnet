using System.Collections.Generic;
using System.Xml.Serialization;

namespace APIdaze.SDK.ScriptBuilder
{
    public class Playback
    {
        [XmlAttribute("file")] public string File { get; set; }

        [XmlElement("bind")] public List<Bind> Binds { get; set; }

        [XmlAttribute("input-timeout")] public double InputTimeoutMillis { get; set; }

        public static Playback FromFile(string file)
        {
            return new Playback {File = file};
        }
    }
}