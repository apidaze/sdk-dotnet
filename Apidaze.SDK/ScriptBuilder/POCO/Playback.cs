using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Apidaze.SDK.ScriptBuilder.POCO
{
    public class Playback
    {
        [XmlAttribute("input-timeout")] public double InputTimeoutMillis { get; set; }
        [XmlAttribute("file")] public string File { get; set; }

        [XmlElement("bind")] public List<Bind> Binds { get; set; }

        public static Playback FromFile(string file)
        {
            return new Playback {File = file};
        }

        public bool ShouldSerializeInputTimeoutMillis()
        {
            return Math.Abs(InputTimeoutMillis) > 0; //Bool EXP
        }
    }
}