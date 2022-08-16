namespace POC_ConsumeAPI.ExceptionTYpe
{
    public class InvalidException : Exception
    {
        public InvalidException(string message) : base(message)
        {
        }
    }

    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }

    public class UnAuthorizedException : Exception
    {
        public UnAuthorizedException(string message) : base(message)
        {
        }
    }

}
