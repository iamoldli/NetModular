using AutoMapper;
using Nm.Lib.Mapper.AutoMapper;
using Nm.Module.Blog.Application.TagService.ViewModels;
using Nm.Module.Blog.Domain.Tag;

namespace Nm.Module.Blog.Application.TagService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<TagAddModel, TagEntity>();
            cfg.CreateMap<TagEntity, TagUpdateModel>();
            cfg.CreateMap<TagUpdateModel, TagEntity>();
        }
    }
}
