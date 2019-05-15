using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Utils.Core.Models;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.CodeGenerator.Application.EnumItemService.ViewModels;
using NetModular.Module.CodeGenerator.Domain.EnumItem;
using NetModular.Module.CodeGenerator.Domain.EnumItem.Models;
using NetModular.Module.CodeGenerator.Infrastructure.Repositories;

namespace NetModular.Module.CodeGenerator.Application.EnumItemService
{
    public class EnumItemService : IEnumItemService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IEnumItemRepository _repository;

        public EnumItemService(IMapper mapper, IEnumItemRepository repository, IUnitOfWork<CodeGeneratorDbContext> uow)
        {
            _mapper = mapper;
            _repository = repository;
            _uow = uow;
        }

        public async Task<IResultModel> Query(EnumItemQueryModel model)
        {
            var result = new QueryResultModel<EnumItemEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };
            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(EnumItemAddModel model)
        {
            var entity = _mapper.Map<EnumItemEntity>(model);
            if (await _repository.Exists(entity))
            {
                return ResultModel.Failed($"名称({entity.Name})或者值{entity.Value}已存在");
            }

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

            var model = _mapper.Map<EnumItemUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(EnumItemUpdateModel model)
        {
            var entity = await _repository.GetAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(model, entity);

            if (await _repository.Exists(entity))
            {
                return ResultModel.Failed($"名称({entity.Name})或者值{entity.Value}已存在");
            }

            var result = await _repository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> QuerySortList(Guid enumId)
        {
            var model = new SortUpdateModel<Guid>();
            var all = await _repository.QueryByEnum(enumId);
            model.Options = all.Select(m => new SortOptionModel<Guid>()
            {
                Id = m.Id,
                Label = m.Name,
                Sort = m.Value
            }).ToList();

            return ResultModel.Success(model);
        }

        public async Task<IResultModel> UpdateSortList(SortUpdateModel<Guid> model)
        {
            if (model.Options == null || !model.Options.Any())
            {
                return ResultModel.Failed("不包含数据");
            }

            _uow.BeginTransaction();

            foreach (var option in model.Options)
            {
                var entity = await _repository.GetAsync(option.Id);
                if (entity == null)
                {
                    _uow.Rollback();
                    return ResultModel.Failed();
                }

                entity.Value = option.Sort;
                if (!await _repository.UpdateAsync(entity))
                {
                    _uow.Rollback();
                    return ResultModel.Failed();
                }
            }

            _uow.Commit();
            return ResultModel.Success();
        }
    }
}
