using ConsoleGame.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Helper
{
    public static class FPSHelper
    {
        private static GameObject FPSText = new GameObject();

        private static float fps = 0.0f;

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Initialize()
        {
            EventSystem.OnUpdate += Update;

            FPSText.SetPosition(0, 0);
            FPSText.SetSortingOrder(Renderer.SORTING_ORDER_UI);
            RefreshFPSText();

            // 每一段时间刷新一次FPS文本（太快看不清）
            Timer timer = new Timer(0.5f, RefreshFPSText);
            timer.IsLooped = true;
            timer.UseRealtime = true;
            timer.Start();
        }

        #region 事件
        private static void Update(object? sender, EventArgs e)
        {
            UpdateEventArgs args = (UpdateEventArgs)e;
            float deltaSeconds = args.DeltaSeconds;

            fps = 1.0f / deltaSeconds;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 刷新FPS文本
        /// </summary>
        private static void RefreshFPSText()
        {
            string fpsStr = "FPS: " + fps.ToString("0.0");
            Image image = Image.FromString(fpsStr);
            ConsoleColor textColor = GetFPSColor(fps);
            image.SetForegroundColor(textColor);
            FPSText.SetImage(image);
        }

        /// <summary>
        /// 根据FPS值获取文本颜色
        /// </summary>
        /// <param name="fps"></param>
        /// <returns></returns>
        private static ConsoleColor GetFPSColor(float fps)
        {
            ConsoleColor textColor;
            if (fps >= 30.0f)
                textColor = ConsoleColor.Green;
            else if (fps >= 10.0f)
                textColor = ConsoleColor.Yellow;
            else
                textColor = ConsoleColor.Red;
            return textColor;
        }
        #endregion
    }
}
