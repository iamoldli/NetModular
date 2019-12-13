using System.Collections.Generic;
using NetModular.Lib.Data.Query;
using NetModular.Lib.Utils.Core.Result;

namespace NetModular.Lib.Excel.Abstractions
{
    /// <summary>
    /// Excel处理接口
    /// </summary>
    public interface IExcelHandler
    {
        /// <summary>
        /// 导出
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        ExcelExportResultModel Export<T>(ExportModel model, IList<T> entities) where T : class, new();
    }
}
