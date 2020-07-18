using Microsoft.Extensions.DependencyInjection;

using NSwag;
using NSwag.Generation.Processors.Security;

using System;
using System.Collections.Generic;
using System.Text;

namespace XPY.WebsiteSolution.Utilities.Extensions.DependencyInjection.OpenApi
{
    public static class OpenApiDependencyInjectionExtensions
    {
        public static IServiceCollection AddOpenApi(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddOpenApiDocument(config =>
            {
                config.Title = "XPY.WebsiteSolution";
                config.Description = "XPY.WebsiteSolution";
                config.Version = "1.0.0";
                config.DocumentProcessors.Add(
                   new SecurityDefinitionAppender("JWT",
                   new OpenApiSecurityScheme
                   {
                       Type = OpenApiSecuritySchemeType.ApiKey,
                       Name = "Authorization",
                       In = OpenApiSecurityApiKeyLocation.Header,
                       Description = "Type into the textbox: Bearer {your JWT token}."
                   }));
                config.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT"));
                //config.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });
        }
    }
}
