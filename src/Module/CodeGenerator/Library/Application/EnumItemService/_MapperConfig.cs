using AutoMapper;
using Nm.Lib.Mapper.AutoMapper;
using Nm.Module.CodeGenerator.Application.EnumItemService.ViewModels;
using Nm.Module.CodeGenerator.Domain.EnumItem;

namespace Nm.Module.CodeGenerator.Application.EnumItemService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<EnumItemAddModel, EnumItemEntity>();
            cfg.CreateMap<EnumItemEntity, EnumItemUpdateModel>();
            cfg.CreateMap<EnumItemUpdateModel, EnumItemEntity>();
        }
    }
}
