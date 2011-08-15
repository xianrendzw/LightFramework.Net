using System;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace LightFramework.Web
{
    /// <summary>
    /// 生成图片验证码的类。
    /// </summary>
    public class ImageValidCodeHelper
    {
        private static byte[] randb = new byte[4];
        private static RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();

        private static Matrix m = new Matrix();
        private static Bitmap charbmp = new Bitmap(40, 40);

        private static Font[] fonts = {
                                        new Font(new FontFamily("Times New Roman"), 16 + Next(3), FontStyle.Regular),
                                        new Font(new FontFamily("Georgia"), 16 + Next(3), FontStyle.Regular),
                                        new Font(new FontFamily("Arial"), 16 + Next(3), FontStyle.Regular),
                                        new Font(new FontFamily("Comic Sans MS"), 16 + Next(3), FontStyle.Regular)
                                     };

        /// <summary>
        /// 生成默认长度(5位字符)的验证码
        /// </summary>
        /// <returns></returns>
        public static string GenValidateCode()
        {
            return GenValidateCode(5);
        }

        /// <summary>
        /// 生成指定长度的验证码
        /// </summary>
        /// <param name="len">验证码的长度</param>
        /// <returns>指定长度的验证码</returns>
        public static string GenValidateCode(int len)
        {
            //如果验证码长度小于1或大于15,则自动设置为默认长度5
            if (len < 1 || len > 10) len = 5;

            Random r = new Random();

            //合法随机显示字符列表
            //string strLetters = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ234567890";
            string strLetters = "1234567890";
            StringBuilder s = new StringBuilder();

            //将随机生成的字符串绘制到图片上
            for (int i = 0; i < len; i++)
            {
                s.Append(strLetters.Substring(r.Next(0, strLetters.Length - 1), 1));
            }

            return s.ToString();
        }


        /// <summary>
        /// 生成指定长度的验证码
        /// </summary>
        /// <param name="len">验证码的长度</param>
        /// <returns>指定长度的验证码</returns>
        public static string GenValidateCode(int len, bool isOnlyNumber)
        {
            //如果验证码长度小于1或大于15,则自动设置为默认长度5
            if (len < 1 || len > 10) len = 5;

            Random r = new Random();

            //合法随机显示字符列表
            string strLetters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            if (isOnlyNumber)
            {
                strLetters = "1234567890";
            }
            StringBuilder s = new StringBuilder();

            //将随机生成的字符串绘制到图片上
            for (int i = 0; i < len; i++)
            {
                s.Append(strLetters.Substring(r.Next(0, strLetters.Length - 1), 1));
            }

            return s.ToString();
        }

        /// <summary>
        /// 输出验证码图片,默认图片宽度是80,高度30,文字大小是18号
        /// </summary>
        /// <param name="response">HttpResponse对象</param>
        /// <param name="validateCode">验证码字符串</param>
        /// <param name="fontSize">字体大小</param>
        public static void OutputValidateCodeImage(System.Web.HttpResponse response, string validateCode, float fontSize)
        {
            OutputValidateCodeImage(response, validateCode, fontSize, 80, 30);
        }

        /// <summary>
        /// 输出验证码图片,默认图片宽度是80,高度30,文字大小是18号
        /// </summary>
        /// <param name="response">HttpResponse对象</param>
        /// <param name="validateCode">验证码字符串</param>
        public static void OutputValidateCodeImage(System.Web.HttpResponse response, string validateCode)
        {
            OutputValidateCodeImage(response, validateCode, 18, 80, 30);
        }

        /// <summary>
        /// 输出验证码图片
        /// </summary>
        /// <param name="response">HttpResponse对象</param>
        /// <param name="validateCode">验证码字符串</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        public static void OutputValidateCodeImage(System.Web.HttpResponse response, string validateCode, float fontSize, int width, int height)
        {
            Bitmap b = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(b);
            g.FillRectangle(new SolidBrush(Color.FromArgb(146, 164, 222)), 0, 0, width, height);
            Font font = new Font(FontFamily.GenericSerif, fontSize, FontStyle.Bold, GraphicsUnit.Pixel);

            Random r = new Random();
            //将验证码画到图片中
            g.DrawString(validateCode, font, new SolidBrush(Color.White), r.Next(1, 3), r.Next(1, 3));

            //生成干扰线条
            Pen pen = new Pen(new SolidBrush(Color.Transparent), 2);
            for (int i = 0; i < 10; i++)
            {
                g.DrawLine(pen, new Point(r.Next(0, 199), r.Next(0, 59)), new Point(r.Next(0, 199), r.Next(0, 59)));
            }

            b.Save(response.OutputStream, ImageFormat.Jpeg);
            g.Dispose();
            b.Dispose();

            //设置输出流图片格式
            response.ContentType = "image/jpeg";
        }

        /// <summary>
        /// 输出验证码图片
        /// </summary>
        /// <param name="response">HttpResponse对象</param>
        /// <param name="validateCode">验证码字符串</param>
        /// /// <param name="fontSize">字体大小</param>
        /// <param name="height">图片高度</param>
        public static void OutputValidateCodeImage(System.Web.HttpResponse response, string validateCode, float fontSize, int height)
        {
            //图片宽,图片高
            int width = Convert.ToInt32((double)(validateCode.Length * 21.5));

            Bitmap b = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(b);

            Color[] colors1 = new Color[] { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Brown, Color.DarkCyan, Color.Purple };
            Color[] colors2 = new Color[] { Color.LightBlue, Color.LightCoral, Color.LightCyan, Color.LightGoldenrodYellow, Color.LightGray, Color.LightGreen, Color.LightPink, Color.LightSalmon, Color.LightSeaGreen, Color.LightSkyBlue, Color.LightYellow };

            Random random = new Random();
            for (int i = 0; i < 40; i++)
            {
                Pen pen = new Pen(colors2[random.Next(11)], 0f);
                Point point1 = new Point(random.Next(width), random.Next(height));
                Point point2 = new Point(random.Next(width), random.Next(height));
                Point point3 = new Point(random.Next(width), random.Next(height));
                Point point4 = new Point(random.Next(width), random.Next(height));
                g.DrawBezier(pen, point1, point2, point3, point4);
            }

            string text1 = string.Empty;
            //左边距离
            int leftSpan = 2;
            for (int i = 0; i < validateCode.Length; i++)
            {
                text1 = validateCode.Substring(i, 1);
                //字体：Arial   方正姚体  华文行楷  幼圆
                g.DrawString(text1, new Font("宋体", fontSize, FontStyle.Bold), new SolidBrush(colors1[random.Next(6)]), (float)leftSpan, 2f);
                //字体间距
                leftSpan += 20;
            }

            b.Save(response.OutputStream, ImageFormat.Jpeg);
            g.Dispose();
            b.Dispose();

            //设置输出流图片格式
            response.ContentType = "image/jpeg";
        }

        /// <summary>
        /// 生成验证码图片
        /// </summary>
        /// <param name="code">要显示的验证码</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="bgcolor">背景色</param>
        /// <param name="textcolor">文字颜色</param>
        public static byte[] OutputValidateCodeImage(string code, int width, int height, Color bgcolor, int textcolor)
        {
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.HighSpeed;
            g.Clear(bgcolor);

            int fixedNumber = textcolor == 2 ? 60 : 0;
            Pen linePen = new Pen(Color.FromArgb(Next(50) + fixedNumber, Next(50) + fixedNumber, Next(50) + fixedNumber), 1);

            SolidBrush drawBrush = new SolidBrush(Color.FromArgb(Next(100), Next(100), Next(100)));
            for (int i = 0; i < 10; i++)
            {
                g.DrawArc(linePen, Next(20) - 10, Next(20) - 10, Next(width) + 10, Next(height) + 10, Next(-100, 100), Next(-200, 200));
            }

            Graphics charg = Graphics.FromImage(charbmp);

            float charx = -18;
            for (int i = 0; i < code.Length; i++)
            {
                m.Reset();
                m.RotateAt(Next(50) - 25, new PointF(Next(3) + 7, Next(3) + 7));

                charg.Clear(Color.Transparent);
                charg.Transform = m;
                //定义前景色为黑色
                drawBrush.Color = Color.Black;

                charx = charx + 18 + Next(5);
                PointF drawPoint = new PointF(charx, 2.0F);
                charg.DrawString(code[i].ToString(), fonts[Next(fonts.Length - 1)], drawBrush, new PointF(0, 0));

                charg.ResetTransform();

                g.DrawImage(charbmp, drawPoint);
            }

            try
            {
                MemoryStream stream = new MemoryStream();

                bitmap.Save(stream, ImageFormat.Jpeg);

                return stream.ToArray();
            }
            finally
            {
                drawBrush.Dispose();
                g.Dispose();
                charg.Dispose();
                bitmap.Dispose();
            }
            //设置输出流图片格式
            //response.ContentType = "image/jpeg";
            //response.Flush();
        }

        /// <summary>
        /// 输出验证码图片，支持png和jpeg
        /// </summary>
        /// <param name="response">HttpResponse对象</param>
        /// <param name="validateCode">验证码字符串</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        /// <param name="bgcolor"></param>
        /// <param name="txcolor"></param>
        /// <param name="fontStyle"></param>
        public static void OutputValidateCodeImage(System.Web.HttpResponse response
            , string validateCode, float fontSize, int width, int height, Color bgcolor, Color txcolor, FontStyle fontStyle, string contentType)
        {
            Bitmap b = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(b);
            g.FillRectangle(new SolidBrush(bgcolor), 0, 0, width, height);
            Font font = new Font(FontFamily.GenericSerif, fontSize, fontStyle, GraphicsUnit.Pixel);

            //将验证码画到图片中
            g.DrawString(validateCode, font, new SolidBrush(txcolor), 3, 3);

            b.Save(response.OutputStream, ImageFormat.Jpeg);
            g.Dispose();
            b.Dispose();

            //设置输出流图片格式
            if (contentType.Equals("png"))
            {
                response.ContentType = "image/png";
            }
            else
            {
                response.ContentType = "image/jpeg";
            }
        }

        /// <summary>
        /// 获得下一个随机数
        /// </summary>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        private static int Next(int max)
        {
            rand.GetBytes(randb);
            int value = BitConverter.ToInt32(randb, 0);
            value = value % (max + 1);
            if (value < 0)
                value = -value;
            return value;
        }

        /// <summary>
        /// 获得下一个随机数
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        private static int Next(int min, int max)
        {
            int value = Next(max - min) + min;
            return value;
        }

    }
}
