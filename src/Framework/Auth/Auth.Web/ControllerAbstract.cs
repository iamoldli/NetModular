using System;
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
    [Auditing]
    public abstract class ControllerAbstract : ControllerBase
    {
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        protected IActionResult ExportExcel(string filePath, string fileName)
        {
            if (fileName.IsNull())
            {
                fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
            }
            return PhysicalFile(filePath, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName, true);
        }
    }
}
