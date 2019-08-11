using Microsoft.AspNetCore.Mvc;
using Nm.Lib.Validation.Abstractions;

namespace Nm.Lib.Auth.Web
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
