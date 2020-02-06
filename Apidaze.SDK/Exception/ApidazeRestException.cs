namespace Apidaze.SDK.Exception
{
    public class ApidazeRestException : System.Exception
    {
        public ApidazeRestException(string message, uint statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public uint StatusCode { get; set; }
    }
}