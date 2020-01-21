namespace NetModular.Lib.Cache.Abstractions
{
    /// <summary>
    /// 缓存键描述信息
    /// </summary>
    public class CacheKeyDescriptor
    {
        /// <summary>
        /// 所属模块编码
        /// </summary>
        public string ModuleCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Desc { get; set; }
    }
}
