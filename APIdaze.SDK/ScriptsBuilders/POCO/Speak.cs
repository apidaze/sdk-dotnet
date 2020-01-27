﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace APIdaze.SDK.ScriptsBuilders.POCO
{
    public class Speak
    {
        [XmlText(typeof(string))] public string Text { get; set; }

        [XmlAttribute("lang")] public LangEnum LangEnum { get; set; }

        [XmlAttribute("voice")] public VoiceEnum Voice { get; set; }

        [XmlAttribute("input-timeout")] public double InputTimeoutMillis { get; set; }

        [XmlElement("bind")] public List<Bind> Binds { get; set; }

        public Speak()
        {
        }

        public bool ShouldSerializeInputTimeoutMillis()
        {
            return Math.Abs(InputTimeoutMillis) > 0;  
        }

        public bool ShouldSerializeLangEnum()
        {
            return LangEnum > 0;  
        }

        public bool ShouldSerializeVoice()
        {
            return Voice > 0; 
        }

        public static Speak WithText(string text)
        {
            return new Speak { Text = text };
        }
    }
}