using System;
using System.Linq;
using Dapper;
using Data.Common;
using Data.Common.Domain;
using Data.Common.Repository;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Entities;
using NetModular.Lib.Data.Abstractions.Options;
using NetModular.Lib.Data.PostgreSQL;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Lib.Utils.Core.Helpers;

namespace Data.PostgreSQL.Tests
{
    public class DbContextTests
    {
        protected readonly IDbContext DbContext;
        protected readonly Guid CategoryId = Guid.NewGuid();

        protected DbContextTests()
        {
            var cfgHelper = new ConfigurationHelper();
            var dbOptions = cfgHelper.Get<DbOptions>("Db");
            dbOptions.Modules.ForEach(m =>
            {
                //数据库名称转小写
                m.Database = m.Database.ToLower();
                m.EntityTypes = typeof(BlogDbContext).Assembly.GetTypes().Where(t => t.IsClass && typeof(IEntity).IsImplementType(t)).ToList();
            });

            var dbContextOptions = new PostgreSQLDbContextOptions(dbOptions, dbOptions.Modules.First(), null, null);

            DbContext = new BlogDbContext(dbContextOptions, null);

            //删除数据库
            try
            {
                using var con = DbContext.NewConnection();
                con.Execute("DROP TABLE nm_blog.article;DROP TABLE nm_blog.category;DROP SCHEMA nm_blog;");
            }
            catch
            {
                //
            }

            //创建数据
            DbContext.CreateDatabase();

            //创建分类
            AddCategory();
        }

        private void AddCategory()
        {
            var categoryRepository = new CategoryRepository(DbContext);
            var category = new CategoryEntity
            {
                Id = CategoryId,
                Name = "ASP.NET Core"
            };

            categoryRepository.AddAsync(category).GetAwaiter().GetResult();
        }
    }
}
