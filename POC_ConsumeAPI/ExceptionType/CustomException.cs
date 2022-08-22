namespace POC_ConsumeAPI.ExceptionTYpe
{
    /// <summary>
    ///  This InvalidException is custom exception class that inherit parent class Exception
    ///  Throwing an exception when invalid request recived 
    /// </summary>
    public class InvalidException : Exception
    {
        public InvalidException(string message) : base(message)
        {
        }
    }

    public class NotFoundException : Exception
    {
        /// <summary>
        ///  This NotFoundException is custom exception class that inherit parent class Exception
        ///  Throwing an exception when Item not found 
        /// </summary>
        public NotFoundException(string message) : base(message)
        {
        }
    }

    /// <summary>
    ///  This UnAuthorizedException is custom exception class that inherit parent class Exception
    ///  Throwing an exception when unauthorized request recived 
    /// </summary>
    public class UnAuthorizedException : Exception
    {
        public UnAuthorizedException(string message) : base(message)
        {
        }
    }

}
