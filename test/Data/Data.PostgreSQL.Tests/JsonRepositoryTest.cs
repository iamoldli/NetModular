using Data.Common.Domain;
using Data.Common.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Data.PostgreSQL.Tests
{
    public class JsonRepositoryTest : DbContextTests
    {
        private readonly IJsonRepository _jsonRepository;

        public JsonRepositoryTest()
        {
            _jsonRepository = new JsonRepository(DbContext);
        }

        [Fact]
        public async Task<JsonEntity> AddTest()
        {
            var json = new JsonEntity
            {
                Body = $"{{\"code\": \"123\", \"name\": \"test\"}}",
            };

            var result = await _jsonRepository.AddAsync(json);

            Assert.True(result);
            return json;
        }

        [Fact]
        public async void DeleteTest()
        {
            var json = await AddTest();

            var result = await _jsonRepository.DeleteAsync(json.Id);

            Assert.True(result);
        }

        //[Fact]
        //public async Task BatchAddTest()
        //{
        //    await AddTest();

        //    var list = new List<JsonEntity>();
        //    for (int i = 0; i < 10000; i++)
        //    {
        //        var json = new JsonEntity
        //        {
        //            Body = $"{{\"code\": \"123\", \"name\": \"test{i.ToString()}\"}}",
        //        };
        //        list.Add(json);
        //    }

        //    var sw = new Stopwatch();
        //    sw.Start();

        //    await _jsonRepository.AddAsync(list);

        //    sw.Stop();

        //    var count = await _jsonRepository.Count();

        //    Assert.True(count == 10001);
        //}

        [Fact]
        public async void GetTest()
        {
            var json = await AddTest();

            var result = await _jsonRepository.GetAsync(json.Id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, json.Id);
        }

        [Fact]
        public async void QueryTest()
        {
            await AddTest();

            var jsons = await _jsonRepository.Query();
            Assert.NotNull(jsons);
            Assert.True(jsons.Count > 0);
        }

        [Fact]
        public async void QueryOrderbyTest()
        {
            await AddTest();

            var jsons = await _jsonRepository.QueryOrderby();
            Assert.NotNull(jsons);
            Assert.True(jsons.Count > 0);
        }

        //[Fact]
        //public async void GetAllTest()
        //{
        //    await BatchAddTest();

        //    var list = await _jsonRepository.GetAllAsync();

        //    Assert.Equal(10001, list.Count);
        //}
    }
}
