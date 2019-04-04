using Microsoft.AspNetCore.Mvc.Filters;

namespace NetModular.Lib.Validation.Abstractions
{
    /// <summary>
    /// 验证结果格式化器
    /// </summary>
    public interface IValidateResultFormatHandler
    {
        /// <summary>
        /// 格式化处理
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        void Format(ResultExecutingContext context);
    }
}
