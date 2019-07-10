using System;
using OpenApi.Generator.Core;

namespace Microsoft.AspNetCore.Builder
{
    public static class DocumentationBuilderExtensions
    {
        public static IApplicationBuilder UseDocumentation(
            this IApplicationBuilder app,
            Action<DocumentationOptions> setupAction = null)
        {
            if (setupAction == null)
            {
                // Don't pass options so it can be configured/injected via DI container instead
                app.UseMiddleware<DocumentationMiddleware>();
            }
            else
            {
                // Configure an options instance here and pass directly to the middleware
                var options = new DocumentationOptions();
                setupAction.Invoke(options);

                app.UseMiddleware<DocumentationMiddleware>(options);
            }

            return app;
        }
    }
}