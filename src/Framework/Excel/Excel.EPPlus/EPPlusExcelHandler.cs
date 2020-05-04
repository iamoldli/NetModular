using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Config.Abstractions;
using NetModular.Lib.Excel.Abstractions;

namespace NetModular.Lib.Excel.EPPlus
{
    public class EPPlusExcelHandler : ExcelHandlerAbstract
    {
        public EPPlusExcelHandler(ILoginInfo loginInfo, IExcelExportHandler exportHandler, ExcelConfig config, IConfigProvider configProvider) : base(loginInfo, exportHandler, config, configProvider)
        {
        }
    }
}
