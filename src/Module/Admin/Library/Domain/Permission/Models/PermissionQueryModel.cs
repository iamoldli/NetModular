using Tm.Lib.Data.Query;

namespace Tm.Module.Admin.Domain.Permission.Models
{
    public class PermissionQueryModel : QueryModel
    {
        /// <summary>
        /// 模块编码(对应请求中area参数)
        /// </summary>
        public string ModuleCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Action { get; set; }
    }
}
