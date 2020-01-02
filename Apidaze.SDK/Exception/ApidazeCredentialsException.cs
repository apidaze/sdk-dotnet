namespace Apidaze.SDK.Exception
{
    public class ApidazeCredentialsException : ApidazeRestException
    {
        public ApidazeCredentialsException(string message, uint statuCode = 400) : base(message, statuCode)
        {

        }
    }
}
