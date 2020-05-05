using Newtonsoft.Json;
using Qiniu.Http;
using System.Text;
namespace Qiniu.Storage
{
    /// <summary>
    /// 查询数据处理状态的返回值
    /// </summary>
    public class PrefopResult:HttpResult
    {
        /// <summary>
        /// 持久化任务的状态
        /// </summary>
        public PfopInfo Result
        {
            get
            {
                PfopInfo info = null;

                if ((Code == (int)HttpCode.OK) && (!string.IsNullOrEmpty(Text)))
                {
                   info= JsonConvert.DeserializeObject<PfopInfo>(Text);
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

            if (this.Result!=null)
            {
                sb.AppendFormat("result: {0}\n", JsonConvert.SerializeObject(this.Result));
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
