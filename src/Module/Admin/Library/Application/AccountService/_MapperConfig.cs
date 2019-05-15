using AutoMapper;
using NetModular.Lib.Mapper.AutoMapper;
using NetModular.Module.Admin.Application.AccountService.ResultModels;
using NetModular.Module.Admin.Application.AccountService.ViewModels;
using NetModular.Module.Admin.Domain.Account;
using NetModular.Module.Admin.Domain.Menu;

namespace NetModular.Module.Admin.Application.AccountService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<AccountEntity, AccountQueryResultModel>();
            cfg.CreateMap<MenuEntity, AccountMenuItem>();
            cfg.CreateMap<AccountAddModel, AccountEntity>();
            cfg.CreateMap<AccountEntity, AccountUpdateModel>();
            cfg.CreateMap<AccountUpdateModel, AccountEntity>();
        }
    }
}
