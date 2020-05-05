using System.Text;
using Qiniu.Http;
using Newtonsoft.Json;

namespace Qiniu.Storage
{
    /// <summary>
    /// 获取空间文件信息(stat操作)的返回消息
    /// </summary>
    public class StatResult : HttpResult
    {
        /// <summary>
        /// stat信息列表
        /// </summary>
        public FileInfo Result
        {
            get
            {
                FileInfo info = null;
                if ((Code == (int)HttpCode.OK) && (!string.IsNullOrEmpty(Text)))
                {
                    info = JsonConvert.DeserializeObject<FileInfo>(Text);
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
                sb.AppendFormat("Size={0}, Type={1}, Hash={2}, Time={3}\n",
                    Result.Fsize, Result.MimeType, Result.Hash, Result.PutTime);

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
