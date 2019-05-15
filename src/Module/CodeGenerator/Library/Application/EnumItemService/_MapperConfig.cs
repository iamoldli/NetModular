using AutoMapper;
using NetModular.Lib.Mapper.AutoMapper;
using NetModular.Module.CodeGenerator.Application.EnumItemService.ViewModels;
using NetModular.Module.CodeGenerator.Domain.EnumItem;

namespace NetModular.Module.CodeGenerator.Application.EnumItemService
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
