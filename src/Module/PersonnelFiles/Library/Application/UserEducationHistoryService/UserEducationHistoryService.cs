using System.Threading.Tasks;
using AutoMapper;
using Tm.Lib.Utils.Core.Result;
using Tm.Module.PersonnelFiles.Application.UserEducationHistoryService.ViewModels;
using Tm.Module.PersonnelFiles.Domain.UserEducationHistory;
using Tm.Module.PersonnelFiles.Domain.UserEducationHistory.Models;

namespace Tm.Module.PersonnelFiles.Application.UserEducationHistoryService
{
    public class UserEducationHistoryService : IUserEducationHistoryService
    {
        private readonly IMapper _mapper;
        private readonly IUserEducationHistoryRepository _repository;
        public UserEducationHistoryService(IMapper mapper, IUserEducationHistoryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IResultModel> Query(UserEducationHistoryQueryModel model)
        {
            var result = new QueryResultModel<UserEducationHistoryEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };
            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(UserEducationHistoryAddModel model)
        {
            var entity = _mapper.Map<UserEducationHistoryEntity>(model);
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

            var model = _mapper.Map<UserEducationHistoryUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(UserEducationHistoryUpdateModel model)
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
    }
}
