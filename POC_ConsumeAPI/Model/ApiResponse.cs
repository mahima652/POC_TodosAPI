namespace POC_ConsumeAPI.Model
{

    /// <summary>
    /// This class is used to send the response for Success and fail
    /// </summary>
    public class ApiResponse
    {
        #region Property
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public object Result { get; set; }

        #endregion

        #region Public Method

        /// <summary>
        /// This method is ued to send the response as Fail
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static ApiResponse Fail(string errorMessage ,int code )
        {
            return new ApiResponse { Succeeded = false, Message = errorMessage , StatusCode = code };
        }

        /// <summary>
        /// This method is used to send the response as Success
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="code"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static ApiResponse Success(string successMessage ,int code  , object result)
        {
            return new ApiResponse { Succeeded = true,  StatusCode = code  , Result = result , Message = successMessage };
        }

        #endregion
    }
}
