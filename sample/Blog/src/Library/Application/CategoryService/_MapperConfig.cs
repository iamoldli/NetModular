using AutoMapper;
using Nm.Lib.Mapper.AutoMapper;
using Nm.Module.Blog.Application.CategoryService.ViewModels;
using Nm.Module.Blog.Domain.Category;

namespace Nm.Module.Blog.Application.CategoryService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<CategoryAddModel, CategoryEntity>();
            cfg.CreateMap<CategoryEntity, CategoryUpdateModel>();
            cfg.CreateMap<CategoryUpdateModel, CategoryEntity>();
        }
    }
}
