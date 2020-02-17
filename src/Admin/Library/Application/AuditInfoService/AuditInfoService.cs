using System.Threading.Tasks;
using NetModular.Lib.Excel.Abstractions;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Domain.AuditInfo;
using NetModular.Module.Admin.Domain.AuditInfo.Models;

namespace NetModular.Module.Admin.Application.AuditInfoService
{
    public class AuditInfoService : IAuditInfoService
    {
        private readonly IAuditInfoRepository _repository;
        private readonly IExcelHandler _excelHandler;

        public AuditInfoService(IAuditInfoRepository repository, IExcelHandler excelHandler)
        {
            _repository = repository;
            _excelHandler = excelHandler;
        }

        public async Task<IResultModel> Add(AuditInfoEntity info)
        {
            if (info == null)
                return ResultModel.Failed();

            var result = await _repository.AddAsync(info);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Query(AuditInfoQueryModel model)
        {
            var result = new QueryResultModel<AuditInfoEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Details(int id)
        {
            var entity = await _repository.Details(id);
            if (entity == null)
                return ResultModel.NotExists;

            return ResultModel.Success(entity);
        }

        public async Task<IResultModel> QueryLatestWeekPv()
        {
            var list = await _repository.QueryLatestWeekPv();
            return ResultModel.Success(list);
        }

        public async Task<IResultModel<ExcelExportResultModel>> Export(AuditInfoQueryModel model)
        {
            var result = new ResultModel<ExcelExportResultModel>();
            var list = await _repository.Query(model);
            if (model.IsOutOfExportCountLimit)
            {
                return result.Failed($"导出数据不能超过{model.ExportCountLimit}条");
            }

            return result.Success(_excelHandler.Export(model.Export, list));
        }
    }
}
