using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Data.Common.Domain;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Pagination;
using Xunit;

namespace Data.MySql.Tests
{
    public class DbSetTests : DbContextTests
    {
        private readonly IDbSet<ArticleEntity> _db;

        public DbSetTests()
        {
            _db = DbContext.Set<ArticleEntity>();
            BatchInsert(100).GetAwaiter().GetResult();
        }

        private async Task BatchInsert(int count)
        {
            var r = new Random();
            var list = new List<ArticleEntity>();
            for (int i = 0; i < count; i++)
            {
                var article = new ArticleEntity
                {
                    Title = "ASP.NET Core",
                    Body = "ASP.NET CoreASP.NET CoreASP.NET CoreASP.NET CoreASP.NET Core",
                    MediaType = r.Next(0, 1) == 0 ? MediaType.Picture : MediaType.Video,
                    ReadCount = r.Next(1, 1000),
                    Published = r.Next(0, 1) == 0,
                    CategoryId = CategoryId,
                    CreatedTime = DateTime.Now
                };
                list.Add(article);
            }

            var sw = new Stopwatch();
            sw.Start();

            await _db.BatchInsertAsync(list);

            sw.Stop();

            var s = sw.ElapsedMilliseconds;
        }

        [Fact]
        public async Task<ArticleEntity> InsertTest()
        {
            var article = new ArticleEntity
            {
                Title = "ASP.NET Core",
                Body = "ASP.NET CoreASP.NET CoreASP.NET CoreASP.NET CoreASP.NET Core",
                MediaType = MediaType.Picture,
                CategoryId = CategoryId,
                CreatedTime = DateTime.Now
            };

            var result = await _db.InsertAsync(article);

            Assert.True(result);
            return article;
        }

        [Fact]
        public async Task BatchInsertTest()
        {
            await BatchInsert(100000);

            var count = await _db.Find().CountAsync();

            Assert.True(count == 10100);
        }

        [Fact]
        public async void DeleteTest()
        {
            var article = await InsertTest();

            var result = await _db.DeleteAsync(article.Id);

            Assert.True(result);
        }

        [Fact]
        public async void UpdateTest()
        {
            var article = await InsertTest();
            article.Title = "测试";

            var result = await _db.UpdateAsync(article);

            Assert.True(result);
        }

        [Fact]
        public async void GetTest()
        {
            var article = await InsertTest();

            var result = await _db.GetAsync(article.Id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, article.Id);
        }

        [Fact]
        public async void SoftDeleteTest()
        {
            var article = await InsertTest();

            await _db.SoftDeleteAsync(article.Id);

            var article1 = await _db.GetAsync(article.Id);

            Assert.Null(article1);
        }

        [Fact]
        public async void ExistsTest()
        {
            var article = await InsertTest();

            var result = await _db.ExistsAsync(article.Id);

            Assert.True(result);
        }

        [Fact]
        public async void QueryTest()
        {
            var query = _db.Find(m => m.Id > 5 && m.Id < 10);
            var sql = query.ToSql();

            var list = await query.ToListAsync();

            Assert.Equal(4, list.Count);
        }

        [Fact]
        public async void WhereTest()
        {
            //暂时布尔类型必须指定具体的值，如m => m.Published == true，不能省略为m => m.Published
            var query = _db.Find().Where(m => m.Published && m.Id > 10);

            //var sql = query.ToSql();
            //SELECT `Id` AS `Id`,`CategoryId` AS `CategoryId`,`Title` AS `Title`,`MediaType` AS `MediaType`,`Body` AS `Body`,
            //`Published` AS `Published`,`ReadCount` AS `ReadCount`,`CreatedTime` AS `CreatedTime`,`CreatedBy` AS `CreatedBy`,
            //`ModifiedTime` AS `ModifiedTime`,`ModifiedBy` AS `ModifiedBy`,`Deleted` AS `Deleted`,`DeletedTime` AS `DeletedTime`,
            //`DeletedBy` AS `DeletedBy` FROM  `nm_blog`.`Article`
            //WHERE (`Published` AND (`Id` > @P1)) AND `Deleted`=0 

            var list = await query.ToListAsync();

            Assert.True(list.Count > 0);
        }

