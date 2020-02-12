using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Apidaze.SDK.ScriptBuilder;
using Apidaze.SDK.ScriptBuilder.POCO;
using Newtonsoft.Json;

namespace IvrExample
{

    /// <summary>
    /// Class ExampleUtil.
    /// </summary>
    public static class ExampleUtil
    {
        /// <summary>
        /// Gets the file contents.
        /// </summary>
        /// <param name="sampleFile">The sample file.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] GetFileContents(string sampleFile)
        {
            var asm = Assembly.GetExecutingAssembly();
            var resource = $"IvrExample.{sampleFile}";
            using var stream = asm.GetManifestResourceStream(resource);
            if (stream == null) return null;
            using var reader = new StreamReader(stream);
            return reader.BaseStream.ToByteArray();
        }

        /// <summary>
        /// Adds the forward slash.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>System.String.</returns>
        public static string AddForwardSlash(this string str)
        {
            return str + "/";
        }

        /// <summary>
        /// Converts to bytearray.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>System.Byte[].</returns>
        private static byte[] ToByteArray(this Stream stream)
        {
            using MemoryStream memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}