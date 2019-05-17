using AutoMapper;
using Nm.Lib.Mapper.AutoMapper;
using Nm.Module.Admin.Application.AccountService.ResultModels;
using Nm.Module.Admin.Application.AccountService.ViewModels;
using Nm.Module.Admin.Domain.Account;
using Nm.Module.Admin.Domain.Menu;

namespace Nm.Module.Admin.Application.AccountService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<MenuEntity, AccountMenuItem>();
            cfg.CreateMap<AccountAddModel, AccountEntity>();
            cfg.CreateMap<AccountEntity, AccountUpdateModel>();
            cfg.CreateMap<AccountUpdateModel, AccountEntity>();
        }
    }
}
