using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tm.Lib.Utils.Core.Result;
using Tm.Module.Admin.Domain.Permission;
using Tm.Module.Admin.Domain.Permission.Models;

namespace Tm.Module.Admin.Application.PermissionService
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
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

            using (var tran = _permissionRepository.BeginTransaction())
            {
                foreach (var permission in permissions)
                {
                    if (!await _permissionRepository.Exists(permission, tran))
                    {
                        if (!await _permissionRepository.AddAsync(permission, tran))
                        {
                            tran.Rollback();
                            return ResultModel.Failed("同步失败");
                        }
                    }
                    else
                    {
                        if (!await _permissionRepository.UpdateForSync(permission, tran))
                        {
                            tran.Rollback();
                            return ResultModel.Failed("同步失败");
                        }
                    }
                }

                tran.Commit();
            }

            return ResultModel.Success();
        }
    }
}
