using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public class MessageBoxHelper
    {
        // 导入Windows API
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

        public static void ShowMessage(string message, string title = "提示")
        {
            MessageBox(IntPtr.Zero, message, title, 0);
        }
    }
}
