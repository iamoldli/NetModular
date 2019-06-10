using AutoMapper;
using Nm.Lib.Mapper.AutoMapper;
using Nm.Module.Common.Application.DictService.ViewModels;
using Nm.Module.Common.Domain.Dict;

namespace Nm.Module.Common.Application.DictService
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
