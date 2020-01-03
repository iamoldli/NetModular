using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using NetModular.Lib.Utils.Core.Attributes;

namespace NetModular.Lib.Utils.Core.Helpers
{
    /// <summary>
    /// 右邮件帮助类
    /// </summary>
    [Singleton]
    public class EmailSendHelper
    {
        /// <summary>
        /// 邮件发送服务器 163：smtp.163.com QQ：smtp.qq.com
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; } = 25;

        /// <summary>
        /// 是否启用SSL
        /// </summary>
        public bool EnableSsl { get; set; }

        /// <summary>
        /// 发送邮箱
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// 显示的发件人姓名
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 发件人用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 发件人密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 接收邮箱,多个邮箱地址已英文";"分割
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// 邮件标题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 邮件内容
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 邮件内容是否为html
        /// </summary>
        public bool IsBodyHtml { get; set; }

        /// <summary>
        /// 邮件抄送人
        /// </summary>
        public string CC { get; set; }

        /// <summary>
        /// 附件列表，多个附件使用英文';'分隔
        /// </summary>
        public string AttachPath { get; set; }

        /// <summary>
        /// 发送
        /// </summary>
        /// <returns></returns>
        public bool Send()
        {
            try
            {
                var message = new MailMessage
                {
                    Subject = Subject,
                    Body = Body,
                    IsBodyHtml = IsBodyHtml,
                    BodyEncoding = Encoding.UTF8,
                    SubjectEncoding = Encoding.UTF8
                };
                if (!string.IsNullOrEmpty(From))
                {
                    message.From = new MailAddress(From, DisplayName, Encoding.UTF8);
                }
                if (!string.IsNullOrEmpty(To))
                {

                    string[] address = To.Split(';');
                    foreach (var add in address)
                    {
                        message.To.Add(new MailAddress(add));
                    }
                }
                if (!string.IsNullOrWhiteSpace(AttachPath))
                {
                    string[] path = AttachPath.Split(';');
                    for (int i = 0; i < path.Length; i++)
                    {
                        var data = new Attachment(path[i], MediaTypeNames.Application.Octet);
                        data.ContentDisposition.CreationDate = File.GetCreationTime(path[i]);
                        data.ContentDisposition.ModificationDate = File.GetLastWriteTime(path[i]);
                        data.ContentDisposition.ReadDate = File.GetLastAccessTime(path[i]);
                        message.Attachments.Add(data);
                    }
                }

                var client = new SmtpClient
                {
                    Host = Host,
                    Port = Port,
                    EnableSsl = EnableSsl,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(UserName, Password),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                };

                client.Send(message);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
