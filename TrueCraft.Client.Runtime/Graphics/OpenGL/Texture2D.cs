using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using SystemPixelFormat = System.Drawing.Imaging.PixelFormat;
using OpenTKPixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;

namespace TrueCraft.Client.Graphics.OpenGL
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Texture2D
        : Texture
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static Texture2D FromStream(Stream stream)
        {
            if (stream == null)
                throw new ArgumentException();

            var texture = new Texture2D();
            using (var image = Image.FromStream(stream))
                texture.SetBitmap((Bitmap)image);
            return texture;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Texture2D FromFile(string path)
        {
            var texture = new Texture2D();
            using (var image = Image.FromFile(path))
                texture.SetBitmap((Bitmap)image);
            return texture;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override TextureTarget Target
        {
            get { return TextureTarget.Texture2D; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Texture2D()
            : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        public void SetBitmap(Bitmap image)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

            if (!IsBound)
                throw new InvalidOperationException();

            SwitchUnit(Unit);

            var imageData = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadOnly,
                SystemPixelFormat.Format32bppArgb);

            GL.TexImage2D(
                (OpenTK.Graphics.OpenGL.TextureTarget)Target,
                0,
                PixelInternalFormat.Rgba,
                image.Width, image.Height,
                0,
                OpenTKPixelFormat.Bgra,
                PixelType.Byte,
                imageData.Scan0);

            image.UnlockBits(imageData);
        }
    }
}
