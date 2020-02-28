using System.Text;
using NetModular.Lib.Utils.Core.Extensions;

namespace NetModular.Lib.Utils.Core.Models
{
    /// <summary>
    /// 区域选择模型
    /// </summary>
    public class AreaSelectModel
    {
        /// <summary>
        /// 省
        /// </summary>
        public AreaSelectOptionModel Province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public AreaSelectOptionModel City { get; set; }

        /// <summary>
        /// 区县
        /// </summary>
        public AreaSelectOptionModel Area { get; set; }

        /// <summary>
        /// 乡镇
        /// </summary>
        public AreaSelectOptionModel Town { get; set; }

        /// <summary>
        /// 完整名称
        /// </summary>
        public string FullName
        {
            get
            {
                var sb = new StringBuilder();
                if (Province != null)
                {
                    sb.Append(Province.Name + Separator);
                }
                if (City != null)
                {
                    sb.Append(City.Name + Separator);
                }
                if (Area != null)
                {
                    sb.Append(Area.Name + Separator);
                }
                if (Town != null)
                {
                    sb.Append(Town.Name + Separator);
                }

                if (sb.Length > 0)
                {
                    sb.Remove(sb.Length - 2, 1);
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// 完整名称中间的分隔符
        /// </summary>
        public string Separator { get; set; }

        public AreaSelectModel()
        {
            Province = new AreaSelectOptionModel();
            City = new AreaSelectOptionModel();
            Area = new AreaSelectOptionModel();
            Town = new AreaSelectOptionModel();
        }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="provinceCode">省份编码</param>
        /// <param name="cityCode">城市编码</param>
        /// <param name="areaCode">区县编码</param>
        /// <param name="townCode">城镇编码</param>
        public void Set(string provinceCode, string cityCode = null, string areaCode = null, string townCode = null)
        {
            if (provinceCode.NotNull())
                Province.Code = provinceCode;
            if (cityCode.NotNull())
                City.Code = cityCode;
            if (areaCode.NotNull())
                Area.Code = areaCode;
            if (townCode.NotNull())
                Town.Code = townCode;
        }
    }

    /// <summary>
    /// 区域选择模型
    /// </summary>
    public class AreaSelectOptionModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        public AreaSelectOptionModel()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code">区域编码</param>
        public AreaSelectOptionModel(string code)
        {
            Code = code;
        }

        /// <summary>
        /// 区域名称
        /// </summary>
        /// <param name="code">区域编码</param>
        /// <param name="name">区域名称</param>
        public AreaSelectOptionModel(string code, string name)
        {
            Name = name;
            Code = code;
        }
    }
}