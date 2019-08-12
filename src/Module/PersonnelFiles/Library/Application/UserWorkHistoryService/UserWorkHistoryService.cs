using System.Threading.Tasks;
using AutoMapper;
using Tm.Lib.Utils.Core.Result;
using Tm.Module.PersonnelFiles.Application.UserWorkHistoryService.ViewModels;
using Tm.Module.PersonnelFiles.Domain.UserWorkHistory;
using Tm.Module.PersonnelFiles.Domain.UserWorkHistory.Models;

namespace Tm.Module.PersonnelFiles.Application.UserWorkHistoryService
{
    public class UserWorkHistoryService : IUserWorkHistoryService
    {
        private readonly IMapper _mapper;
        private readonly IUserWorkHistoryRepository _repository;
        public UserWorkHistoryService(IMapper mapper, IUserWorkHistoryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IResultModel> Query(UserWorkHistoryQueryModel model)
        {
            var result = new QueryResultModel<UserWorkHistoryEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };
            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(UserWorkHistoryAddModel model)
        {
            var entity = _mapper.Map<UserWorkHistoryEntity>(model);
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

            var model = _mapper.Map<UserWorkHistoryUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(UserWorkHistoryUpdateModel model)
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
