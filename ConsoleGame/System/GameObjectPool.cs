using ConsoleGame.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    /// <summary>
    /// 对象池
    /// </summary>
    public static class GameObjectPool
    {
        private static List<GameObject> gameObjectList = new List<GameObject>();

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Initialize()
        {
            EventSystem.OnUpdate += EventSystem_OnUpdate;
            EventSystem.OnGameObjectCreate += EventSystem_OnGameObjectCreate;
            EventSystem.OnGameObjectDestroy += EventSystem_OnGameObjectDestroy;
        }

        #region 公共方法
        /// <summary>
        /// 获取对象列表
        /// </summary>
        /// <returns></returns>
        public static List<GameObject> GetGameObjectList()
        {
            return gameObjectList;
        }
        #endregion

        #region 事件
        private static void EventSystem_OnUpdate(object? sender, EventArgs e)
        {

        }

        private static void EventSystem_OnGameObjectCreate(object? sender, EventArgs e)
        {
            GameObject? gameObject = sender as GameObject;
            if (gameObject == null)
            {
                MessageBoxHelper.ShowMessage("GameObjcet is null!", "错误");
                throw new Exception("GameObjcet is null!");
            }

            gameObjectList.Add(gameObject);
        }

        private static void EventSystem_OnGameObjectDestroy(object? sender, EventArgs e)
        {
            GameObject? gameObject = sender as GameObject;
            if (gameObject == null)
            {
                MessageBoxHelper.ShowMessage("GameObjcet is null!", "错误");
                throw new Exception("GameObjcet is null!");
            }

            gameObjectList.Remove(gameObject);
        }
        #endregion
    }
}
