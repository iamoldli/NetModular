using Microsoft.AspNetCore.Mvc;
using Nm.Lib.Auth.Abstractions.Attributes;
using Nm.Lib.Validation.Abstractions;

namespace Nm.Lib.Module.Abstractions
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
