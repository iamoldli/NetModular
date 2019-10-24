using System.Threading.Tasks;
using AutoMapper;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.Admin.Domain.AuditInfo;
using Nm.Module.Admin.Domain.AuditInfo.Models;

namespace Nm.Module.Admin.Application.AuditInfoService
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
    }
}
