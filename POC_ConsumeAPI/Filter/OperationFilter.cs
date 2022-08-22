using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;


namespace POC_ConsumeAPI.Filter
{
    /// <summary>
    ///  This filter is used to add the header parameter in Swagger UI
    /// </summary>
    public class OperationFilter : IOperationFilter
    {
        #region Public Method
        /// <summary>
        ///  This method is used to implement IOperationFilter interfaces 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "X-APIKey",
                In = ParameterLocation.Header,
                Required = false
            });
        }

        #endregion
    }

}
