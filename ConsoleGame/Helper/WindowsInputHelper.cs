using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public class WindowsInputHelper
    {
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        [DllImport("user32.dll")]
        private static extern bool GetKeyboardState(byte[] lpKeyState);

        /// <summary>
        /// 检测Windows虚拟按键是否按下
        /// </summary>
        public static bool IsKeyPressed(int virtualKeyCode)
        {
            return (GetAsyncKeyState(virtualKeyCode) & 0x8000) != 0;
        }

        /// <summary>
        /// 检测控制台按键是否按下
        /// </summary>
        public static bool IsConsoleKeyPressed(ConsoleKey key)
        {
            int vk = MapConsoleKeyToVirtualKey(key);
            return IsKeyPressed(vk);
        }

        /// <summary>
        /// 映射ConsoleKey到Windows虚拟键码
        /// </summary>
        private static int MapConsoleKeyToVirtualKey(ConsoleKey key)
        {
            // 简单的映射，可以根据需要扩展
            return (int)key;
        }
    }
}