        [Fact]
        public async void WhereIfTest()
        {
            var readCount = 10;
            var query = _db.Find().WhereIf(readCount > 1, m => m.ReadCount > readCount);

            //var sql = query.ToSql();
            //SELECT `Id` AS `Id`,`CategoryId` AS `CategoryId`,`Title` AS `Title`,`MediaType` AS `MediaType`,`Body` AS `Body`,
            //`Published` AS `Published`,`ReadCount` AS `ReadCount`,`CreatedTime` AS `CreatedTime`,`CreatedBy` AS `CreatedBy`,
            //`ModifiedTime` AS `ModifiedTime`,`ModifiedBy` AS `ModifiedBy`,`Deleted` AS `Deleted`,`DeletedTime` AS `DeletedTime`,
            //`DeletedBy` AS `DeletedBy` FROM  `nm_blog`.`Article`
            //WHERE (`ReadCount` > @P1) AND `Deleted`=0 

            var list = await query.ToListAsync();

            Assert.True(list.Count > 0);
        }

        [Fact]
        public async void OrderByTest()
        {
            var first = await _db.Find().OrderBy(m => m.Id).FirstAsync();

            Assert.NotNull(first);
            Assert.Equal(1, first.Id);
        }

        [Fact]
        public async void OrderByDescendingTest()
        {
            var first = await _db.Find().OrderByDescending(m => m.Id).FirstAsync();

            Assert.NotNull(first);
            Assert.Equal(100, first.Id);
        }

        [Fact]
        public async void LimitTest()
        {
            var list = await _db.Find().Limit(5, 10).ToListAsync();

            Assert.Equal(10, list.Count);
            Assert.Equal(6, list.First().Id);
        }

        [Fact]
        public async void SelectTest()
        {
            var first = await _db.Find().Select(m => new { m.Id, m.Title }).FirstAsync();

            Assert.NotNull(first.Title);
            Assert.Null(first.Body);
        }

        [Fact]
        public async void LeftJoinTest()
        {
            var query = _db.Find().LeftJoin<CategoryEntity>((x, y) => x.CategoryId == y.Id)
                .Select((x, y) => new { x, CategoryName = y.Name });

            var first = await query.FirstAsync();

            Assert.Equal("ASP.NET Core", first.CategoryName);
        }

        [Fact]
        public async void DeleteForWhereTest()
        {
            await _db.Find(m => m.Id <= 10).DeleteAsync();

            var count = await _db.Find().CountAsync();

            Assert.Equal(90, count);
        }

        [Fact]
        public async void SoftDeleteForWhereTest()
        {
            await _db.Find(m => m.Id <= 10).SoftDeleteAsync();

            var count = await _db.Find().CountAsync();

            Assert.Equal(90, count);
        }

        [Fact]
        public async void UpdateForWhereTest()
        {
            await _db.Find().UpdateAsync(m => new ArticleEntity { Title = "测试" });

            var count = await _db.Find(m => m.Title == "测试").CountAsync();

            Assert.Equal(100, count);
        }

        [Fact]
        public async void MaxTest()
        {
            var val = await _db.Find().MaxAsync(m => m.ReadCount);

            Assert.True(val > 0);
        }

        [Fact]
        public async void MinTest()
        {
            var val = await _db.Find().MinAsync(m => m.ReadCount);

            Assert.True(val > 0);
        }

        [Fact]
        public async void SumTest()
        {
            var val = await _db.Find().SumAsync(m => m.ReadCount);

            Assert.True(val > 0);
        }

        [Fact]
        public async void AvgTest()
        {
            var val = await _db.Find().AvgAsync<decimal>(m => m.ReadCount);

            Assert.True(val > 0);
        }

        [Fact]
        public async void GroupByTest()
        {
            var val = await _db.Find().GroupBy(m => new { m.ReadCount }).Select(m => new { m.Key.ReadCount }).FirstAsync();

            Assert.True(val.ReadCount > 0);
        }

        [Fact]
        public async void PaginationTest()
        {
            var paging = new Paging();
            var list = await _db.Find().PaginationAsync(paging);

            Assert.Equal(15, list.Count);
            Assert.Equal(100, paging.TotalCount);
        }

        [Fact]
        public async void IncludeDeletedTest()
        {
            await _db.Find(m => m.Id > 10).SoftDeleteAsync();

            var count = await _db.Find().IncludeDeleted().CountAsync();

            Assert.Equal(100, count);
        }
    }
}
