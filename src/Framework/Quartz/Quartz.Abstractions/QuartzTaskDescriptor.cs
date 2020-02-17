namespace NetModular.Lib.Quartz.Abstractions
{
    /// <summary>
    /// 任务描述信息
    /// </summary>
    public class QuartzTaskDescriptor
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类名称
        /// </summary>
        public string Class { get; set; }
    }
}
