using AutoMapper;
using Tm.Lib.Mapper.AutoMapper;
using Tm.Module.Admin.Application.AccountService.ResultModels;
using Tm.Module.Admin.Application.AccountService.ViewModels;
using Tm.Module.Admin.Domain.Account;
using Tm.Module.Admin.Domain.Menu;

namespace Tm.Module.Admin.Application.AccountService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<MenuEntity, AccountMenuItem>();
            cfg.CreateMap<AccountAddModel, AccountEntity>().ForMember(m => m.Roles, opt => opt.Ignore());
            cfg.CreateMap<AccountEntity, AccountUpdateModel>();
            cfg.CreateMap<AccountUpdateModel, AccountEntity>().ForMember(m => m.Roles, opt => opt.Ignore());
        }
    }
}
