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
            if (controller.ControllerType.Namespace.IsNull())
                return;

            var arr = controller.ControllerType.FullName.Split('.');
            if (arr.Length == 7)
            {
                controller.ApiExplorer.GroupName = arr[2] + "_" + arr[5];
            }
            else
            {
                controller.ApiExplorer.GroupName = arr[2];
            }
        }
    }
}
