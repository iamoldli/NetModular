using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Tm.Lib.Utils.Core.Result;
using Tm.Module.Quartz.Application.GroupService.ViewModels;
using Tm.Module.Quartz.Domain.Group;
using Tm.Module.Quartz.Domain.Group.Models;
using Tm.Module.Quartz.Domain.Job;

namespace Tm.Module.Quartz.Application.GroupService
{
    public class GroupService : IGroupService
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _repository;
        private readonly IJobRepository _jobRepository;

        public GroupService(IMapper mapper, IGroupRepository repository, IJobRepository jobRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _jobRepository = jobRepository;
        }

        public async Task<IResultModel> Query(GroupQueryModel model)
        {
            var result = new QueryResultModel<GroupEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };
            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(GroupAddModel model)
        {
            var entity = _mapper.Map<GroupEntity>(model);
            if (await _repository.Exists(entity))
            {
                return ResultModel.Failed("编码");
            }

            var result = await _repository.AddAsync(entity);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Delete(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
            {
                return ResultModel.NotExists;
            }

            if (await _jobRepository.ExistsByGroup(entity.Code))
            {
                return ResultModel.Failed("有任务绑定了该分组，请先删除任务");
            }

            var result = await _repository.DeleteAsync(id);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Select()
        {
            var list = await _repository.GetAllAsync();
            var select = list.Select(m => new OptionResultModel
            {
                Label = m.Name,
                Value = m.Code
            });

            return ResultModel.Success(select);
        }
    }
}
