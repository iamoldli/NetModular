using System.Collections.Generic;
using System.Threading.Tasks;
using Nm.Module.Common.Domain.Area;

namespace Nm.Module.Common.Infrastructure.AreaCrawling
{
    /// <summary>
    /// 行政区域数据爬取处理
    /// </summary>
    public interface IAreaCrawlingHandler
    {
        Task<IList<AreaCrawlingModel>> Crawling();
    }
}
