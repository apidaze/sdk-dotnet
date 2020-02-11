namespace IvrExample
{
    public class ContextHttp
    {
        public string AbsolutePath { get; set; }

        public string ContentType { get; set; } = "text/xml";

        public ContextHttp()
        {
        }

        public ContextHttp(string path) => AbsolutePath = path;

        public override bool Equals(object? obj)
        {
            return obj is ContextHttp other && other.AbsolutePath == this.AbsolutePath;
        }

        public override int GetHashCode()
        {
            return AbsolutePath == null ? 0 : AbsolutePath.GetHashCode();
        }
    }
}
