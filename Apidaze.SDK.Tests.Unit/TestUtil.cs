using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Apidaze.SDK.Base;

namespace Apidaze.SDK.Tests.Unit
{
    /// <summary>
    /// Class TestUtil.
    /// </summary>
    public static class TestUtil
    {
        /// <summary>
        /// The API key
        /// </summary>
        private static readonly string API_KEY = "some-api-key";
        /// <summary>
        /// The API secret
        /// </summary>
        private static readonly string API_SECRET = "some-api-secret";
        /// <summary>
        /// The credentials for test
        /// </summary>
        public static readonly Credentials CredentialsForTest = new Credentials(API_KEY, API_SECRET);

        /// <summary>
        /// Removes the white spaces.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>System.String.</returns>
        public static string RemoveWhiteSpaces(this string input)
        {
            return Regex.Replace(input, @"\s+", "");
        }

        /// <summary>
        /// Gets the file contents.
        /// </summary>
        /// <param name="sampleFile">The sample file.</param>
        /// <returns>System.String.</returns>
        public static string GetFileContents(string sampleFile)
        {
            var asm = Assembly.GetExecutingAssembly();
            var resource = $"Apidaze.SDK.Tests.Unit.ScriptBuilder.TestScripts.{sampleFile}";
            using (var stream = asm.GetManifestResourceStream(resource))
            {
                if (stream != null)
                {
                    var reader = new StreamReader(stream);
                    return reader.ReadToEnd();
                }
            }
            return string.Empty;
        }
    }
}