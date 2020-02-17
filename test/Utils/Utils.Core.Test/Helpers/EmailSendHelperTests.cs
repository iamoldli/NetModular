using NetModular.Lib.Utils.Core.Helpers;
using Xunit;

namespace Utils.Core.Test.Helpers
{
    public class EmailSendHelperTests
    {
        [Fact]
        public void SendTest()
        {
            var helper = new EmailSendHelper
            {
                Host = "smtp.exmail.qq.com",
                Port = 587,
                EnableSsl = true,
                From = "",
                UserName = "",
                Password = "",
                To = "wenju1991@163.com",
                Subject = "test",
                Body = "test"
            };

            var b = helper.Send();
            Assert.True(b);
        }
    }
}
