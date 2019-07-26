using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Nm.Lib.Utils.Core.Models;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.CodeGenerator.Application.PropertyService.ViewModels;
using Nm.Module.CodeGenerator.Domain.Class;
using Nm.Module.CodeGenerator.Domain.Property;
using Nm.Module.CodeGenerator.Domain.Property.Models;

namespace Nm.Module.CodeGenerator.Application.PropertyService
{
    public class PropertyService : IPropertyService
    {
        private readonly IMapper _mapper;
        private readonly IPropertyRepository _repository;
        private readonly IClassRepository _classRepository;

        public PropertyService(IMapper mapper, IPropertyRepository repository, IClassRepository classRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _classRepository = classRepository;
        }

        public async Task<IResultModel> Query(PropertyQueryModel model)
        {
            var result = new QueryResultModel<PropertyEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };
            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(PropertyAddModel model)
        {
            var classEntity = await _classRepository.GetAsync(model.ClassId);
            if (classEntity == null)
                return ResultModel.Failed("关联类不存在");

            var entity = _mapper.Map<PropertyEntity>(model);
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

            var model = _mapper.Map<PropertyUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(PropertyUpdateModel model)
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

        public async Task<IResultModel> QuerySortList(Guid classId)
        {
            var model = new SortUpdateModel<Guid>();
            var all = await _repository.QueryByClass(classId);
            model.Options = all.Where(m => !m.IsInherit).Select(m => new SortOptionModel<Guid>()
            {
                Id = m.Id,
                Label = m.Name,
                Sort = m.Sort
            }).ToList();

            return ResultModel.Success(model);
        }

        public async Task<IResultModel> UpdateSortList(SortUpdateModel<Guid> model)
        {
            if (model.Options == null || !model.Options.Any())
            {
                return ResultModel.Failed("不包含数据");
            }

            using (var tran = _repository.BeginTransaction())
            {
                foreach (var option in model.Options)
                {
                    var entity = await _repository.GetAsync(option.Id, tran);
                    if (entity == null)
                    {
                        tran.Rollback();
                        return ResultModel.Failed();
                    }

                    entity.Sort = option.Sort;
                    if (!await _repository.UpdateAsync(entity, tran))
                    {
                        tran.Rollback();
                        return ResultModel.Failed();
                    }
                }

                tran.Commit();
            }

            return ResultModel.Success();
        }

        public async Task<IResultModel> UpdateNullable(PropertyUpdateNullableModel model)
        {
            var entity = await _repository.GetAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            entity.Nullable = model.Nullable;
            var result = await _repository.UpdateAsync(entity);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Select(Guid classId)
        {
            var all = await _repository.QueryByClass(classId);
            var result = all.Select(m => new OptionResultModel()
            {
                Label = $"{m.Name}({m.Remarks})",
                Value = m.Name,
                Data = m.Id
            }).ToList();

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> UpdateShowInList(PropertyUpdateShowInListModel model)
        {
            var entity = await _repository.GetAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            entity.ShowInList = model.ShowInList;
            var result = await _repository.UpdateAsync(entity);
            return ResultModel.Result(result);
        }
    }
}
