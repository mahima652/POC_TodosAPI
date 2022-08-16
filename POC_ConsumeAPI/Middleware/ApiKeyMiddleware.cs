using POC_ConsumeAPI.ExceptionTYpe;

namespace POC_ConsumeAPI.Middleware
{
    public class ApiKeyMiddleware
    {

        private readonly RequestDelegate _next;
        private const string APIKEYNAME = "ApiKey";

        public ApiKeyMiddleware(RequestDelegate next )
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(APIKEYNAME, out var actualKeyValue))
            {
                throw new UnAuthorizedException("Api key was not provided (using APIKeyMiddleware)") ;
            }

            var appSetting = context.RequestServices.GetRequiredService<IConfiguration>();

            var expectedKeyValue = appSetting.GetValue<string>(APIKEYNAME);

            if (!expectedKeyValue.Equals(actualKeyValue))
            {
                throw new UnAuthorizedException("UnAuthorizedException Request");
            }

            await _next(context);
        }

    }
}
