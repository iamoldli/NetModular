using Qiniu.Http;
using Newtonsoft.Json;
using System.Text;
namespace Qiniu.Storage
{
    /// <summary>
    /// 文件抓取返回的消息
    /// </summary>
    public class FetchResult : HttpResult
    {
        /// <summary>
        /// Fetch信息列表
        /// </summary>
        public FetchInfo Result
        {
            get
            {
                FetchInfo info = null;
                if ((Code == (int)HttpCode.OK) && (!string.IsNullOrEmpty(Text)))
                {
                    info = JsonConvert.DeserializeObject<FetchInfo>(Text);
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
                sb.AppendFormat("Key={0}, Size={1}, Type={2}, Hash={3}\n",
                    Result.Key, Result.Fsize, Result.MimeType, Result.Hash);
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
