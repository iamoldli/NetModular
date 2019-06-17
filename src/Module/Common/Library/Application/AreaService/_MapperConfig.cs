using AutoMapper;
using Nm.Lib.Mapper.AutoMapper;
using Nm.Module.Common.Application.AreaService.ViewModels;
using Nm.Module.Common.Domain.Area;
using Nm.Module.Common.Infrastructure.AreaCrawling;

namespace Nm.Module.Common.Application.AreaService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<AreaAddModel, AreaEntity>();
            cfg.CreateMap<AreaEntity, AreaUpdateModel>();
            cfg.CreateMap<AreaUpdateModel, AreaEntity>();
            cfg.CreateMap<AreaCrawlingModel, AreaEntity>();
        }
    }
}
