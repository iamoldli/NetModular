using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.Common.Application.AreaService.ViewModels;
using Nm.Module.Common.Domain.Area;
using Nm.Module.Common.Domain.Area.Models;
using Nm.Module.Common.Infrastructure;
using Nm.Module.Common.Infrastructure.AreaCrawling;
using Nm.Module.Common.Infrastructure.Repositories;

namespace Nm.Module.Common.Application.AreaService
{
    public class AreaService : IAreaService
    {
        private readonly IMapper _mapper;
        private readonly IAreaRepository _repository;
        private readonly IAreaCrawlingHandler _areaCrawlingHandler;
        private readonly IUnitOfWork _uow;

        public AreaService(IMapper mapper, IAreaRepository repository, IAreaCrawlingHandler areaCrawlingHandler, IUnitOfWork<CommonDbContext> uow)
        {
            _mapper = mapper;
            _repository = repository;
            _areaCrawlingHandler = areaCrawlingHandler;
            _uow = uow;
        }

        public Task<IResultModel> GetTree()
        {
            throw new NotImplementedException();
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
            //if (await _repository.Exists(entity))
            //{
            //return ResultModel.HasExists;
            //}

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

            //if (await _repository.Exists(entity))
            //{
            //return ResultModel.HasExists;
            //}

            var result = await _repository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Crawling()
        {
            for (int i = 18; i < 31; i++)
            {
                var list = await _areaCrawlingHandler.Crawling(i);

                _uow.BeginTransaction();

                foreach (var m in list)
                {
                    var entity = _mapper.Map<AreaEntity>(m);
                    await CrawlingInsert(entity, m.Children);
                }

                _uow.Commit();
            }

            return ResultModel.Success();
        }

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
    }
}
