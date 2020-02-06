namespace Apidaze.SDK.Exception
{
    public class ApidazeCredentialsException : ApidazeRestException
    {
        public ApidazeCredentialsException(string message, uint statusCode = 400) : base(message, statusCode)
        {
        }
    }
}