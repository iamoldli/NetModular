using AutoMapper;
using Nm.Lib.Mapper.AutoMapper;
using Nm.Module.PersonnelFiles.Application.UserWorkHistoryService.ViewModels;
using Nm.Module.PersonnelFiles.Domain.UserWorkHistory;

namespace Nm.Module.PersonnelFiles.Application.UserWorkHistoryService
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
