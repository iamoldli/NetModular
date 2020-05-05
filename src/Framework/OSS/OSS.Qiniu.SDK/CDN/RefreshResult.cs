using System.Text;
using Newtonsoft.Json;
using Qiniu.Http;

namespace Qiniu.CDN
{
    /// <summary>
    /// 缓存刷新-结果
    /// </summary>
    public class RefreshResult : HttpResult
    {
        /// <summary>
        /// 获取缓存刷新信息
        /// </summary>
        public RefreshInfo Result
        {
            get
            {
                RefreshInfo info = null;
                if ((Code == (int)HttpCode.OK) && (!string.IsNullOrEmpty(Text)))
                {
                    info=JsonConvert.DeserializeObject<RefreshInfo>(Text);
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
                if (!string.IsNullOrEmpty(Result.RequestId))
                {
                    sb.AppendFormat("requestId:{0}\n", Result.RequestId);
                }
                if (Result.InvalidDirs != null && Result.InvalidDirs.Count > 0)
                {
                    sb.Append("invalidDirs:");
                    foreach (var s in Result.InvalidDirs)
                    {
                        sb.Append(s + " ");
                    }
                    sb.AppendLine();
                }
                if (Result.InvalidUrls != null && Result.InvalidUrls.Count > 0)
                {
                    sb.Append("invalidUrls:");
                    foreach (var s in Result.InvalidUrls)
                    {
                        sb.Append(s + " ");
                    }
                    sb.AppendLine();
                }
                sb.AppendFormat("dirQuotaDay:{0}\n", Result.DirQuotaDay);
                sb.AppendFormat("dirSurplusDay:{0}\n", Result.DirSurplusDay);
                sb.AppendFormat("urlQuotaDay:{0}\n", Result.UrlQuotaDay);
                sb.AppendFormat("urlSurplusDay:{0}\n", Result.UrlSurplusDay);
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