using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Swashbuckle.AspNetCore.SwaggerGen
{
    public class DocumentApplicationConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            application.ApiExplorer.IsVisible = true;
        }
    }
}