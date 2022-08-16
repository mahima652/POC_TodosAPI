namespace POC_ConsumeAPI.Model
{

    public class ApiResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
        public object Result { get; set; }


        public static ApiResponse Fail(string errorMessage ,int code)
        {
            return new ApiResponse { Succeeded = false, Message = errorMessage , Code = code };
        }
        public static ApiResponse Success( int code  , object result)
        {
            return new ApiResponse { Succeeded = true,  Code = code  , Result = result };
        }
    }
}
