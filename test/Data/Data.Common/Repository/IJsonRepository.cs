using Data.Common.Domain;
using NetModular.Lib.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Common.Repository
{
    public interface IJsonRepository : IRepository<JsonEntity>
    {
        Task<IList<JsonEntity>> Query();

        Task<IList<JsonEntity>> QueryOrderby();
    }
}
