using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using Qiniu.Http;

namespace Qiniu.Storage
{
    /// <summary>
    /// 批量处理结果
    /// </summary>
    public class BatchResult : HttpResult
    {
        /// <summary>
        /// 错误消息
        /// </summary>
        public string Error
        {
            get
            {
                string ex = null;
                if (Code != (int)HttpCode.OK && Code != (int)HttpCode.PARTLY_OK)
                {

                    Dictionary<string, string> ret = JsonConvert.DeserializeObject<Dictionary<string, string>>(Text);
                    if (ret.ContainsKey("error"))
                    {
                        ex = ret["error"];
                    }
                }
                return ex;
            }
        }

        /// <summary>
        /// 获取批量处理结果
        /// </summary>
        public List<BatchInfo> Result
        {
            get
            {
                List<BatchInfo> info = null;
                if ((Code == (int)HttpCode.OK || Code == (int)HttpCode.PARTLY_OK) &&
                    (!string.IsNullOrEmpty(Text)))
                {
                    info = JsonConvert.DeserializeObject<List<BatchInfo>>(Text);
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
                sb.AppendLine("result:");
                int i = 0, n = Result.Count;
                foreach (var v in Result)
                {
                    sb.AppendFormat("#{0}/{1}\n", ++i, n);
                    sb.AppendFormat("code: {0}\n", v.Code);
                    sb.AppendFormat("data:\n{0}\n\n", v.Data);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(Error))
                {
                    sb.AppendFormat("Error: {0}\n", Error);
                }
                else if (!string.IsNullOrEmpty(Text))
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
