namespace NetModular.Lib.Config.Abstraction
{
    /// <summary>
    /// 配置描述
    /// </summary>
    public class ConfigDescriptor
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
