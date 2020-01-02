namespace Apidaze.SDK.Exception
{
    public class ApidazeRestException : System.Exception
    {
        public uint StatusCode { get; set; }

        public ApidazeRestException(string message, uint statusCode) : base(message)
        {
            this.StatusCode = statusCode;
        }
    }
}
