namespace Tm.Module.Common.Application.AreaService.ViewModels
{
    /// <summary>
    /// 区划代码添加模型
    /// </summary>
    public class AreaAddModel
    {
        /// <summary>
        /// 父节点
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 区域代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// 精度
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// 区号
        /// </summary>
        public string AreaCode { get; set; }
    }
}
