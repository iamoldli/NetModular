using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Data.Common.Domain;
using Data.Common.Repository;
using Xunit;

namespace Data.PostgreSQL.Test
{
    public class RepositoryTest : DbContextTest
    {
        private readonly IArticleRepository _articleRepository;

        public RepositoryTest()
        {
            _articleRepository = new ArticleRepository(DbContext);
        }

        [Fact]
        public async Task<ArticleEntity> AddTest()
        {
            var article = new ArticleEntity
            {
                Title = "ASP.NET Core",
                Body = "ASP.NET CoreASP.NET CoreASP.NET CoreASP.NET CoreASP.NET Core",
                MediaType = MediaType.Picture,
                CategoryId = CategoryId,
                CreatedTime = DateTime.Now
            };

            var result = await _articleRepository.AddAsync(article);

            Assert.True(result);
            return article;
        }

        [Fact]
        public async Task BatchAddTest()
        {
            await AddTest();

            var list = new List<ArticleEntity>();
            for (int i = 0; i < 10000; i++)
            {
                var article = new ArticleEntity
                {
                    Title = "ASP.NET Core",
                    Body = "ASP.NET CoreASP.NET CoreASP.NET CoreASP.NET CoreASP.NET Core",
                    MediaType = MediaType.Picture,
                    CategoryId = CategoryId,
                    CreatedTime = DateTime.Now
                };
                list.Add(article);
            }

            var sw = new Stopwatch();
            sw.Start();

            await _articleRepository.AddAsync(list);

            sw.Stop();

            var count = await _articleRepository.Count();

            Assert.True(count == 10001);
        }

        [Fact]
        public async void DeleteTest()
        {
            var article = await AddTest();

            var result = await _articleRepository.DeleteAsync(article.Id);

            Assert.True(result);
        }

        [Fact]
        public async void UpdateTest()
        {
            var article = await AddTest();
            article.Title = "测试";

            var result = await _articleRepository.UpdateAsync(article);

            Assert.True(result);
        }

        [Fact]
        public async void GetTest()
        {
            var article = await AddTest();

            var result = await _articleRepository.GetAsync(article.Id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, article.Id);
        }

        [Fact]
        public async void GetAllTest()
        {
            await BatchAddTest();

            var list = await _articleRepository.GetAllAsync();

            Assert.Equal(10001, list.Count);
        }

        [Fact]
        public async void SoftDeleteTest()
        {
            var article = await AddTest();

            await _articleRepository.SoftDeleteAsync(article.Id);

            var article1 = await _articleRepository.GetAsync(article.Id);

            Assert.Null(article1);
        }
    }
}
