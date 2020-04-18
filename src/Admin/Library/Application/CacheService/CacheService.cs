using System.Linq;
using System.Threading.Tasks;
using NetModular.Lib.Cache.Abstractions;

namespace NetModular.Module.Admin.Application.CacheService
{
    public class CacheService : ICacheService
    {
        private readonly CacheKeyDescriptorCollection _collection;
        private readonly ICacheHandler _cacheHandler;

        public CacheService(CacheKeyDescriptorCollection collection, ICacheHandler cacheHandler)
        {
            _collection = collection;
            _cacheHandler = cacheHandler;
        }

        public IResultModel Query(string moduleCode)
        {
            var list = _collection.Where(m => m.ModuleCode.EqualsIgnoreCase(moduleCode)).ToList();
            var result = new QueryResultModel<CacheKeyDescriptor>
            {
                Rows = list,
                Total = list.Count
            };
            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Clear(string prefix)
        {
            if (prefix.IsNull())
                return ResultModel.Failed();

            await _cacheHandler.RemoveByPrefixAsync(prefix);
            return ResultModel.Success();
        }
    }
}
