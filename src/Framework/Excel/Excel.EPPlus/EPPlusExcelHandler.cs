using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Config.Abstractions;
using NetModular.Lib.Excel.Abstractions;
using NetModular.Lib.Utils.Core.Attributes;

namespace NetModular.Lib.Excel.EPPlus
{
    [Singleton]
    public class EPPlusExcelHandler : ExcelHandlerAbstract
    {
        public EPPlusExcelHandler(ILoginInfo loginInfo, IExcelExportHandler exportHandler, IConfigProvider configProvider) : base(loginInfo, exportHandler, configProvider)
        {
        }
    }
}
