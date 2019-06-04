using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.Admin.Application.DepartmentService.ResultModels;
using Nm.Module.Admin.Application.DepartmentService.ViewModels;
using Nm.Module.Admin.Domain.Department;
using Nm.Module.Admin.Domain.Department.Models;

namespace Nm.Module.Admin.Application.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentRepository _repository;

        public DepartmentService(IDepartmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IResultModel> GetTree()
        {
            var treeModel = new List<DepartmentTreeResultModel>();

            var all = await _repository.GetAllAsync();
            var father = all.Where(m => m.ParentId == default)
                .OrderBy(m => m.Sort).ToList();

            foreach (var dept in father)
            {
                treeModel.Add(Dept2TreeModel(all, dept));
            }

            return ResultModel.Success(treeModel);
        }

        private DepartmentTreeResultModel Dept2TreeModel(IList<DepartmentEntity> all, DepartmentEntity dept)
        {
            var model = new DepartmentTreeResultModel
            {
                Id = dept.Id,
                ParentId = dept.ParentId,
                Name = dept.Name,
                Sort = dept.Sort
            };

            if (!all.Any())
                return model;

            var children = all.Where(m => m.ParentId == dept.Id).OrderBy(m => m.Sort).ToList();
            if (children.Any())
            {
                foreach (var child in children)
                {
                    model.Children.Add(Dept2TreeModel(all, child));
                }
            }

            return model;
        }

        public async Task<IResultModel> Query(DepartmentQueryModel model)
        {
            var result = new QueryResultModel<DepartmentEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(DepartmentAddModel model)
        {
            var entity = _mapper.Map<DepartmentEntity>(model);

            if (await _repository.Exists(entity))
                return ResultModel.HasExists;

            var result = await _repository.AddAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Delete(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
                return ResultModel.Failed("部门不存在");

            if (await _repository.ExistsChild(id))
                return ResultModel.Failed($"当前部门({entity.Name})存在子部门，请先删除子部门");

            return ResultModel.Failed();
        }

        public Task<IResultModel> Edit(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IResultModel> Update(DepartmentUpdateModel model)
        {
            throw new NotImplementedException();
        }
    }
}
