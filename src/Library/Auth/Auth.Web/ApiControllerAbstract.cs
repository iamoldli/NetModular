using Microsoft.AspNetCore.Mvc;
using Tm.Lib.Validation.Abstractions;

namespace Tm.Lib.Auth.Web
{
    /// <summary>
    /// 控制器抽象
    /// </summary>
    [Route("api/[area]/[controller]/[action]")]
    [ApiController]
    [ValidateResultFormat]
    public abstract class ApiControllerAbstract : ControllerBase
    {
       
    }
}
