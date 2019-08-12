using AutoMapper;
using Tm.Lib.Mapper.AutoMapper;
using Tm.Module.PersonnelFiles.Application.PositionService.ViewModels;
using Tm.Module.PersonnelFiles.Domain.Position;

namespace Tm.Module.PersonnelFiles.Application.PositionService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<PositionAddModel, PositionEntity>();
            cfg.CreateMap<PositionEntity, PositionUpdateModel>();
            cfg.CreateMap<PositionUpdateModel, PositionEntity>();
        }
    }
}
