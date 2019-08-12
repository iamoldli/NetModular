using AutoMapper;
using Tm.Lib.Mapper.AutoMapper;
using Tm.Module.CodeGenerator.Application.EnumItemService.ViewModels;
using Tm.Module.CodeGenerator.Domain.EnumItem;

namespace Tm.Module.CodeGenerator.Application.EnumItemService
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
