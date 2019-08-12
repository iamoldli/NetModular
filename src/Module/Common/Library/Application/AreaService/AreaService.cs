using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AutoMapper;
using Tm.Lib.Cache.Abstractions;
using Tm.Lib.Utils.Core.Extensions;
using Tm.Lib.Utils.Core.Result;
using Tm.Module.Common.Application.AreaService.ViewModels;
using Tm.Module.Common.Domain.Area;
using Tm.Module.Common.Domain.Area.Models;
using Tm.Module.Common.Infrastructure;
using Tm.Module.Common.Infrastructure.AreaCrawling;

namespace Tm.Module.Common.Application.AreaService
{
    public class AreaService : IAreaService
    {
        private readonly ICacheHandler _cache;
        private readonly IMapper _mapper;
        private readonly IAreaRepository _repository;

        public AreaService(IMapper mapper, IAreaRepository repository, ICacheHandler cache)
        {
            _mapper = mapper;
            _repository = repository;
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

            entity.Pinyin = NPinyin.Pinyin.GetPinyin(entity.Name);
            entity.Jianpin = NPinyin.Pinyin.GetInitials(entity.Name);

            var result = await _repository.AddAsync(entity);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Delete(int id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            if (await _repository.DeleteAsync(id))
            {
                await ClearCache(entity);
                return ResultModel.Success();
            }

            return ResultModel.Failed();
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

            if (await _repository.UpdateAsync(entity))
            {
                //清除缓存
                await ClearCache(entity);
            }

            return ResultModel.Failed();
        }

        public async Task<IResultModel> CrawlInsert(IList<AreaCrawlingModel> list)
        {
            using (var tran = _repository.BeginTransaction())
            {
                foreach (var m in list)
                {
                    var entity = _mapper.Map<AreaEntity>(m);
                    await CrawlingInsert(entity, m.Children, tran);
                }

                tran.Commit();
            }

            return ResultModel.Success();
        }

        //插入爬取的数据
        private async Task CrawlingInsert(AreaEntity entity, List<AreaCrawlingModel> children, IDbTransaction transaction)
        {
            if (await _repository.AddAsync(entity, transaction))
            {
                foreach (var child in children)
                {
                    var childEntity = _mapper.Map<AreaEntity>(child);
                    childEntity.Level = entity.Level + 1;
                    childEntity.ParentId = entity.Id;
                    await CrawlingInsert(childEntity, child.Children, transaction);
                }
            }
        }

        public async Task<IResultModel<IList<AreaEntity>>> QueryChildren(string parentCode)
        {
            var result = new ResultModel<IList<AreaEntity>>();
            var cacheKey = CacheKeys.Area + parentCode;
            if (!_cache.TryGetValue(cacheKey, out IList<AreaEntity> list))
            {
                var parentId = 0;
                if (parentCode.NotNull() && parentCode != "0")
                {
                    var entity = await _repository.GetByCode(parentCode);
                    if (entity != null)
                    {
                        parentId = entity.Id;
                    }
                }

                list = await _repository.QueryChildren(parentId);
                await _cache.SetAsync(cacheKey, list);
            }

            return result.Success(list);
        }

        /// <summary>
        /// 清楚缓存
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private async Task ClearCache(AreaEntity entity)
        {
            if (entity.ParentId > 0)
            {
                var parent = await _repository.GetAsync(entity.ParentId);
                if (parent != null)
                {
                    await _cache.RemoveAsync(CacheKeys.Area + parent.Code);
                }
            }
        }
    }
}
