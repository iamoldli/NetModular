using AutoMapper;
using Tm.Lib.Mapper.AutoMapper;
using Tm.Module.PersonnelFiles.Application.UserEducationHistoryService.ViewModels;
using Tm.Module.PersonnelFiles.Domain.UserEducationHistory;

namespace Tm.Module.PersonnelFiles.Application.UserEducationHistoryService
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
