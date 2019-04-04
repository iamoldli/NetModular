using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetModular.Lib.Data.Abstractions.SqlQueryable.GroupByQueryable
{
    public interface IGroupByQueryable
    {
        IList<TResult> ToList<TResult>();

        Task<IList<TResult>> ToListAsync<TResult>();
    }
}
