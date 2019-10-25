using Microsoft.AspNetCore.Mvc;
using NetModular.Lib.Auth.Web.Attributes;
using NetModular.Lib.Validation.Abstractions;

namespace NetModular.Lib.Auth.Web
{
    /// <summary>
    /// 控制器抽象
    /// </summary>
    [Route("api/[area]/[controller]/[action]")]
    [ApiController]
    [PermissionValidate]
    [ValidateResultFormat]
    public abstract class ControllerAbstract : ControllerBase
    {
       
    }
}
