using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.Admin.Application.AccountService;
using Nm.Module.Admin.Application.AccountService.ViewModels;
using Nm.Module.Admin.Domain.Account;
using Nm.Module.Admin.Domain.AccountRole;
using Nm.Module.Admin.Infrastructure.Repositories;
using Nm.Module.PersonnelFiles.Application.DepartmentService;
using Nm.Module.PersonnelFiles.Application.UserService.ViewModels;
using Nm.Module.PersonnelFiles.Domain.User;
using Nm.Module.PersonnelFiles.Domain.User.Models;
using Nm.Module.PersonnelFiles.Domain.UserContact;
using Nm.Module.PersonnelFiles.Infrastructure.Options;
using Nm.Module.PersonnelFiles.Infrastructure.Repositories;

namespace Nm.Module.PersonnelFiles.Application.UserService
{
    public class UserService : IUserService
    {
        private readonly PersonnelFilesOptions _options;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IUnitOfWork _adminUow;
        private readonly IUserRepository _repository;
        private readonly IAccountService _accountService;
        private readonly IDepartmentService _departmentService;
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IUserContactRepository _contactRepository;

        public UserService(IMapper mapper, IUserRepository repository,
            IOptionsMonitor<PersonnelFilesOptions> optionsMonitor, IUnitOfWork<AdminDbContext> adminUow,
            IUnitOfWork<PersonnelFilesDbContext> uow, IAccountService accountService,
            IDepartmentService departmentService, IAccountRepository accountRepository,
            IAccountRoleRepository accountRoleRepository, IUserContactRepository contactRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _adminUow = adminUow;
            _uow = uow;
            _accountService = accountService;
            _departmentService = departmentService;
            _accountRepository = accountRepository;
            _accountRoleRepository = accountRoleRepository;
            _contactRepository = contactRepository;
            _options = optionsMonitor.CurrentValue;
        }

        public async Task<IResultModel> Query(UserQueryModel model)
        {
            var result = new QueryResultModel<UserEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };
            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(UserAddModel model)
        {
            var entity = _mapper.Map<UserEntity>(model);
            var maxJobNumber = await _repository.GetMaxJobNumber();
            if (maxJobNumber < 1)
            {
                maxJobNumber = _options.UserInitialJobNumber;
            }

            if (maxJobNumber < 1)
            {
                //д╛хо100000
                maxJobNumber = 100000;
            }

            entity.JobNo = maxJobNumber + 1;

            var account = new AccountAddModel
            {
                Type = 1,
                UserName = model.UserName,
                Phone = model.Phone,
                Email = model.Email,
                Name = model.Name,
                Roles = model.Roles,
                Password = model.Password
            };

            _adminUow.BeginTransaction();
            var result = await _accountService.Add(account);
            if (result.Successful)
            {
                entity.AccountId = result.Data;
                _uow.BeginTransaction();
                if (await _repository.AddAsync(entity))
                {
                    _adminUow.Commit();
                    _uow.Commit();
                    return ResultModel.Success();
                }
            }

            return result;
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

            var model = _mapper.Map<UserUpdateModel>(entity);
            if (!model.DepartmentId.IsEmpty())
                model.DeptFullPath = await _departmentService.GetFullPath(entity.DepartmentId);

            var roles = await _accountRoleRepository.QueryRole(entity.AccountId);
            model.Roles = roles.Select(m => m.Id).ToList();

            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(UserUpdateModel model)
        {
            var entity = await _repository.GetAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(model, entity);

            var result = await _repository.UpdateAsync(entity);
            if (result)
            {
                var account = await _accountRepository.GetAsync(entity.AccountId);
                account.Phone = model.Phone;
                account.Email = model.Email;

                result = await _accountRepository.UpdateAsync(account);
            }

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> EditContact(Guid id)
        {
            var user = await _repository.GetAsync(id);
            if (user == null)
                return ResultModel.NotExists;

            var model = new UserContactUpdateViewModel
            {
                UserId = id
            };
            var entity = await _contactRepository.GetByUser(id);
            if (entity != null)
            {
                _mapper.Map(entity, model);
            }

            return ResultModel.Success(model);
        }

        public async Task<IResultModel> UpdateContact(UserContactUpdateViewModel model)
        {
            var entity = await _contactRepository.GetByUser(model.UserId);
            if (entity == null)
            {
                entity = new UserContactEntity();
            }

            _mapper.Map(model, entity);

            bool result;
            if (entity.Id > 0)
            {
                result = await _contactRepository.UpdateAsync(entity);
            }
            else
            {
                result = await _contactRepository.AddAsync(entity);
            }

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> ContactDetails(Guid id)
        {
            var user = await _repository.GetAsync(id);
            if (user == null)
                return ResultModel.NotExists;

            var entity = await _contactRepository.GetByUser(id);
            entity = entity ?? new UserContactEntity();
            return ResultModel.Success(entity);
        }
    }
}
