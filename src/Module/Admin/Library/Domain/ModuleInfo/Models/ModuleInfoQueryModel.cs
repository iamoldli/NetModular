using Tm.Lib.Data.Query;

namespace Tm.Module.Admin.Domain.ModuleInfo.Models
{
    public class ModuleInfoQueryModel : QueryModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
    }
}
