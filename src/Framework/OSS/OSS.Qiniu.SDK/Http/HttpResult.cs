using System.Collections.Generic;
using System.Text;

namespace Qiniu.Http
{
    /// <summary>
    /// HTTP请求(GET,POST等)的返回消息
    /// </summary>
    public class HttpResult
    {
        /// <summary>
        /// 状态码 (200表示OK)
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 消息或错误文本
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 消息或错误(二进制格式)
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// 参考代码(用户自定义)
        /// </summary>
        public int RefCode { get; set; }

        /// <summary>
        /// 附加信息(用户自定义,如Exception内容)
        /// </summary>
        public string RefText { get; set; }

        /// <summary>
        /// 参考信息(从返回消息WebResponse的头部获取)
        /// </summary>
        public Dictionary<string, string> RefInfo { get; set; }

        /// <summary>
        /// 初始化(所有成员默认值，需要后续赋值)
        /// </summary>
        public HttpResult()
        {
            Code = (int)HttpCode.USER_UNDEF;
            Text = null;
            Data = null;

            RefCode = (int)HttpCode.USER_UNDEF;
            RefInfo = null;
        }

        /// <summary>
        /// 对象复制
        /// </summary>
        /// <param name="hr">要复制其内容的来源</param>
        public void Shadow(HttpResult hr)
        {
            this.Code = hr.Code;
            this.Text = hr.Text;
            this.Data = hr.Data;
            this.RefCode = hr.RefCode;
            this.RefText += hr.RefText;
            this.RefInfo = hr.RefInfo;
        }

        /// <summary>
        /// 转换为易读或便于打印的字符串格式
        /// </summary>
        /// <returns>便于打印和阅读的字符串</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("code:{0}", Code);
            sb.AppendLine();

            if (!string.IsNullOrEmpty(Text))
            {
                sb.AppendLine("text:");
                sb.AppendLine(Text);
            }           

            if (Data != null)
            {
                sb.AppendLine("data:");
                int n = 1024;
                if (Data.Length <= n)
                {
                    sb.AppendLine(Encoding.UTF8.GetString(Data));
                }
                else
                {
                    
                    sb.AppendLine(Encoding.UTF8.GetString(Data, 0, n));
                    sb.AppendFormat("<--- TOO-LARGE-TO-DISPLAY --- TOTAL {0} BYTES --->", Data.Length);
                    sb.AppendLine();
                }                
            }

            sb.AppendLine();      

            sb.AppendFormat("ref-code:{0}", RefCode);
            sb.AppendLine();

            if(!string.IsNullOrEmpty(RefText))
            {
                sb.AppendLine("ref-text:");
                sb.AppendLine(RefText);
            }

            if (RefInfo != null)
            {
                sb.AppendLine("ref-info:");
                foreach (var d in RefInfo)
                {
                    sb.AppendLine(string.Format("{0}:{1}", d.Key, d.Value));
                }                
            }

            sb.AppendLine();

            return sb.ToString();
        }

        /// <summary>
        /// 非法上传凭证错误
        /// </summary>
        public static HttpResult InvalidToken = new HttpResult {
              Code=(int)HttpCode.INVALID_TOKEN,
              Text="invalid uptoken"
        };

        /// <summary>
        /// 非法文件错误
        /// </summary>
        public static HttpResult InvalidFile = new HttpResult
        {
            Code = (int)HttpCode.INVALID_FILE,
            Text = "invalid file"
        };
    }
}
