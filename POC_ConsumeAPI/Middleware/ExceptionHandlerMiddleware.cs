using Newtonsoft.Json;
using POC_ConsumeAPI.ExceptionTYpe;
using POC_ConsumeAPI.Model;
using System.Net;

namespace POC_ConsumeAPI.Middleware
{
    /// <summary>
    /// Middleware for handling global errors or exceptions
    /// Responsible for catching the exceptions and logging to the log file
    /// </summary>
    public class ExceptionHandlerMiddleware 
    {
        #region Property

        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        #endregion

        #region Constructor

        /// <summary>
        ///  Initializes a new instance of the <see cref="ExceptionHandlerMiddleware"/> class.
        /// </summary>
        /// <param name="next"></param>
        public ExceptionHandlerMiddleware(RequestDelegate next , ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        #endregion

        public async Task InvokeAsync(HttpContext context )
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex, _logger);
            }
        }

        private static Task HandleException(HttpContext context, Exception ex, ILogger logger)
        {
            logger.LogError(ex.Message);
            var errorMessageObject = ApiResponse.Fail( ex.Message , (int)HttpStatusCode.InternalServerError);

            switch (ex)
            {
                case InvalidException:
                    errorMessageObject.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case NotFoundException:
                    errorMessageObject.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                case UnAuthorizedException:
                    errorMessageObject.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;

             
            }

            var errorMessage = JsonConvert.SerializeObject(errorMessageObject);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errorMessageObject.StatusCode;
            return context.Response.WriteAsync(errorMessage);
        }
    }
}
