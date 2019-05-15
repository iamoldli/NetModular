using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using NetModular.Lib.Data.Query;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.AuditInfoService.ResultModels;
using NetModular.Module.Admin.Application.AuditInfoService.ViewModels;
using NetModular.Module.Admin.Domain.AuditInfo;

namespace NetModular.Module.Admin.Application.AuditInfoService
{
    public class AuditInfoService : IAuditInfoService
    {
        private readonly IMapper _mapper;
        private readonly IAuditInfoRepository _repository;

        public AuditInfoService(IAuditInfoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
            var result = new QueryResultModel<AuditInfoQueryResultModel>();
            var paging = model.Paging();
            var list = await _repository.Query(paging, model.ModuleCode, model.Controller, model.Action, model.StartTime, model.EndTime);

            result.Rows = _mapper.Map<List<AuditInfoQueryResultModel>>(list);
            result.Total = paging.TotalCount;

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Details(int id)
        {
            var entity = await _repository.Details(id);
            if (entity == null)
                return ResultModel.NotExists;

            return ResultModel.Success(entity);
        }
    }
}
