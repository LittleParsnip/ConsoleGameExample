using ConsoleGame.Event;
using ConsoleGame.Helper;
using System.Diagnostics;

namespace ConsoleGame
{
    /// <summary>
    /// 主程序，建议将游戏逻辑写到MainScene中而不是这里
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// 是否为正方形字体（宽高比1:2的字符会变形）
        /// </summary>
        public const bool IS_SQUARE_FONT = false;

        /// <summary>
        /// 与上一帧的间隔
        /// </summary>
        private static TimeSpan deltaTime = TimeSpan.MinValue;
        /// <summary>
        /// 帧计时器
        /// </summary>
        private static Stopwatch frameTimer = new Stopwatch();

        /// <summary>
        /// 主场景
        /// </summary>
        private static MainScene? mainScene;

        private static void Main(string[] args)
        {
            Time.Initialize();
            InputSystem.Initialize();
            Renderer.Initialize();
            FPSHelper.Initialize();
            GameObjectPool.Initialize();

            WaitForAnyKey();
            // 建议将游戏逻辑写到MainScene中而不是Program.cs
            mainScene = new MainScene();

            frameTimer.Start();
            while (true)
            {
                Update();
                Renderer.Render();

                // 控制帧率
                FrameSleep(8);
            }
        }

        /// <summary>
        /// 等待按键开始
        /// </summary>
        private static void WaitForAnyKey()
        {
            Console.WriteLine("按下任意按键开始游戏...");
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// 每帧更新
        /// </summary>
        private static void Update()
        {
            // 计算帧间隔并重启
            deltaTime = frameTimer.Elapsed;
            frameTimer.Restart();

            // 触发事件
            EventSystem.RaiseUpdate(null, new UpdateEventArgs(deltaTime));
        }

        /// <summary>
        /// 每帧间隔
        /// </summary>
        private static void FrameSleep(int milliseconds)
        {
            // deltaTime是上一帧的间隔，这里重新用frameTimer计算是为了更精确
            int currentFrameTime = (int)frameTimer.Elapsed.TotalMilliseconds;
            int sleepTime = milliseconds - currentFrameTime;
            if (sleepTime > 0)
                Thread.Sleep(sleepTime);
        }
    }
}