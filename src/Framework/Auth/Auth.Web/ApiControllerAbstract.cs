using Microsoft.AspNetCore.Mvc;
using NetModular.Lib.Module.AspNetCore.Attributes;
using NetModular.Lib.Validation.Abstractions;

namespace NetModular.Lib.Auth.Web
{
    /// <summary>
    /// 控制器抽象
    /// </summary>
    [Route("api/[area]/[controller]/[action]")]
    [ApiController]
    [ValidateResultFormat]
    [DisableAuditing]
    public abstract class ApiControllerAbstract : ControllerBase
    {
       
    }
}
