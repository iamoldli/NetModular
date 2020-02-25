using System.Linq;
using NetModular.Lib.Module.Abstractions;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Lib.Utils.Core.Result;

namespace NetModular.Module.Admin.Application.ToolService
{
    public class ToolService : IToolService
    {
        private readonly IModuleCollection _moduleCollection;

        public ToolService(IModuleCollection moduleCollection)
        {
            _moduleCollection = moduleCollection;
        }

        public IResultModel GetEnumSelect(string moduleCode, string enumName, string libName = "Domain")
        {
            if (moduleCode.IsNull() || enumName.IsNull() || libName.IsNull())
                return ResultModel.Failed("参数有误");

            var module = _moduleCollection.FirstOrDefault(m => m.Id.EqualsIgnoreCase(moduleCode));
            if (module == null)
                return ResultModel.Failed("模块不存在");

            var enumDescriptor = module.EnumDescriptors.FirstOrDefault(m =>
                m.Name.EqualsIgnoreCase(enumName) && m.LibraryName.EqualsIgnoreCase(libName));
            if (enumDescriptor == null)
                return ResultModel.Failed("数据不存在");

            return ResultModel.Success(enumDescriptor.Options);
        }
    }
}
