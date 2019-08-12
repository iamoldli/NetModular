using System;
using System.Threading.Tasks;
using AutoMapper;
using Tm.Lib.Cache.Abstractions;
using Tm.Lib.Utils.Core.Result;
using Tm.Module.Common.Application.DictService.ViewModels;
using Tm.Module.Common.Domain.Dict;
using Tm.Module.Common.Domain.Dict.Models;

namespace Tm.Module.Common.Application.DictService
{
    public class DictService : IDictService
    {
        private const string DictCacheKey = "COMMON_Dict_";
        private readonly ICacheHandler _cache;
        private readonly IMapper _mapper;
        private readonly IDictRepository _repository;

        public DictService(IMapper mapper, IDictRepository repository, ICacheHandler cache)
        {
            _mapper = mapper;
            _repository = repository;
            _cache = cache;
        }

        public async Task<IResultModel> Query(DictQueryModel model)
        {
            var result = new QueryResultModel<DictEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };
            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(DictAddModel model)
        {
            var entity = _mapper.Map<DictEntity>(model);
            //if (await _repository.Exists(entity))
            //{
                //return ResultModel.HasExists;
            //}

            var result = await _repository.AddAsync(entity);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Delete(Guid id)
        {
            var result = await _repository.DeleteAsync(id);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Edit(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var model = _mapper.Map<DictUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(DictUpdateModel model)
        {
            var entity = await _repository.GetAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(model, entity);

            //if (await _repository.Exists(entity))
            //{
                //return ResultModel.HasExists;
            //}

            var result = await _repository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }

        public Task<IResultModel> QueryChildren(int parentId)
        {
            throw new NotImplementedException();
        }
    }
}
