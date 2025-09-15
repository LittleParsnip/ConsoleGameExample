using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    /// <summary>
    /// 像素缓冲区
    /// </summary>
    public class PixelBuffer
    {
        /// <summary>
        /// 宽度
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// 高度
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// 索引器
        /// </summary>
        private Pixel[,] pixels;

        public PixelBuffer(int width, int height)
        {
            Width = width;
            Height = height;

            pixels = new Pixel[height, width];
            Initialize();
        }

        public Pixel GetPixel(int x, int y)
        {
            return pixels[y, x];
        }

        public void SetPixel(int x, int y, Pixel pixel)
        {
            // 防止出界
            if (x >= Width || y >= Height)
                return;
            if (x < 0 || y < 0)
                return;

            pixel.SetPosition(x, y);
            pixels[y, x] = pixel;
        }

        public void SetPixel(Pixel pixel)
        {
            SetPixel(pixel.X, pixel.Y, pixel);
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    pixels[y, x] = new Pixel(x, y, ' ');
                }
            }
        }

        #region 私有方法
        private void Initialize()
        {
            Clear();
        }
        #endregion
    }
}
