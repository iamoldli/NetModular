using System;
using System.IO;
using System.Linq;
using NetModular.Lib.Utils.Core.Attributes;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

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
            code = _stringHelper.GenerateRandom(length);
            using var img = new Image<Rgba32>(4 + 16 * code.Length, 40);
            var font = new Font(SystemFonts.Families.First(), 16, FontStyle.Regular);
            var codeStr = code;
            img.Mutate(x =>
            {
                x.BackgroundColor(Color.WhiteSmoke);

                var r = new Random();

                //画噪线 
                for (var i = 0; i < 4; i++)
                {
                    int x1 = r.Next(img.Width);
                    int y1 = r.Next(img.Height);
                    int x2 = r.Next(img.Width);
                    int y2 = r.Next(img.Height);
                    x.DrawLine(new SolidPen(_colors.RandomGet(), 1L), new PointF(x1, y1), new PointF(x2, y2));
                }

                //画验证码字符串 
                for (int i = 0; i < codeStr.Length; i++)
                {
                    x.DrawText(codeStr[i].ToString(), font, _colors.RandomGet(), new PointF((float)i * 16 + 4, 8));
                }
            });

            using var stream = new MemoryStream();
            img.SaveAsPng(stream);
            return stream.GetBuffer();
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
