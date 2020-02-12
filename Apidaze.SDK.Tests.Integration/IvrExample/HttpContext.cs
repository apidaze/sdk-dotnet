namespace IvrExample
{
    public class HttpContext
    {
        public string AbsolutePath { get; set; }

        public string ContentType { get; set; } = "text/xml";

        public HttpContext()
        {
        }

        public HttpContext(string path) => AbsolutePath = path;

        public override bool Equals(object? obj)
        {
            return obj is HttpContext other && other.AbsolutePath == this.AbsolutePath;
        }

        public override int GetHashCode()
        {
            return AbsolutePath == null ? 0 : AbsolutePath.GetHashCode();
        }
    }
}
