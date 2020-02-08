using NetModular.Lib.Data.Query;

namespace NetModular.Module.Admin.Domain.Config.Models
{
    public class ConfigQueryModel : QueryModel
    {
        public string Key { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public ConfigType? Type { get; set; }
    }
}
