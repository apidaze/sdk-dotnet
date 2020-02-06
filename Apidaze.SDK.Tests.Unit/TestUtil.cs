using Apidaze.SDK.Base;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Apidaze.SDK.Tests.Unit
{
    public static class TestUtil
    {
        private static readonly string API_KEY = "some-api-key";
        private static readonly string API_SECRET = "some-api-secret";
        public static readonly Credentials CredentialsForTest = new Credentials(API_KEY, API_SECRET);

        public static string RemoveWhiteSpaces(this string input)
        {
            return Regex.Replace(input, @"\s+", "");
        }


        public static string GetFileContents(string sampleFile)
        {
            var asm = Assembly.GetExecutingAssembly();
            var resource = $"Apidaze.SDK.Tests.Unit.ScriptsBuilders.TestScripts.{sampleFile}";
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