using Infrastructure.MultiTenancy.Settings;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Infrastructure.Swagger.OperationFilters
{
    internal class TenancyOperationFilter : IOperationFilter
    {
        private readonly TenancySettings _tenancySettings;

        public TenancyOperationFilter(IOptions<TenancySettings> tenancySettings)
        {
            _tenancySettings = tenancySettings.Value;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (_tenancySettings.IsEnabled)
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "tenant",
                    In = ParameterLocation.Header,
                    Required = true,
                    Schema = new OpenApiSchema
                    {
                        Type = "string"
                    }
                });
            }
        }
    }
}
