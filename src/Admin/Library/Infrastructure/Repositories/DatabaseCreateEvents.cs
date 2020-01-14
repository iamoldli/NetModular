using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Utils.Core.Extensions;

namespace NetModular.Module.Admin.Infrastructure.Repositories
{
    public class DatabaseCreateEvents : IDatabaseCreateEvents
    {
        public IDbContext DbContext { get; set; }

        public Task Before()
        {
            return Task.CompletedTask;
        }

        public async Task After()
        {
            var assembly = GetType().Assembly;
            var resources = assembly.GetManifestResourceNames();
            var name = resources.FirstOrDefault(m => m.EndsWith($"Infrastructure.Repositories.{DbContext.Options.SqlAdapter.SqlDialect.ToDescription()}._init.sql"));
            if (name.IsNull())
                return;

            var stream = assembly.GetManifestResourceStream(name);
            if (stream == null)
                return;

            var sr = new StreamReader(stream);
            var sql = sr.ReadToEnd();
            sr.Close();
            stream.Close();

            if (sql.NotNull())
            {
                var con = DbContext.NewConnection();

                await con.ExecuteAsync(sql);
            }
        }
    }
}
