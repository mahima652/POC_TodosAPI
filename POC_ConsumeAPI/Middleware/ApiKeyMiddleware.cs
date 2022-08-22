using POC_ConsumeAPI.ExceptionTYpe;

namespace POC_ConsumeAPI.Middleware
{
    /// <summary>
    ///  This Middleware is used to handling the API key Authentication 
    ///  Responsible for throwing the exceptions related to the authentication
    ///  </summary>
    public class ApiKeyMiddleware
    {
        #region Property

        private readonly RequestDelegate _next;
        private const string APIKEYNAME = "X-APIKey";

        #endregion

        #region Constructor 

        /// <summary>
        ///  Initializes a new instance of the <see cref="ApiKeyMiddleware"/> class.
        /// </summary>
        /// <param name="next"></param>
        public ApiKeyMiddleware(RequestDelegate next )
        {
            _next = next;
        }

        #endregion

        #region Public Method

        /// <summary>
        ///  This Invoke method is used to handel the header with APIKEY Authentication
        ///  And pass the request to the next middleware
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="UnAuthorizedException"></exception>
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

        #endregion

    }
}
