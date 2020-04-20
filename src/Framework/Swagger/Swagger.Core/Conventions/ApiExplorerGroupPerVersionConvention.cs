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

            string[] array = controller.ControllerType.FullName.Split('.');
            if (array.Length == 7)
            {
                controller.ApiExplorer.GroupName = array[2] + "_" + array[5];
                return;
            }
            controller.ApiExplorer.GroupName = array[2];
        }
    }
}
