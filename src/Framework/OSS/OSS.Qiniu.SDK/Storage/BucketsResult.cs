using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using Qiniu.Http;

namespace Qiniu.Storage
{
    /// <summary>
    /// 获取空间列表-结果
    /// </summary>
    public class BucketsResult:HttpResult
    {
        /// <summary>
        /// 空间列表
        /// </summary>
        public List<string> Result
        {
            get
            {
                List<string> buckets = null;
                if ((Code == (int)HttpCode.OK) && (!string.IsNullOrEmpty(Text)))
                {
                    buckets = JsonConvert.DeserializeObject<List<string>>(Text);
                }
                return buckets;
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
                sb.AppendLine("bucket(s):");
                foreach(var b in Result)
                {
                    sb.AppendLine(b);
                }
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
