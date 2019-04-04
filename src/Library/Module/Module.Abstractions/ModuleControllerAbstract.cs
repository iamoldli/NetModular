using Microsoft.AspNetCore.Mvc;
using NetModular.Lib.Auth.Abstractions.Attributes;
using NetModular.Lib.Validation.Abstractions;

namespace NetModular.Lib.Module.Abstractions
{
    /// <summary>
    /// 模块通用控制器
    /// </summary>
    [Route("api/[area]/[controller]/[action]")]
    [ApiController]
    [PermissionValidate]
    [ValidateResultFormat]
    public abstract class ModuleControllerAbstract : ControllerBase
    {
       
    }
}
