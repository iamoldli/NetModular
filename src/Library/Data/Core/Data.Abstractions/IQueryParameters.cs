using Dapper;

namespace Tm.Lib.Data.Abstractions
{
    /// <summary>
    /// 参数集合
    /// </summary>
    public interface IQueryParameters
    {
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        string Add(object value);

        /// <summary>
        /// 转换参数
        /// </summary>
        /// <returns></returns>
        DynamicParameters Parse();
    }
}
