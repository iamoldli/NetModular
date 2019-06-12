using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Nm.Lib.Cache.Abstractions;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.Common.Application.AreaService.ViewModels;
using Nm.Module.Common.Domain.Area;
using Nm.Module.Common.Domain.Area.Models;
using Nm.Module.Common.Infrastructure.AreaCrawling;
using Nm.Module.Common.Infrastructure.Repositories;

namespace Nm.Module.Common.Application.AreaService
{
    public class AreaService : IAreaService
    {
        private const string AreaCacheKey = "COMMON_AREA_";
        private readonly ICacheHandler _cache;
        private readonly IMapper _mapper;
        private readonly IAreaRepository _repository;
        private readonly IAreaCrawlingHandler _areaCrawlingHandler;
        private readonly IUnitOfWork _uow;

        public AreaService(IMapper mapper, IAreaRepository repository, IAreaCrawlingHandler areaCrawlingHandler, IUnitOfWork<CommonDbContext> uow, ICacheHandler cache)
        {
            _mapper = mapper;
            _repository = repository;
            _areaCrawlingHandler = areaCrawlingHandler;
            _uow = uow;
            _cache = cache;
        }

        public async Task<IResultModel> Query(AreaQueryModel model)
        {
            var result = new QueryResultModel<AreaEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };
            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(AreaAddModel model)
        {
            var entity = _mapper.Map<AreaEntity>(model);
            if (await _repository.Exists(entity))
            {
                return ResultModel.HasExists;
            }
            
            entity.Pinyin= NPinyin.Pinyin.GetPinyin(entity.Name);
            entity.Jianpin= NPinyin.Pinyin.GetInitials(entity.Name);

            var result = await _repository.AddAsync(entity);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Delete(int id)
        {
            var result = await _repository.DeleteAsync(id);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Edit(int id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var model = _mapper.Map<AreaUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(AreaUpdateModel model)
        {
            var entity = await _repository.GetAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(model, entity);

            if (await _repository.Exists(entity))
            {
                return ResultModel.HasExists;
            }

            entity.Pinyin = NPinyin.Pinyin.GetPinyin(entity.Name);
            entity.Jianpin = NPinyin.Pinyin.GetInitials(entity.Name);

            var result = await _repository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Crawling()
        {
            var list = await _areaCrawlingHandler.Crawling();

            _uow.BeginTransaction();

            foreach (var m in list)
            {
                var entity = _mapper.Map<AreaEntity>(m);
                await CrawlingInsert(entity, m.Children);
            }

            _uow.Commit();

            return ResultModel.Success();
        }

        //插入爬取的数据
        private async Task CrawlingInsert(AreaEntity entity, List<AreaCrawlingModel> children)
        {
            if (await _repository.AddAsync(entity))
            {
                foreach (var child in children)
                {
                    var childEntity = _mapper.Map<AreaEntity>(child);
                    childEntity.Level = entity.Level + 1;
                    childEntity.ParentId = entity.Id;
                    await CrawlingInsert(childEntity, child.Children);
                }
            }
        }

        public async Task<IResultModel> QueryChildren(int parentId)
        {
            var cacheKey = AreaCacheKey + parentId;
            if (!_cache.TryGetValue(cacheKey, out IList<AreaEntity> list))
            {
                list = await _repository.QueryChildren(parentId);

                await _cache.SetAsync(cacheKey, list);
            }

            return ResultModel.Success(list);
        }
    }
}
