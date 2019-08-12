using AutoMapper;
using Tm.Lib.Mapper.AutoMapper;
using Tm.Module.PersonnelFiles.Application.UserWorkHistoryService.ViewModels;
using Tm.Module.PersonnelFiles.Domain.UserWorkHistory;

namespace Tm.Module.PersonnelFiles.Application.UserWorkHistoryService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<UserWorkHistoryAddModel, UserWorkHistoryEntity>();
            cfg.CreateMap<UserWorkHistoryEntity, UserWorkHistoryUpdateModel>();
            cfg.CreateMap<UserWorkHistoryUpdateModel, UserWorkHistoryEntity>();
        }
    }
}
