using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Config.Abstractions;
using NetModular.Lib.Config.Abstractions.Impl;
using NetModular.Lib.Data.Query;
using NetModular.Lib.Excel.Abstractions;
using NetModular.Lib.Utils.Core.Attributes;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace NetModular.Lib.Excel.EPPlus
{
    [Singleton]
    public class EPPlusExcelExportHandler : IExcelExportHandler
    {
        private readonly ILoginInfo _loginInfo;
        private readonly IConfigProvider _configProvider;

        public EPPlusExcelExportHandler(ILoginInfo loginInfo, IConfigProvider configProvider)
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            _loginInfo = loginInfo;
            _configProvider = configProvider;
        }

        public void CreateExcel<T>(ExportModel model, IList<T> entities, Stream stream) where T : class, new()
        {
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add(model.Title);

            var index = 1;
            SetTitle(worksheet, model, ref index);

            SetDescription(worksheet, model, ref index);

            SetColumnName(worksheet, model, ref index);

            SetColumn<T>(worksheet, model, entities, index);

            package.SaveAs(stream);
        }

        /// <summary>
        /// 设置标题
        /// </summary>
        private void SetTitle(ExcelWorksheet sheet, ExportModel model, ref int index)
        {
            if (!model.ShowTitle)
                return;

            sheet.Row(index).Height = 30;
            var title = sheet.Cells[1, 1, 1, model.Columns.Count];
            title.Value = model.Title;
            title.Merge = true;
            title.Style.Font.Size = 17;
            title.Style.Font.Color.SetColor(Color.Black);
            title.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            title.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            title.Style.Fill.PatternType = ExcelFillStyle.Solid;
            title.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(221, 235, 247));
            index++;
        }

        /// <summary>
        /// 设置说明
        /// </summary>
        private void SetDescription(ExcelWorksheet sheet, ExportModel model, ref int index)
        {
            var subSb = new StringBuilder();
            if (model.ShowExportPeople && _loginInfo != null)
            {
                subSb.AppendFormat("导出人：{0}    ", _loginInfo.AccountName);
            }

            if (model.ShowExportDate)
            {
                subSb.AppendFormat("导出时间：{0}    ", DateTime.Now.Format());
            }

            if (model.ShowCopyright)
            {
                var config = _configProvider.Get<SystemConfig>();
                subSb.AppendFormat("{0}", config.Copyright);
            }

            if (subSb.Length < 1)
                return;

            sheet.Row(index).Height = 20;
            var cell = sheet.Cells[2, 1, 2, model.Columns.Count];
            cell.Value = subSb.ToString();
            cell.Merge = true;
            cell.Style.Font.Size = 10;
            cell.Style.Font.Color.SetColor(Color.Black);
            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
            cell.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(198, 224, 180));
            index++;
        }

        /// <summary>
        /// 设置列名
        /// </summary>
        private void SetColumnName(ExcelWorksheet sheet, ExportModel model, ref int index)
        {
            if (!model.ShowColName)
                return;

            sheet.Row(index).Height = 25;
            for (int i = 0; i < model.Columns.Count; i++)
            {
                var col = model.Columns[i];
                var cell = sheet.Cells[index, i + 1];
                cell.Value = col.Label;
                cell.Style.Font.Size = 12;
                cell.Style.Font.Bold = true;
                cell.Style.Font.Color.SetColor(Color.CornflowerBlue);
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                if (col.Width > 0)
                {
                    sheet.Column(i + 1).Width = col.Width;
                }
                else
                {
                    sheet.Column(i + 1).AutoFit();
                }
            }
            index++;
        }

        /// <summary>
        /// 设置列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sheet"></param>
        /// <param name="model"></param>
        /// <param name="entities"></param>
        /// <param name="index"></param>
        private void SetColumn<T>(ExcelWorksheet sheet, ExportModel model, IList<T> entities, int index) where T : class, new()
        {
            foreach (var entity in entities)
            {
                sheet.Row(index).Height = 20;

                for (int i = 0; i < model.Columns.Count; i++)
                {
                    var col = model.Columns[i];
                    var cell = sheet.Cells[index, i + 1];
                    cell.Style.Font.Size = 11;
                    cell.Style.Font.Color.SetColor(Color.Black);
                    cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    if (col.PropertyInfo == null)
                    {
                        cell.Value = "";
                    }
                    else
                    {
                        if (col.PropertyInfo.PropertyType.IsDateTime())
                        {
                            cell.Style.Numberformat.Format = "yyyy/MM/dd HH:mm:ss";
                            cell.Formula = "=DATE(2014,10,5)";
                        }

                        //格式化
                        if (col.Format.NotNull())
                        {
                            cell.Style.Numberformat.Format = col.Format;
                        }

                        cell.Value = col.PropertyInfo.GetValue(entity);
                    }
                }

                index++;
            }
        }
    }
}
