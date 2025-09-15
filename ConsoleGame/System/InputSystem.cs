using ConsoleGame.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public static class InputSystem
    {
        /// <summary>
        /// 上一次按下的Key
        /// </summary>
        private static Dictionary<ConsoleKey, bool> lastPressedKey = new Dictionary<ConsoleKey, bool>();
        /// <summary>
        /// 当前按下的Key
        /// </summary>
        private static Dictionary<ConsoleKey, bool> currentPressedKey = new Dictionary<ConsoleKey, bool>();

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Initialize()
        {
            EventSystem.OnUpdate += Update;
        }

        #region 事件
        private static void Update(object? sender, EventArgs e)
        {
            // 更新上一次Key字典
            foreach (var pair in currentPressedKey)
                lastPressedKey[pair.Key] = pair.Value;
            // 获取本次Key字典
            for (int i = 0; i < 256; i++)
                currentPressedKey[(ConsoleKey)i] = WindowsInputHelper.IsKeyPressed(i);
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 按键刚刚按下
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsKeyDown(ConsoleKey key)
        {
            return !IsKeyLastPressed(key) && IsKeyCurrentPressed(key);
        }

        /// <summary>
        /// 按键按下
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsKeyPressed(ConsoleKey key)
        {
            return IsKeyCurrentPressed(key);
        }

        /// <summary>
        /// 按键刚刚抬起
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsKeyUp(ConsoleKey key)
        {
            return IsKeyLastPressed(key) && !IsKeyCurrentPressed(key);
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 按键当前按着
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static bool IsKeyCurrentPressed(ConsoleKey key)
        {
            bool currentPressed = currentPressedKey.ContainsKey(key) && currentPressedKey[key];
            return currentPressed;
        }

        /// <summary>
        /// 按键上一帧按着
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static bool IsKeyLastPressed(ConsoleKey key)
        {
            bool lastPressed = lastPressedKey.ContainsKey(key) && lastPressedKey[key];
            return lastPressed;
        }
        #endregion
    }
}
