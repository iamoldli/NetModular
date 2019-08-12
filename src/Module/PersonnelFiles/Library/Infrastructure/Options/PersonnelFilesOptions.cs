using Tm.Lib.Utils.Core.Options;

namespace Tm.Module.PersonnelFiles.Infrastructure.Options
{
    /// <summary>
    /// 人事档案配置项
    /// </summary>
    public class PersonnelFilesOptions : IModuleOptions
    {
        /// <summary>
        /// 用户初始工号，默认100000
        /// </summary>
        public int UserInitialJobNumber { get; set; }

        public PersonnelFilesOptions()
        {
            UserInitialJobNumber = 100000;
        }
    }
}
