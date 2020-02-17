using System;
using System.Collections.Generic;
using System.IO;
using NetModular.Lib.Data.Query;
using NetModular.Lib.Excel.Abstractions;

namespace NetModular.Lib.Excel.NPOI
{
    public class NPOIExcelExportHandler : IExcelExportHandler
    {
        private readonly ExcelOptions _options;

        public NPOIExcelExportHandler(ExcelOptions options)
        {
            _options = options;
        }
        public void CreateExcel<T>(ExportModel model, IList<T> entities, Stream stream) where T : class, new()
        {
            throw new NotImplementedException();
        }
    }
}
