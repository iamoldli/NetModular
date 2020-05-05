using System.Text;
using Newtonsoft.Json;
using Qiniu.Http;

namespace Qiniu.Storage
{
    /// <summary>
    /// 获取bucket信息-结果
    /// </summary>
    public class BucketResult : HttpResult
    {
        /// <summary>
        /// bucket信息
        /// </summary>
        public BucketInfo Result
        {
            get
            {
                BucketInfo info = null;

                if (Code == (int)HttpCode.OK && !string.IsNullOrEmpty(Text))
                {
                   info= JsonConvert.DeserializeObject<BucketInfo>(Text);
                }

                return info;
            }
        }

        /// <summary>
        /// 转换为易读字符串格式
        /// </summary>
        /// <returns>便于打印和阅读的字符串</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("code: {0}\n", Code);

            if (Result != null)
            {
                sb.AppendLine("bucket-info:");
                sb.AppendFormat("tbl={0}\n", Result.tbl);
                sb.AppendFormat("zone={0}\n", Result.zone);
                sb.AppendFormat("region={0}\n", Result.region);
                sb.AppendFormat("isGlobal={0}\n", Result.global);
                sb.AppendFormat("isLine={0}\n", Result.line);
            }
            else
            {
                if (!string.IsNullOrEmpty(Text))
                {
                    sb.AppendLine("text:");
                    sb.AppendLine(Text);
                }
            }
            sb.AppendLine();

            sb.AppendFormat("ref-code: {0}\n", RefCode);

            if (!string.IsNullOrEmpty(RefText))
            {
                sb.AppendLine("ref-text:");
                sb.AppendLine(RefText);
            }

            if (RefInfo != null)
            {
                sb.AppendFormat("ref-info:\n");
                foreach (var d in RefInfo)
                {
                    sb.AppendLine(string.Format("{0}: {1}", d.Key, d.Value));
                }
            }

            return sb.ToString();
        }
    }
}
