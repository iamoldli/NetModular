using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using NetModular.Lib.Utils.Core.Attributes;

namespace NetModular.Lib.Utils.Core.Helpers
{
    /// <summary>
    /// 绘图帮助类
    /// </summary>
    [Singleton]
    public class DrawingHelper
    {
        //颜色列表，用于验证码、噪线、噪点 
        private readonly Color[] _colors = new[] { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue };

        private readonly StringHelper _stringHelper;

        public DrawingHelper(StringHelper stringHelper)
        {
            _stringHelper = stringHelper;
        }

        /// <summary>
        /// 绘制验证码图片，返回图片的字节数组
        /// </summary>
        /// <param name="code"></param>
        /// <param name="length">验证码长度</param>
        /// <returns></returns>
        public byte[] DrawVerifyCode(out string code, int length = 6)
        {
            code = _stringHelper.GenerateRandomNumber(length);
            //创建画布
            var bmp = new Bitmap(4 + 16 * code.Length, 40);
            //字体
            var font = new Font("Times New Roman", 16);

            var r = new Random();

            var g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            //画噪线 
            for (var i = 0; i < 4; i++)
            {
                int x1 = r.Next(bmp.Width);
                int y1 = r.Next(bmp.Height);
                int x2 = r.Next(bmp.Width);
                int y2 = r.Next(bmp.Height);
                g.DrawLine(new Pen(_colors.RandomGet()), x1, y1, x2, y2);
            }

            //画验证码字符串 
            for (int i = 0; i < code.Length; i++)
            {
                g.DrawString(code[i].ToString(), font, new SolidBrush(_colors.RandomGet()), (float)i * 16 + 2, 8);
            }

            //将验证码图片写入内存流，并将其以 "image/Png" 格式输出 
            var ms = new MemoryStream();
            try
            {
                bmp.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                g.Dispose();
                bmp.Dispose();
            }
        }

        /// <summary>
        /// 绘制验证码图片，返回图片的Base64字符串
        /// </summary>
        /// <param name="code"></param>
        /// <param name="length">验证码长度</param>
        /// <returns></returns>
        public string DrawVerifyCodeBase64String(out string code, int length = 6)
        {
            var bytes = DrawVerifyCode(out code, length);

            return "data:image/png;base64," + Convert.ToBase64String(bytes);
        }
    }
}
