using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using Qiniu.Http;

namespace Qiniu.Storage
{
    /// <summary>
    /// 获取空间域名(domains操作)的返回消息
    /// </summary>
    public class DomainsResult:HttpResult
    {
        /// <summary>
        /// 域名(列表)
        /// </summary>
        public List<string> Result
        {
            get
            {
                List<string> domains = null;
                if ((Code == (int)HttpCode.OK) && (!string.IsNullOrEmpty(Text)))
                {
                    domains=JsonConvert.DeserializeObject<List<string>>(Text);
                }
                return domains;
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

            sb.AppendLine();

            if (Result != null)
            {
                sb.AppendLine("domain(s):");
                foreach (var d in Result)
                {
                    sb.AppendLine(d);
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
