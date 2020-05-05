using System.Text;
using Newtonsoft.Json;
using Qiniu.Http;

namespace Qiniu.CDN
{
    /// <summary>
    /// 查询日志-结果
    /// </summary>
    public class LogListResult : HttpResult
    {
        /// <summary>
        /// 获取日志列表信息
        /// </summary>
        public LogListInfo Result
        {
            get
            {
                LogListInfo info = null;
                if ((Code == (int)HttpCode.OK) && (!string.IsNullOrEmpty(Text)))
                {
                    info=JsonConvert.DeserializeObject<LogListInfo>(Text);
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

            sb.AppendFormat("code:{0}\n", Code);
            sb.AppendLine();

            if (Result != null)
            {
                sb.AppendLine("result:");
                sb.AppendFormat("code:{0}\n", Result.Code);
                if (!string.IsNullOrEmpty(Result.Error))
                {
                    sb.AppendFormat("error:{0}\n", Result.Error);
                }
                if (Result.Data != null && Result.Data.Count > 0)
                {
                    sb.AppendLine("log:");
                    foreach (var key in Result.Data.Keys)
                    {
                        sb.AppendFormat("{0}:\n", key);
                        foreach (var d in Result.Data)
                        {
                            if (d.Value != null)
                            {
                                sb.AppendFormat("Domain:{0}\n", d.Key);
                                foreach (var s in d.Value)
                                {
                                    if (s != null)
                                    {
                                        sb.AppendFormat("Name:{0}\nSize:{1}\nMtime:{2}\nUrl:{3}\n\n", s.Name, s.Size, s.Mtime, s.Url);
                                    }
                                }
                            }
                        }
                        sb.AppendLine();
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

            sb.AppendFormat("ref-code:{0}\n", RefCode);

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
                    sb.AppendLine(string.Format("{0}:{1}", d.Key, d.Value));
                }
            }

            return sb.ToString();
        }
    }
}

