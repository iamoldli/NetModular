using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.Quartz.Application.JobService.ViewModels;
using Nm.Module.Quartz.Domain.Job;
using Nm.Module.Quartz.Domain.Job.Models;

namespace Nm.Module.Quartz.Application.JobService
{
    public class JobService : IJobService
    {
        private readonly IMapper _mapper;
        private readonly IJobRepository _repository;
        public JobService(IMapper mapper, IJobRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IResultModel> Query(JobQueryModel model)
        {
            var result = new QueryResultModel<JobEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };
            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(JobAddModel model)
        {
            var entity = _mapper.Map<JobEntity>(model);
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

            var model = _mapper.Map<JobUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(JobUpdateModel model)
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
