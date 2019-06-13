using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.PersonnelFiles.Application.UserContactService.ViewModels;
using Nm.Module.PersonnelFiles.Domain.UserContact;
using Nm.Module.PersonnelFiles.Domain.UserContact.Models;

namespace Nm.Module.PersonnelFiles.Application.UserContactService
{
    public class UserContactService : IUserContactService
    {
        private readonly IMapper _mapper;
        private readonly IUserContactRepository _repository;
        public UserContactService(IMapper mapper, IUserContactRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IResultModel> Query(UserContactQueryModel model)
        {
            var result = new QueryResultModel<UserContactEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };
            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(UserContactAddModel model)
        {
            var entity = _mapper.Map<UserContactEntity>(model);
            //if (await _repository.Exists(entity))
            //{
                //return ResultModel.HasExists;
            //}

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

            var model = _mapper.Map<UserContactUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(UserContactUpdateModel model)
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
