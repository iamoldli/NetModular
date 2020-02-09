namespace NetModular.Lib.Options.Abstraction
{
    /// <summary>
    /// 模块配置项变更事件
    /// </summary>
    public interface IModuleOptionsChangedEvent<in TModuleOptions> where TModuleOptions : IModuleOptions
    {
        /// <summary>
        /// 变更事件
        /// </summary>
        /// <param name="options">新对象</param>
        /// <param name="oldOptions">旧对象</param>
        void OnChanged(TModuleOptions options, TModuleOptions oldOptions);
    }
}
