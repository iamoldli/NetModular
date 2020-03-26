namespace NetModular.Module.Admin.Application.ToolService
{
    /// <summary>
    /// 工具服务
    /// </summary>
    public interface IToolService
    {
        /// <summary>
        /// 获取枚举下拉列表
        /// </summary>
        /// <param name="moduleCode">模块编码</param>
        /// <param name="enumName">枚举名称</param>
        /// <param name="libName">库名称</param>
        /// <returns></returns>
        IResultModel GetEnumSelect(string moduleCode, string enumName, string libName = "Domain");
    }
}
