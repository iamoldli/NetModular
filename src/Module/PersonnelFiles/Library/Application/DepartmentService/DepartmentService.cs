using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.PersonnelFiles.Application.DepartmentService.ResultModels;
using Nm.Module.PersonnelFiles.Application.DepartmentService.ViewModels;
using Nm.Module.PersonnelFiles.Domain.Company;
using Nm.Module.PersonnelFiles.Domain.Department;
using Nm.Module.PersonnelFiles.Domain.Department.Models;
using Nm.Module.PersonnelFiles.Domain.User;

namespace Nm.Module.PersonnelFiles.Application.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;

        public DepartmentService(IMapper mapper, IDepartmentRepository repository, IUserRepository userRepository, ICompanyRepository companyRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _userRepository = userRepository;
            _companyRepository = companyRepository;
        }

        public async Task<IResultModel> GetTree(Guid companyId)
        {
            var list = new List<DepartmentTreeResultModel>();

            var all = await _repository.QueryAllByCompany(companyId);
            foreach (var entity in all.Where(m => m.ParentId == Guid.Empty).OrderBy(m => m.Sort))
            {
                list.Add(Entity2Tree(entity, all));
            }

            return ResultModel.Success(list);
        }

        private DepartmentTreeResultModel Entity2Tree(DepartmentEntity entity, IList<DepartmentEntity> all)
        {
            var model = new DepartmentTreeResultModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Sort = entity.Sort
            };

            var children = all.Where(m => m.ParentId == model.Id).OrderBy(m => m.Sort);
            foreach (var child in children)
            {
                model.Children.Add(Entity2Tree(child, all));
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
            {
                return ResultModel.HasExists;
            }

            var result = await _repository.AddAsync(entity);
            if (result)
            {
                return ResultModel.Success(entity.Id);
            }

            return ResultModel.Failed();
        }

        public async Task<IResultModel> Delete(Guid id)
        {
            if (await _repository.ExistsChildren(id))
            {
                return ResultModel.Failed("请先删除子部门");
            }

            if (await _userRepository.ExistsBindDept(id))
            {
                return ResultModel.Failed("有人员绑定了该部门，无法删除");
            }

            var result = await _repository.DeleteAsync(id);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Edit(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var model = _mapper.Map<DepartmentUpdateModel>(entity);
            if (model.Leader.NotEmpty())
            {
                var leader = await _userRepository.GetAsync(model.Leader);
                if (leader != null)
                {
                    model.LeaderName = leader.Name;
                }
            }
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(DepartmentUpdateModel model)
        {
            var entity = await _repository.GetAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(model, entity);

            if (await _repository.Exists(entity))
            {
                return ResultModel.HasExists;
            }

            var result = await _repository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<string> GetFullPath(Guid id)
        {
            var path = string.Empty;
            var entity = await _repository.GetAsync(id);
            if (entity == null)
                return path;

            path = entity.Name;
            if (!entity.ParentId.IsEmpty())
            {
                path = (await GetFullPath(entity.ParentId)) + " / " + path;
            }
            else
            {
                var company = await _companyRepository.GetAsync(entity.CompanyId);
                if (company != null)
                {
                    path = company.Name + " / " + path;
                }
            }

            return path;
        }
    }
}
