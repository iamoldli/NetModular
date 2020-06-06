using Microsoft.AspNetCore.Mvc;
using NetModular.Lib.Validation.Abstractions;

namespace NetModular.Lib.Auth.Web
{
    /// <summary>
    /// 公共接口抽象控制器
    /// </summary>
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    [ValidateResultFormat]
    public abstract class ApiControllerAbstract : ControllerBase
    {
       
    }
}
