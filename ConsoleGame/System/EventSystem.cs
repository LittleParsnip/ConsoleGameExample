using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Event
{
    internal static class EventSystem
    {
        public static event EventHandler? OnUpdate;
        public static event EventHandler? OnGameObjectCreate;
        public static event EventHandler? OnGameObjectDestroy;

        /// <summary>
        /// 触发Update事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void RaiseUpdate(object? sender, EventArgs e)
        {
            OnUpdate?.Invoke(sender, e);
        }

        /// <summary>
        /// 触发游戏物体创建事件
        /// </summary>
        /// <param name="gameObjcet"></param>
        /// <param name="args"></param>
        public static void RaiseGameObjectCreate(GameObject gameObjcet, EventArgs e)
        {
            OnGameObjectCreate?.Invoke(gameObjcet, e);
        }

        /// <summary>
        /// 触发游戏物体销毁事件
        /// </summary>
        /// <param name="gameObjcet"></param>
        /// <param name="args"></param>
        public static void RaiseGameObjectDestroy(GameObject gameObjcet, EventArgs e)
        {
            OnGameObjectDestroy?.Invoke(gameObjcet, e);
        }
    }
}
