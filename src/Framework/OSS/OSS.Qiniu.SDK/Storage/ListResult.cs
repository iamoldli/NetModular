using System.Text;
using Newtonsoft.Json;
using Qiniu.Http;

namespace Qiniu.Storage
{
    /// <summary>
    /// 获取空间文件列表(list操作)的返回消息
    /// </summary>
    public class ListResult:HttpResult
    {
        /// <summary>
        /// 文件列表信息
        /// </summary>
        public ListInfo Result
        {
            get
            {
                ListInfo info = null;
                if ((Code == (int)HttpCode.OK) && (!string.IsNullOrEmpty(Text)))
                {
                   info= JsonConvert.DeserializeObject<ListInfo>(Text);
                }
                return info;
            }
        }

        /// <summary>
        /// 转换为易读字符串格式
        /// </summary>
        /// <returns>便于打印和阅读的字符串></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("code: {0}\n", Code);

            if (Result != null)
            {
                if (Result.CommonPrefixes != null)
                {
                    sb.Append("commonPrefixes:");
                    foreach(var p in Result.CommonPrefixes)
                    {
                        sb.AppendFormat("{0} ", p);
                    }
                    sb.AppendLine();
                }

                if (!string.IsNullOrEmpty(Result.Marker))
                {
                    sb.AppendFormat("marker: {0}\n", Result.Marker);
                }

                if (Result.Items != null)
                {
                    sb.AppendLine("items:");
                    int i = 0, n = Result.Items.Count;
                    foreach (var item in Result.Items)
                    {
                        sb.AppendFormat("#{0}/{1}:Key={2}, Size={3}, Mime={4}, Hash={5}, Time={6}, Type={7}\n",
                            ++i, n, item.Key, item.Fsize, item.MimeType, item.Hash, item.PutTime, item.FileType);
                    }
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
