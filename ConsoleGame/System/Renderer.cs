using ConsoleGame.Event;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    /// <summary>
    /// 渲染器
    /// </summary>
    public static class Renderer
    {
        /* 暂时没用到，如果渲染要单独分出一个线程就用上了
        public const int FRAME_RATE = 60;
        public const int FRAME_TIME = 1000 / FRAME_RATE;
        */
        public const int SCREEN_WIDTH = 80;
        public const int SCREEN_HEIGHT = Program.IS_SQUARE_FONT ? 60 : 30;

        #region 图层
        public const int SORTING_ORDER_BACKGROUND = -10;
        public const int SORTING_ORDER_WARNING_ZONE = 1;
        public const int SORTING_ORDER_PLAYERBULLET = 3;
        public const int SORTING_ORDER_TARGET_RING = 4;
        public const int SORTING_ORDER_PLAYER = 5;
        public const int SORTING_ORDER_ENEMYBULLET = 6;
        public const int SORTING_ORDER_ENEMY = 7;
        public const int SORTING_ORDER_BOSS = 9;
        public const int SORTING_ORDER_FOREGROUND = 10;
        public const int SORTING_ORDER_UI = 100;
        #endregion

        /// <summary>
        /// 上一帧时间
        /// </summary>
        private static DateTime lastFrameTime = DateTime.MinValue;

        /// <summary>
        /// 像素缓冲区（双缓冲）
        /// </summary>
        private static PixelBuffer currentPixelBuffer = new PixelBuffer(SCREEN_WIDTH, SCREEN_HEIGHT);
        /// <summary>
        /// 像素缓冲区（双缓冲）
        /// </summary>
        private static PixelBuffer nextPixelBuffer = new PixelBuffer(SCREEN_WIDTH, SCREEN_HEIGHT);
        /// <summary>
        /// 游戏物体缓冲区
        /// </summary>
        private static List<GameObject> gameObjectBuffer = new List<GameObject>();
        /// <summary>
        /// 图像缓冲区
        /// </summary>
        private static List<Image> imageBuffer = new List<Image>();

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Initialize()
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(SCREEN_WIDTH, SCREEN_HEIGHT);
            Console.Clear();

            EventSystem.OnGameObjectCreate += EventSystem_OnGameObjectCreate;
            EventSystem.OnGameObjectDestroy += EventSystem_OnGameObjectDestroy;
        }

        /// <summary>
        /// 渲染
        /// </summary>
        public static void Render()
        {
            // 加入图片
            RefreshImageList();
            SubmitAllImage();
            // 交换缓冲区
            SwapPixelBuffer();
            // 渲染
            RenderAllPixel();
            nextPixelBuffer.Clear();

            lastFrameTime = DateTime.Now;
        }

        #region 私有方法
        /// <summary>
        /// 交换双缓冲
        /// </summary>
        private static void SwapPixelBuffer()
        {
            PixelBuffer temp = currentPixelBuffer;
            currentPixelBuffer = nextPixelBuffer;
            nextPixelBuffer = temp;
        }

        /// <summary>
        /// 刷新图像列表
        /// </summary>
        private static void RefreshImageList()
        {
            imageBuffer.Clear();

            // 按照渲染顺序排序
            foreach (GameObject go in gameObjectBuffer)
                imageBuffer.Add(go.Image);
            imageBuffer.Sort((m1, m2) => m1.SortingOrder.CompareTo(m2.SortingOrder));
        }

        /// <summary>
        /// 加入所有图像
        /// </summary>
        private static void SubmitAllImage()
        {
            foreach (Image image in imageBuffer)
            {
                SubmitImage(image);
            }
        }

        /// <summary>
        /// 加入图像
        /// </summary>
        /// <param name="image">图像</param>
        private static void SubmitImage(Image image)
        {
            foreach (Pixel pixel in image.GetPixels())
            {
                ScreenVector2 pos = image.Position + pixel.Position;
                SubmitPixel(pos.X, pos.Y, pixel);
            }
        }

        /// <summary>
        /// 加入像素
        /// </summary>
        /// <param name="pixel">像素点</param>
        private static void SubmitPixel(int x, int y, Pixel pixel)
        {
            nextPixelBuffer.SetPixel(x, y, pixel);
        }

        /// <summary>
        /// 加入像素
        /// </summary>
        /// <param name="pixel">像素点</param>
        private static void SubmitPixel(Pixel pixel)
        {
            nextPixelBuffer.SetPixel(pixel);
        }

        /// <summary>
        /// 渲染所有像素
        /// </summary>
        private static void RenderAllPixel()
        {
            for (int y = 0; y < SCREEN_HEIGHT; y++)
            {
                // 连续的更改像素，不需要重复移动光标
                List<Pixel> continuousChangedPixels = new List<Pixel>();
                for (int x = 0; x < SCREEN_WIDTH; x++)
                {
                    Pixel originalPixel = nextPixelBuffer.GetPixel(x, y);
                    Pixel pixel = currentPixelBuffer.GetPixel(x, y);
                    if (originalPixel != pixel)
                    {
                        // 加入连续更改点列表
                        continuousChangedPixels.Add(pixel);
                    }
                    else
                    {
                        // 找到断点就开始渲染
                        RenderPixels(continuousChangedPixels);
                        continuousChangedPixels.Clear();
                    }
                }
                // 本排最后一个像素
                RenderPixels(continuousChangedPixels);
            }
        }

        /// <summary>
        /// 渲染一排多个像素
        /// </summary>
        /// <param name="pixels">像素点（连在一起）</param>
        private static void RenderPixels(List<Pixel> pixels)
        {
            if (pixels.Count <= 0) return;

            Console.SetCursorPosition(pixels[0].X, pixels[0].Y);
            for (int i = 0; i < pixels.Count; i++)
                RenderPixel(pixels[i]);
        }

        /// <summary>
        /// 渲染像素
        /// </summary>
        /// <param name="pixel">像素点</param>
        private static void RenderPixel(Pixel pixel)
        {
            if (Console.ForegroundColor != pixel.ForegroundColor)
                Console.ForegroundColor = pixel.ForegroundColor;
            if (Console.BackgroundColor != pixel.BackgroundColor)
                Console.BackgroundColor = pixel.BackgroundColor;
            Console.Write(pixel.Symbol);
        }
        #endregion

        #region 事件处理
        private static void EventSystem_OnGameObjectCreate(object? sender, EventArgs e)
        {
            GameObject? gameObjcet = sender as GameObject;
            if (gameObjcet == null)
            {
                MessageBoxHelper.ShowMessage("GameObjcet is null!", "错误");
                throw new Exception("GameObjcet is null!");
            }

            gameObjectBuffer.Add(gameObjcet);
        }

        private static void EventSystem_OnGameObjectDestroy(object? sender, EventArgs e)
        {
            GameObject? gameObjcet = sender as GameObject;
            if (gameObjcet == null)
            {
                MessageBoxHelper.ShowMessage("GameObjcet is null!", "错误");
                throw new Exception("GameObjcet is null!");
            }

            gameObjectBuffer.Remove(gameObjcet);
        }
        #endregion
    }
}
