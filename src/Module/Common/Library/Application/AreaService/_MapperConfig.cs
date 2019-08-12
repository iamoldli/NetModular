using AutoMapper;
using Tm.Lib.Mapper.AutoMapper;
using Tm.Module.Common.Application.AreaService.ViewModels;
using Tm.Module.Common.Domain.Area;
using Tm.Module.Common.Infrastructure.AreaCrawling;

namespace Tm.Module.Common.Application.AreaService
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
