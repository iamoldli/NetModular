using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace NetModular.Lib.Swagger.Conventions
{
    /// <summary>
    /// API分组约定
    /// </summary>
    public class ApiExplorerGroupConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var controllerNamespace = controller.ControllerType.Namespace;
            var arr = controllerNamespace.Split('.');
            var moduleName = arr[arr.Length - 3];

            controller.ApiExplorer.GroupName = moduleName;
        }
    }
}
