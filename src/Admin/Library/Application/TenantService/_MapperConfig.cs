using AutoMapper;
using NetModular.Lib.Mapper.AutoMapper;
using NetModular.Module.Admin.Application.TenantService.ViewModels;
using NetModular.Module.Admin.Domain.Tenant;

namespace NetModular.Module.Admin.Application.TenantService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<TenantAddModel, TenantEntity>();
            cfg.CreateMap<TenantEntity, TenantUpdateModel>();
            cfg.CreateMap<TenantUpdateModel, TenantEntity>();
        }
    }
}
