using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Apidaze.SDK.ScriptBuilder.POCO;

namespace Apidaze.SDK.ScriptBuilder
{
    /// <summary>
    ///     Class ApidazeScript.
    /// </summary>
    [XmlRoot(ElementName = "document")]
    public class ApidazeScript
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ApidazeScript" /> class.
        /// </summary>
        public ApidazeScript()
        {
            Nodes = new List<object>();
        }

        /// <summary>
        ///     Gets or sets the nodes.
        /// </summary>
        /// <value>The nodes.</value>
        [XmlArrayItem(typeof(Conference), ElementName = "conference")]
        [XmlArrayItem(typeof(Echo), ElementName = "echo")]
        [XmlArrayItem(typeof(Intercept), ElementName = "intercept")]
        [XmlArrayItem(typeof(Record), ElementName = "record")]
        [XmlArrayItem(typeof(Ringback), ElementName = "ringback")]
        [XmlArrayItem(typeof(Speak), ElementName = "speak")]
        [XmlArrayItem(typeof(Wait), ElementName = "wait")]
        [XmlArrayItem(typeof(Answer), ElementName = "answer")]
        [XmlArrayItem(typeof(Hangup), ElementName = "hangup")]
        [XmlArrayItem(typeof(Playback), ElementName = "playback")]
        [XmlArrayItem(typeof(Bind), ElementName = "bind")]
        [XmlArrayItem(typeof(Dial), ElementName = "dial")]
        [XmlArray("work")]
        public List<object> Nodes { get; set; }

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns>ApidazeScript.</returns>
        public static ApidazeScript Build()
        {
            return new ApidazeScript();
        }

        /// <summary>
        ///     Adds the node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>ApidazeScript.</returns>
        public ApidazeScript AddNode(object node)
        {
            Nodes.Add(node);
            return this;
        }

        /// <summary>
        ///     Converts to xml.
        /// </summary>
        /// <param name="withFormatting">if set to <c>true</c> [with formatting].</param>
        /// <param name="omitXmlDeclaration">if set to <c>true</c> [omit XML declaration].</param>
        /// <returns>System.String.</returns>
        public string ToXml(bool withFormatting = true, bool omitXmlDeclaration = false)
        {
            var listOfType = new List<Type>
            {
                typeof(Hangup), typeof(Answer),
                typeof(Playback), typeof(Bind), typeof(Dial), typeof(Conference),
                typeof(Echo), typeof(Intercept), typeof(Record), typeof(Ringback),
                typeof(Speak), typeof(Wait)
            };

            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = omitXmlDeclaration,
                Encoding = new UTF8Encoding(true),
                ConformanceLevel = ConformanceLevel.Document,
                CloseOutput = false,
                Indent = withFormatting,
                NewLineHandling = NewLineHandling.Replace
            };

            var xns = new XmlSerializerNamespaces();
            xns.Add(string.Empty, string.Empty);
            var serializer = new XmlSerializer(typeof(ApidazeScript), listOfType.ToArray());

            using var stringWriter = new UTF8StringWriter();
            using var xmlWriter = XmlWriter.Create(stringWriter, settings);
            serializer.Serialize(xmlWriter, this, xns);
            return stringWriter.ToString().Replace(" />", "/>");
        }

        /// <summary>
        ///     Class UTF8StringWriter.
        ///     Implements the <see cref="System.IO.StringWriter" />
        /// </summary>
        /// <seealso cref="System.IO.StringWriter" />
        private class UTF8StringWriter : StringWriter
        {
            /// <summary>
            ///     Gets the encoding.
            /// </summary>
            /// <value>The encoding.</value>
            public override Encoding Encoding => Encoding.UTF8;
        }
    }
}