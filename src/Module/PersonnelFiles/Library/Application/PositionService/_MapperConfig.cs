using AutoMapper;
using Nm.Lib.Mapper.AutoMapper;
using Nm.Module.PersonnelFiles.Application.PositionService.ViewModels;
using Nm.Module.PersonnelFiles.Domain.Position;

namespace Nm.Module.PersonnelFiles.Application.PositionService
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
