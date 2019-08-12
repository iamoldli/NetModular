using System.Collections.Generic;

namespace Tm.Module.Common.Infrastructure.AreaCrawling
{
    /// <summary>
    /// 区域模型
    /// </summary>
    public class AreaCrawlingModel
    {
        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        public string Pinyin { get; set; }

        /// <summary>
        /// 简拼
        /// </summary>
        public string Jianpin { get; set; }

        /// <summary>
        /// 精度
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// 区号
        /// </summary>
        public string AreaCode { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// 子级
        /// </summary>
        public List<AreaCrawlingModel> Children { get; set; } = new List<AreaCrawlingModel>();

        /// <summary>
        /// 完整名称
        /// </summary>
        public string FullName { get; set; }
    }
}
