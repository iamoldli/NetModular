using AutoMapper;
using Nm.Lib.Mapper.AutoMapper;
using Nm.Module.PersonnelFiles.Application.UserEducationHistoryService.ViewModels;
using Nm.Module.PersonnelFiles.Domain.UserEducationHistory;

namespace Nm.Module.PersonnelFiles.Application.UserEducationHistoryService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<UserEducationHistoryAddModel, UserEducationHistoryEntity>();
            cfg.CreateMap<UserEducationHistoryEntity, UserEducationHistoryUpdateModel>();
            cfg.CreateMap<UserEducationHistoryUpdateModel, UserEducationHistoryEntity>();
        }
    }
}
