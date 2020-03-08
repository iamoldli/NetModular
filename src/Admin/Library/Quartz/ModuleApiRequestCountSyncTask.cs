using System.ComponentModel;
using System.Threading.Tasks;
using NetModular.Lib.Quartz.Abstractions;
using NetModular.Module.Admin.Application.ModuleService;

namespace NetModular.Module.Admin.Quartz
{
    [Description("模块接口请求数量同步")]
    public class ModuleApiRequestCountSyncTask : TaskAbstract
    {
        private readonly IModuleService _moduleService;
        public ModuleApiRequestCountSyncTask(ITaskLogger logger, IModuleService moduleService) : base(logger)
        {
            _moduleService = moduleService;
        }

        public override async Task Execute(ITaskExecutionContext context)
        {
            await _moduleService.SyncApiRequestCount();
        }
    }
}
