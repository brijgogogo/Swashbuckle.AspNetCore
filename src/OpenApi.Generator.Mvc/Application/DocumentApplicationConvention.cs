using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace OpenApi.Generator.Mvc
{
    public class DocumentApplicationConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            application.ApiExplorer.IsVisible = true;
        }
    }
}