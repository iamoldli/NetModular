using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.Quartz.Application.JobClassService.ViewModels;
using Nm.Module.Quartz.Domain.JobClass;
using Nm.Module.Quartz.Domain.JobClass.Models;

namespace Nm.Module.Quartz.Application.JobClassService
{
    public class JobClassService : IJobClassService
    {
        private readonly IMapper _mapper;
        private readonly IJobClassRepository _repository;
        public JobClassService(IMapper mapper, IJobClassRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IResultModel> Query(JobClassQueryModel model)
        {
            var result = new QueryResultModel<JobClassEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };
            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(JobClassAddModel model)
        {
            var entity = _mapper.Map<JobClassEntity>(model);
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

            var model = _mapper.Map<JobClassUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(JobClassUpdateModel model)
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
