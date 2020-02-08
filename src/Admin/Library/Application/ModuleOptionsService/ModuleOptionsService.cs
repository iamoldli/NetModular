using System.Linq;
using NetModular.Lib.Options.Abstraction;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.ModuleOptionsService.ViewModels;

namespace NetModular.Module.Admin.Application.ModuleOptionsService
{
    public class ModuleOptionsService : IModuleOptionsService
    {
        private readonly IModuleOptionsContainer _moduleOptionsContainer;

        public ModuleOptionsService(IModuleOptionsContainer moduleOptionsContainer)
        {
            _moduleOptionsContainer = moduleOptionsContainer;
        }

        public IResultModel Edit(string moduleCode)
        {
            var model = new ModuleOptionsUpdateModel
            {
                ModuleCode = moduleCode,
                Definitions = _moduleOptionsContainer.GetDefinitions(moduleCode),
                Instance = _moduleOptionsContainer.GetInstance(moduleCode)
            };

            return ResultModel.Success(model);
        }

        public IResultModel Update(ModuleOptionsUpdateModel model)
        {
            if (model.Values != null && model.Values.Any())
            {
                _moduleOptionsContainer.RefreshInstance(model.ModuleCode, model.Values);
                return ResultModel.Success("保存成功");
            }

            return ResultModel.Failed("配置信息不存在");
        }
    }
}
