using AutoMapper;
using Tm.Lib.Mapper.AutoMapper;
using Tm.Module.Common.Application.DictService.ViewModels;
using Tm.Module.Common.Domain.Dict;

namespace Tm.Module.Common.Application.DictService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<DictAddModel, DictEntity>();
            cfg.CreateMap<DictEntity, DictUpdateModel>();
            cfg.CreateMap<DictUpdateModel, DictEntity>();
        }
    }
}
