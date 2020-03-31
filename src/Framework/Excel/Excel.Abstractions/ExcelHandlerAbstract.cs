using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Data.Query;
using NetModular.Lib.Utils.Core.Result;

namespace NetModular.Lib.Excel.Abstractions
{
    public abstract class ExcelHandlerAbstract : IExcelHandler
    {
        protected readonly ExcelOptions Options;
        protected readonly SystemConfigModel SystemConfig;
        protected readonly ILoginInfo LoginInfo;
        private readonly IExcelExportHandler _exportHandler;
        //导出Excel的对象的属性类型列表
        private readonly ConcurrentDictionary<RuntimeTypeHandle, Dictionary<string, PropertyInfo>> _exportObjectProperties = new ConcurrentDictionary<RuntimeTypeHandle, Dictionary<string, PropertyInfo>>();

        protected ExcelHandlerAbstract(ExcelOptions options, SystemConfigModel systemConfig, ILoginInfo loginInfo, IExcelExportHandler exportHandler)
        {
            Options = options;
            SystemConfig = systemConfig;
            LoginInfo = loginInfo;
            _exportHandler = exportHandler;
        }

        public ExcelExportResultModel Export<T>(ExportModel model, IList<T> entities) where T : class, new()
        {
            if (model == null)
                throw new NullReferenceException("Excel导出信息不存在");

            //设置列对应的属性类型
            SetColumnPropertyType<T>(model);

            var saveName = Guid.NewGuid() + model.Format.ToDescription();
            var saveDir = Path.Combine(Options.TempPath, "Export", DateTime.Now.Format("yyyyMMdd"));

            if (!Directory.Exists(saveDir))
            {
                Directory.CreateDirectory(saveDir);
            }

            var result = new ExcelExportResultModel
            {
                SaveName = saveName,
                FileName = model.FileName,
                Path = Path.Combine(saveDir, saveName)
            };

            using var fs = new FileStream(result.Path, FileMode.Create, FileAccess.Write);

            //创建文件
            _exportHandler.CreateExcel(model, entities, fs);

            return result;
        }

        /// <summary>
        /// 设置列属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        private void SetColumnPropertyType<T>(ExportModel model) where T : class, new()
        {
            if (model.Columns == null || !model.Columns.Any())
                return;

            var objectType = typeof(T);

            if (!_exportObjectProperties.TryGetValue(objectType.TypeHandle, out Dictionary<string, PropertyInfo> types))
            {
                types = new Dictionary<string, PropertyInfo>();
                var properties = objectType.GetProperties();
                foreach (var property in properties)
                {
                    types.Add(property.Name.ToLower(), property);
                }

                _exportObjectProperties.TryAdd(objectType.TypeHandle, types);
            }

            foreach (var column in model.Columns)
            {
                column.PropertyInfo = types.FirstOrDefault(m => m.Key.EqualsIgnoreCase(column.Name)).Value;
            }
        }
    }
}
