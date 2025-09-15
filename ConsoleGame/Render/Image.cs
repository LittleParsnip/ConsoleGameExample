using ConsoleGame.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    /// <summary>
    /// 图像
    /// </summary>
    public class Image : IDisposable
    {
        public readonly static Image Empty = new Image();

        /// <summary>
        /// 位置
        /// </summary>
        public ScreenVector2 Position { get; set; } = new ScreenVector2(0, 0);
        /// <summary>
        /// 渲染顺序
        /// </summary>
        public int SortingOrder { get; set; } = 0;

        private List<Pixel> pixels = new List<Pixel>();

        public Image()
        {

        }

        public Image(ScreenVector2 position) : this()
        {
            Position = position;
        }

        public Image(List<Pixel> pixels) : this()
        {
            this.pixels = pixels;
        }

        public Image(ScreenVector2 position, List<Pixel> pixels) : this(position)
        {
            this.pixels = pixels;
        }

        /// <summary>
        /// 通过字符串创建（\n可换行）
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static Image FromString(string str)
        {
            Image image = new Image(new ScreenVector2());
            string[] columns = str.Split('\n');

            for (int y = 0; y < columns.Length; y++)
            {
                string column = columns[y];
                for (int x = 0; x < column.Length; x++)
                {
                    char c = column[x];
                    if (char.IsWhiteSpace(c)) // 空格作为透明像素，跳过空格
                        continue;

                    Pixel pixel = new Pixel(x, y, c);
                    image.pixels.Add(pixel);
                }
            }

            return image;
        }

        /// <summary>
        /// 通过文件路径读取txt文本创建
        /// </summary>
        /// <param name="filePath">路径</param>
        /// <returns></returns>
        public static Image FromFilePath(string filePath)
        {
            try
            {
                string str = File.ReadAllText(filePath);
                return Image.FromString(str);
            }
            catch (IOException)
            {
                throw;
            }
        }

        /// <summary>
        /// 通过资源文件创建
        /// </summary>
        /// <param name="resName">资源名称</param>
        /// <returns></returns>
        public static Image FromResource(string resName)
        {
            string? str = ImageRes.ResourceManager.GetString(resName);
            if (str == null)
                throw new Exception($"资源 {resName} 不存在！");
            return Image.FromString(str);
        }

        /// <summary>
        /// 获取像素列表
        /// </summary>
        /// <returns></returns>
        public List<Pixel> GetPixels()
        {
            // 克隆一份返回，防止外部修改
            return pixels.ToList();
        }

        /// <summary>
        /// 添加一个像素点
        /// </summary>
        /// <param name="pixel">像素点</param>
        public void AddPixel(Pixel pixel)
        {
            pixels.Add(pixel);
        }

        /// <summary>
        /// 设置图像像素
        /// </summary>
        /// <param name="pixels">像素列表</param>
        public void Set(List<Pixel> pixels)
        {
            this.pixels = pixels;
        }

        /// <summary>
        /// 设置前景色
        /// </summary>
        /// <param name="color"></param>
        public void SetForegroundColor(ConsoleColor color)
        {
            /*
             * 注意：不能使用foreach，因为Pixel是struct，使用foreach修改是无效的！
             * 也不能直接修改pixels[i]，只能通过赋值来修改！
            */
            for (int i = 0; i < pixels.Count; i++)
            {
                Pixel pixel = pixels[i];
                pixel.ForegroundColor = color;
                pixels[i] = pixel;
            }
        }

        /// <summary>
        /// 设置背景色
        /// </summary>
        /// <param name="color"></param>
        public void SetBackgroundColor(ConsoleColor color)
        {
            /*
             * 注意：不能使用foreach，因为Pixel是struct，使用foreach修改是无效的！
             * 也不能直接修改pixels[i]，只能通过赋值来修改！
            */
            for (int i = 0; i < pixels.Count; i++)
            {
                Pixel pixel = pixels[i];
                pixel.BackgroundColor = color;
                pixels[i] = pixel;
            }
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            pixels.Clear();
            GC.SuppressFinalize(this);
        }
    }
}
