using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using OpenTK.Graphics.OpenGL;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace pulse.Client.Graphics
{
    static class TextureManager
    {
        public static int LoadRawTextImage(string text, Font font, out SizeF size)
        {
            int textureId;
            size = GetStringSize(text, font);

            using (Bitmap textBitmap = new Bitmap((int)size.Width, (int)size.Height))
            {
                GL.GenTextures(1, out textureId);
                GL.BindTexture(TextureTarget.Texture2D, textureId);
                BitmapData data =
                    textBitmap.LockBits(new Rectangle(0, 0, textBitmap.Width, textBitmap.Height),
                        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                    OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                    (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                    (int)TextureMagFilter.Linear);
                GL.Finish();
                textBitmap.UnlockBits(data);

                var gfx = System.Drawing.Graphics.FromImage(textBitmap);
                gfx.Clear(Color.Transparent);
                gfx.TextRenderingHint = TextRenderingHint.AntiAlias;
                gfx.DrawString(text, font, new SolidBrush(Color.White), new RectangleF(0, 0, size.Width + 10, size.Height));

                BitmapData data2 = textBitmap.LockBits(new Rectangle(0, 0, textBitmap.Width, textBitmap.Height),
                    ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, textBitmap.Width, textBitmap.Height, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data2.Scan0);
                textBitmap.UnlockBits(data2);
                gfx.Dispose();
            }

            return textureId;
        }
        public static int LoadImage(string path)
        {
            if (!File.Exists(path))
                return -1;
            
            Bitmap bitmap = new Bitmap(Bitmap.FromFile(path));

            return GenerateFromBitmap(bitmap);
        }

        private static SizeF GetStringSize(string text, Font font)
        {
            using (Bitmap temp = new Bitmap(1, 1))
            {
                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(temp);
                SizeF temp2 = g.MeasureString(text, font);
                return temp2;
            }
        }

        private static int GenerateFromBitmap(Bitmap bitmap)
        {
            try
            {
                int texture;

                GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
                GL.GenTextures(1, out texture);
                GL.BindTexture(TextureTarget.Texture2D, texture);

                BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                    OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                GL.Finish();

                bitmap.UnlockBits(data);

                return texture;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
