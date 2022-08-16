using Newtonsoft.Json;
using POC_ConsumeAPI.ExceptionTYpe;
using POC_ConsumeAPI.Model;
using System.Net;

namespace POC_ConsumeAPI.Middleware
{
    public class ExceptionHandlerMiddleware 
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, ILogger<ExceptionHandlerMiddleware> logger )
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex, logger);
            }
        }

        private static Task HandleException(HttpContext context, Exception ex, ILogger<ExceptionHandlerMiddleware> logger)
        {
            logger.LogError("Error retrieving data from the database " + " Giving excetion as " + ex.Message);
            var errorMessageObject = ApiResponse.Fail(ex.Message , 500);
            var statusCode = (int)HttpStatusCode.InternalServerError;
            switch (ex)
            {
                case InvalidException:
                    errorMessageObject.StatusCode = 400;
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case NotFoundException:
                    errorMessageObject.StatusCode = 404;
                    statusCode = (int)HttpStatusCode.NotFound;
                    break;

                case UnAuthorizedException:
                    errorMessageObject.StatusCode = 401;
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    break;

            }

            var errorMessage = JsonConvert.SerializeObject(errorMessageObject);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(errorMessage);
        }
    }
}
