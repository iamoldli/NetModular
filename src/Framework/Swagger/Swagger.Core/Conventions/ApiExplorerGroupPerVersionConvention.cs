using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace NetModular.Lib.Swagger.Core.Conventions
{
    /// <summary>
    /// API分组约定
    /// </summary>
    public class ApiExplorerGroupConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (controller.ControllerType.Namespace != null)
                controller.ApiExplorer.GroupName = controller.ControllerType.Namespace.Split('.')[2];
        }
    }
}
