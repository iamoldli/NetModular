namespace NetModular.Lib.Options.Abstraction
{
    /// <summary>
    /// 模块配置项描述信息
    /// </summary>
    public class ModuleOptionDescriptor
    {
        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
}
