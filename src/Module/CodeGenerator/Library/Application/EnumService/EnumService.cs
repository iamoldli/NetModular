using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.CodeGenerator.Application.EnumService.ViewModels;
using NetModular.Module.CodeGenerator.Domain.Enum;
using NetModular.Module.CodeGenerator.Domain.Enum.Models;
using NetModular.Module.CodeGenerator.Domain.Property;

namespace NetModular.Module.CodeGenerator.Application.EnumService
{
    public class EnumService : IEnumService
    {
        private readonly IMapper _mapper;
        private readonly IEnumRepository _repository;
        private readonly IPropertyRepository _propertyRepository;
        public EnumService(IMapper mapper, IEnumRepository repository, IPropertyRepository propertyRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _propertyRepository = propertyRepository;
        }

        public async Task<IResultModel> Query(EnumQueryModel model)
        {
            var result = new QueryResultModel<EnumEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };
            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(EnumAddModel model)
        {
            var entity = _mapper.Map<EnumEntity>(model);
            if (await _repository.Exists(entity))
            {
                return ResultModel.Failed($"枚举({entity.Name})已存在");
            }

            var result = await _repository.AddAsync(entity);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Delete(Guid id)
        {
            if (await _propertyRepository.ExistsByEnum(id))
            {
                return ResultModel.Failed("有数据关联了该枚举，无法删除");
            }

            var result = await _repository.DeleteAsync(id);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Edit(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var model = _mapper.Map<EnumUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(EnumUpdateModel model)
        {
            var entity = await _repository.GetAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(model, entity);

            if (await _repository.Exists(entity))
            {
                return ResultModel.Failed($"枚举({entity.Name})已存在");
            }

            var result = await _repository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Select()
        {
            var all = await _repository.GetAllAsync();
            var list = all.Select(m => new OptionResultModel
            {
                Label = $"{m.Name}({m.Remarks})",
                Value = m.Id
            }).ToList();
            return ResultModel.Success(list);
        }
    }
}
