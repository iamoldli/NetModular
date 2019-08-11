using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Nm.Lib.Auth.Abstractions;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Lib.Utils.Core.Models;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.Admin.Application.AccountService;
using Nm.Module.Admin.Application.AccountService.ViewModels;
using Nm.Module.Admin.Domain.Account;
using Nm.Module.Admin.Domain.AccountRole;
using Nm.Module.PersonnelFiles.Application.DepartmentService;
using Nm.Module.PersonnelFiles.Application.UserService.ViewModels;
using Nm.Module.PersonnelFiles.Domain.User;
using Nm.Module.PersonnelFiles.Domain.User.Models;
using Nm.Module.PersonnelFiles.Domain.UserContact;
using Nm.Module.PersonnelFiles.Infrastructure.Options;

namespace Nm.Module.PersonnelFiles.Application.UserService
{
    public class UserService : IUserService
    {
        private readonly ILoginInfo _loginInfo;
        private readonly PersonnelFilesOptions _options;
        private readonly IMapper _mapper;
        private readonly IUserRepository _repository;
        private readonly IAccountService _accountService;
        private readonly IDepartmentService _departmentService;
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IUserContactRepository _contactRepository;

        public UserService(IMapper mapper, IUserRepository repository,
            IOptionsMonitor<PersonnelFilesOptions> optionsMonitor, IAccountService accountService,
            IDepartmentService departmentService, IAccountRepository accountRepository,
            IAccountRoleRepository accountRoleRepository, IUserContactRepository contactRepository, ILoginInfo loginInfo )
        {
            _loginInfo = loginInfo;
            _mapper = mapper;
            _repository = repository;
            _accountService = accountService;
            _departmentService = departmentService;
            _accountRepository = accountRepository;
            _accountRoleRepository = accountRoleRepository;
            _contactRepository = contactRepository;
            _options = optionsMonitor.CurrentValue;
        }

        public async Task<IResultModel> Query(UserQueryModel model)
        {
            model.CID =await _accountService.GetCID(_loginInfo.AccountId);
            

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
                //默认100000
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
                Password = model.Password,
                CID=model.CID
    
            };
            
            var cid=await _accountService.GetCID(_loginInfo.AccountId);
            entity.CID = cid;
            account.CID = cid;
            var result = await _accountService.Add(account);
            if (result.Successful)
            {
                entity.AccountId = result.Data;
                if (await _repository.AddAsync(entity))
                {
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
                account.CID = model.CID;

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
                UserId = id,
                Area = new AreaSelectModel()
            };
            var entity = await _contactRepository.GetByUser(id);
            if (entity != null)
            {
                _mapper.Map(entity, model);
                model.Area.Province = new AreaSelectOptionModel(entity.ProvinceCode);
                model.Area.City = new AreaSelectOptionModel(entity.CityCode);
                model.Area.Area = new AreaSelectOptionModel(entity.AreaCode);
                model.Area.Town = new AreaSelectOptionModel(entity.TownCode);
            }

            return ResultModel.Success(model);
        }

        public async Task<IResultModel> UpdateContact(UserContactUpdateViewModel model)
        {
            var entity = await _contactRepository.GetByUser(model.UserId) ?? new UserContactEntity();

            _mapper.Map(model, entity);

            ClearContactAreaInfo(entity);

            SetContactAreaInfo(entity, model.Area);

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

        /// <summary>
        /// 清除联系方式的区域信息
        /// </summary>
        /// <param name="entity"></param>
        private void ClearContactAreaInfo(UserContactEntity entity)
        {
            entity.ProvinceCode = "";
            entity.ProvinceName = "";
            entity.CityCode = "";
            entity.CityName = "";
            entity.AreaCode = "";
            entity.AreaName = "";
            entity.TownCode = "";
            entity.TownName = "";
        }

        /// <summary>
        /// 设置联系方式的区域信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="areaInfo"></param>
        private void SetContactAreaInfo(UserContactEntity entity, AreaSelectModel areaInfo)
        {
            if (areaInfo.Province == null)
                return;

            entity.ProvinceCode = areaInfo.Province.Code;
            entity.ProvinceName = areaInfo.Province.Name;

            if (areaInfo.City == null)
                return;

            entity.CityCode = areaInfo.City.Code;
            entity.CityName = areaInfo.City.Name;

            if (areaInfo.Area == null)
                return;

            entity.AreaCode = areaInfo.Area.Code;
            entity.AreaName = areaInfo.Area.Name;

            if (areaInfo.Town == null)
                return;

            entity.TownCode = areaInfo.Town.Code;
            entity.TownName = areaInfo.Town.Name;
        }
    }
}
