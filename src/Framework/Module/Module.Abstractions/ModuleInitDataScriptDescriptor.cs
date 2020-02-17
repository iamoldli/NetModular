namespace NetModular.Lib.Module.Abstractions
{
    /// <summary>
    /// 模块初始化数据脚本信息
    /// </summary>
    public class ModuleInitDataScriptDescriptor
    {
        /// <summary>
        /// SqlServer脚本路径
        /// </summary>
        public string SqlServer { get; set; }

        /// <summary>
        /// MySql脚本路径
        /// </summary>
        public string MySql { get; set; }

        /// <summary>
        /// SQLite脚本路径
        /// </summary>
        public string SQLite { get; set; }

        /// <summary>
        /// PostgreSQL脚本路径
        /// </summary>
        public string PostgreSQL { get; set; }

        /// <summary>
        /// Oracle脚本路径
        /// </summary>
        public string Oracle { get; set; }
    }
}
