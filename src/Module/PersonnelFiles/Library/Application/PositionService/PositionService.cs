using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Tm.Lib.Utils.Core.Result;
using Tm.Module.PersonnelFiles.Application.PositionService.ViewModels;
using Tm.Module.PersonnelFiles.Domain.Position;
using Tm.Module.PersonnelFiles.Domain.Position.Models;
using Tm.Module.PersonnelFiles.Domain.User;

namespace Tm.Module.PersonnelFiles.Application.PositionService
{
    public class PositionService : IPositionService
    {
        private readonly IMapper _mapper;
        private readonly IPositionRepository _repository;
        private readonly IUserRepository _userRepository;

        public PositionService(IMapper mapper, IPositionRepository repository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task<IResultModel> Query(PositionQueryModel model)
        {
            var result = new QueryResultModel<PositionEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };
            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(PositionAddModel model)
        {
            var entity = _mapper.Map<PositionEntity>(model);
            if (await _repository.Exists(entity))
            {
                return ResultModel.Failed("岗位名称或编码已存在");
            }

            var result = await _repository.AddAsync(entity);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Delete(Guid id)
        {
            if (await _userRepository.ExistsBindPosition(id))
            {
                return ResultModel.Failed("有人员绑定了该职位，无法删除");
            }

            var result = await _repository.DeleteAsync(id);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Edit(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var model = _mapper.Map<PositionUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(PositionUpdateModel model)
        {
            var entity = await _repository.GetAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(model, entity);

            if (await _repository.Exists(entity))
            {
                return ResultModel.Failed("岗位名称或编码已存在");
            }

            var result = await _repository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Select(Guid departmentId)
        {
            var list = await _repository.QueryByDepartment(departmentId);
            var result = list.Select(m => new OptionResultModel
            {
                Label = m.Name,
                Value = m.Id
            });

            return ResultModel.Success(result);
        }
    }
}
