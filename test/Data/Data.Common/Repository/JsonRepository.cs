using Data.Common.Domain;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Common.Repository
{
    public class JsonRepository : RepositoryAbstract<JsonEntity>, IJsonRepository
    {
        public JsonRepository(IDbContext context) : base(context)
        {
        }

        public async Task<IList<JsonEntity>> Query()
        {
            var query = Db.Find();

            var list = await query.Where(" body @> '{\"code\": \"123\"}'").ToListAsync();
            return list;
        }

        public async Task<IList<JsonEntity>> QueryOrderby()
        {
            var query = Db.Find();

            var list = await query.Where(" body @> '{\"code\": \"123\"}'").OrderBy("body -> 'code'").ToListAsync();
            return list;
        }
    }
}
