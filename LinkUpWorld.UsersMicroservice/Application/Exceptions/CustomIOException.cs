namespace LinkUpWorld.UsersMicroservice.Application.Exceptions
{
    public class CustomIOException : Exception
    {
        public CustomIOException(string message) : base(message)
        {

        }

        public CustomIOException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
