using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Nm.Lib.Utils.Core.Models;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.CodeGenerator.Application.ModelPropertyService.ViewModels;
using Nm.Module.CodeGenerator.Domain.Class;
using Nm.Module.CodeGenerator.Domain.ModelProperty;
using Nm.Module.CodeGenerator.Domain.ModelProperty.Models;
using Nm.Module.CodeGenerator.Domain.Property;
using Nm.Module.CodeGenerator.Infrastructure.Repositories;

namespace Nm.Module.CodeGenerator.Application.ModelPropertyService
{
    public class ModelPropertyService : IModelPropertyService
    {
        private readonly IMapper _mapper;
        private readonly IModelPropertyRepository _repository;
        private readonly IClassRepository _classRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly CodeGeneratorDbContext _dbContext;

        public ModelPropertyService(IMapper mapper, IClassRepository classRepository, IModelPropertyRepository repository, IPropertyRepository propertyRepository, CodeGeneratorDbContext dbContext)
        {
            _mapper = mapper;
            _classRepository = classRepository;
            _repository = repository;
            _propertyRepository = propertyRepository;
            _dbContext = dbContext;
        }

        public async Task<IResultModel> Query(ModelPropertyQueryModel model)
        {
            var result = new QueryResultModel<ModelPropertyEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };
            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(ModelPropertyAddModel model)
        {
            var classEntity = await _classRepository.GetAsync(model.ClassId);
            if (classEntity == null)
                return ResultModel.Failed("关联类不存在");

            var entity = _mapper.Map<ModelPropertyEntity>(model);
            entity.ProjectId = classEntity.ProjectId;

            if (await _repository.Exists(entity))
            {
                return ResultModel.Failed($"属性名称({entity.Name})已存在");
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

            var model = _mapper.Map<ModelPropertyUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(ModelPropertyUpdateModel model)
        {
            var entity = await _repository.GetAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(model, entity);

            if (await _repository.Exists(entity))
            {
                return ResultModel.Failed($"属性名称({entity.Name})已存在");
            }

            var result = await _repository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> QuerySortList(ModelPropertyQueryModel model)
        {
            var result = new SortUpdateModel<Guid>();
            var all = await _repository.QueryAll(model);
            result.Options = all.OrderBy(m => m.Sort).Select(m => new SortOptionModel<Guid>()
            {
                Id = m.Id,
                Label = m.Name,
                Sort = m.Sort
            }).ToList();

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> UpdateSortList(SortUpdateModel<Guid> model)
        {
            if (model.Options == null || !model.Options.Any())
            {
                return ResultModel.Failed("不包含数据");
            }

            using (var uow = _dbContext.NewUnitOfWork())
            {
                foreach (var option in model.Options)
                {
                    var entity = await _repository.GetAsync(option.Id, uow);
                    if (entity == null)
                    {
                        uow.Rollback();
                        return ResultModel.Failed();
                    }

                    entity.Sort = option.Sort;
                    if (!await _repository.UpdateAsync(entity, uow))
                    {
                        uow.Rollback();
                        return ResultModel.Failed();
                    }
                }

                uow.Commit();
            }

            return ResultModel.Success();
        }

        public async Task<IResultModel> UpdateNullable(ModelPropertyUpdateNullableModel model)
        {
            var entity = await _repository.GetAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            entity.Nullable = model.Nullable;
            var result = await _repository.UpdateAsync(entity);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Select(ModelPropertyQueryModel model)
        {
            var all = await _repository.QueryAll(model);
            var result = all.OrderBy(m => m.Sort).Select(m => new OptionResultModel()
            {
                Label = $"{m.Name}({m.Remarks})",
                Value = m.Name,
                Data = m.Id
            }).ToList();

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> ImportFromEntity(ModelPropertyImportModel model)
        {
            if (model.Ids == null || !model.Ids.Any())
                return ResultModel.Failed("请选择要导入的属性");

            var entities = new List<ModelPropertyEntity>();
            foreach (var pId in model.Ids)
            {
                var pEntity = await _propertyRepository.GetAsync(pId);
                entities.Add(new ModelPropertyEntity
                {
                    ClassId = model.ClassId,
                    Name = pEntity.Name,
                    ModelType = model.ModelType,
                    EnumId = pEntity.EnumId,
                    Nullable = pEntity.Nullable,
                    Remarks = pEntity.Remarks,
                    Type = pEntity.Type,
                    Sort = model.Sort
                });

                model.Sort++;
            }

            var result = await _repository.AddAsync(entities);
            return ResultModel.Result(result);
        }
    }
}
