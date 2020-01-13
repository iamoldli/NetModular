using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetModular.Lib.Cache.Abstractions;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Lib.Utils.Core.SystemConfig;
using NetModular.Module.Admin.Application.PermissionService.ResultModels;
using NetModular.Module.Admin.Domain.ModuleInfo;
using NetModular.Module.Admin.Domain.Permission;
using NetModular.Module.Admin.Domain.Permission.Models;
using NetModular.Module.Admin.Infrastructure;
using NetModular.Module.Admin.Infrastructure.Repositories;

namespace NetModular.Module.Admin.Application.PermissionService
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _repository;
        private readonly IModuleInfoRepository _moduleInfoRepository;
        private readonly AdminDbContext _dbContext;
        private readonly SystemConfigModel _systemConfig;
        private readonly ICacheHandler _cacheHandler;

        public PermissionService(IPermissionRepository permissionRepository, AdminDbContext dbContext, IModuleInfoRepository moduleInfoRepository, SystemConfigModel systemConfig, ICacheHandler cacheHandler)
        {
            _repository = permissionRepository;
            _dbContext = dbContext;
            _moduleInfoRepository = moduleInfoRepository;
            _systemConfig = systemConfig;
            _cacheHandler = cacheHandler;
        }

        public async Task<IResultModel> Query(PermissionQueryModel model)
        {
            var queryResult = new QueryResultModel<PermissionEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };

            return ResultModel.Success(queryResult);
        }

        public async Task<IResultModel> Sync(List<PermissionEntity> permissions)
        {
            if (permissions == null || !permissions.Any())
                return ResultModel.Failed("未找到权限信息");

            using var uow = _dbContext.NewUnitOfWork();

            //先清除已有权限信息
            await _repository.ClearAsync(uow);

            foreach (var permission in permissions)
            {
                if (!await _repository.AddAsync(permission, uow))
                {
                    uow.Rollback();
                    return ResultModel.Failed("同步失败");
                }
            }

            uow.Commit();

            return ResultModel.Success();
        }

        public async Task<IResultModel> GetTree()
        {
            //先取缓存
            if (_cacheHandler.TryGetValue(CacheKeys.PermissionTree, out TreeResultModel<int, PermissionTreeResultModel> root))
            {
                return ResultModel.Success(root);
            }

            var id = 0;
            root = new TreeResultModel<int, PermissionTreeResultModel>
            {
                Id = id,
                Label = _systemConfig.Base.Title,
                Item = new PermissionTreeResultModel()
            };
            root.Path.Add(root.Label);

            var modules = _moduleInfoRepository.GetAllAsync();
            var permissions = await _repository.GetAllAsync();
            //模块
            foreach (var module in await modules)
            {
                var moduleNode = new TreeResultModel<int, PermissionTreeResultModel>
                {
                    Id = ++id,
                    Label = module.Name,
                    Item = new PermissionTreeResultModel
                    {
                        Label = module.Name,
                        Code = module.Code
                    }
                };

                moduleNode.Path.AddRange(root.Path);
                moduleNode.Path.Add(module.Name);

                var controllers = permissions.Where(m => m.ModuleCode.EqualsIgnoreCase(module.Code)).DistinctBy(m => m.Controller);
                //控制器
                foreach (var controller in controllers)
                {
                    var controllerName = controller.Name.Split('_')[0];
                    var controllerNode = new TreeResultModel<int, PermissionTreeResultModel>
                    {
                        Id = ++id,
                        Label = controllerName,
                        Item = new PermissionTreeResultModel
                        {
                            Label = controllerName,
                            Code = controller.Controller
                        }
                    };

                    controllerNode.Path.AddRange(moduleNode.Path);
                    controllerNode.Path.Add(controllerName);

                    var permissionList = permissions.Where(m => m.ModuleCode.EqualsIgnoreCase(module.Code) && m.Controller.EqualsIgnoreCase(controller.Controller));
                    //权限
                    foreach (var permission in permissionList)
                    {
                        var permissionName = permission.Name.Contains("_") ? permission.Name.Split('_')[1] : permission.Name;
                        var permissionNode = new TreeResultModel<int, PermissionTreeResultModel>
                        {
                            Id = ++id,
                            Label = permissionName,
                            Item = new PermissionTreeResultModel
                            {
                                Label = permissionName,
                                Code = permission.Code,
                                IsPermission = true
                            }
                        };

                        permissionNode.Path.AddRange(controllerNode.Path);
                        permissionNode.Path.Add(permissionName);

                        controllerNode.Children.Add(permissionNode);
                    }

                    moduleNode.Children.Add(controllerNode);
                }
                root.Children.Add(moduleNode);
            }

            await _cacheHandler.SetAsync(CacheKeys.PermissionTree, root);
            return ResultModel.Success(root);
        }
    }
}
