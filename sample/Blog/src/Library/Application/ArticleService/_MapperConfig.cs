using AutoMapper;
using Nm.Lib.Mapper.AutoMapper;
using Nm.Module.Blog.Application.ArticleService.ViewModels;
using Nm.Module.Blog.Domain.Article;

namespace Nm.Module.Blog.Application.ArticleService
{
    public class MapperConfig : IMapperConfig
    {
        public void Bind(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ArticleAddModel, ArticleEntity>();
            cfg.CreateMap<ArticleEntity, ArticleUpdateModel>();
            cfg.CreateMap<ArticleUpdateModel, ArticleEntity>();
        }
    }
}
