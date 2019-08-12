using Microsoft.AspNetCore.Mvc;
using Tm.Lib.Auth.Web.Attributes;
using Tm.Lib.Validation.Abstractions;

namespace Tm.Lib.Auth.Web
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
