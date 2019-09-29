using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.Admin.Domain.Permission;
using Nm.Module.Admin.Domain.Permission.Models;
using Nm.Module.Admin.Infrastructure.Repositories;

namespace Nm.Module.Admin.Application.PermissionService
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly AdminDbContext _dbContext;

        public PermissionService(IPermissionRepository permissionRepository, AdminDbContext dbContext)
        {
            _permissionRepository = permissionRepository;
            _dbContext = dbContext;
        }

        public async Task<IResultModel> Query(PermissionQueryModel model)
        {
            var queryResult = new QueryResultModel<PermissionEntity>
            {
                Rows = await _permissionRepository.Query(model),
                Total = model.TotalCount
            };

            return ResultModel.Success(queryResult);
        }

        public async Task<IResultModel> Sync(List<PermissionEntity> permissions)
        {
            if (permissions == null || !permissions.Any())
                return ResultModel.Failed("未找到权限信息");

            using (var uow = _dbContext.NewUnitOfWork())
            {
                foreach (var permission in permissions)
                {
                    if (!await _permissionRepository.Exists(permission, uow))
                    {
                        if (!await _permissionRepository.AddAsync(permission, uow))
                        {
                            uow.Rollback();
                            return ResultModel.Failed("同步失败");
                        }
                    }
                    else
                    {
                        if (!await _permissionRepository.UpdateForSync(permission, uow))
                        {
                            uow.Rollback();
                            return ResultModel.Failed("同步失败");
                        }
                    }
                }

                uow.Commit();
            }

            return ResultModel.Success();
        }
    }
}
