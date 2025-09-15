using ConsoleGame.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    /// <summary>
    /// 时间系统
    /// </summary>
    public static class Time
    {
        #region 属性
        /// <summary>
        /// 上一帧真实时间（不受TimeScale影响）
        /// </summary>
        public static TimeSpan RealtimeDeltaTime { get; private set; }
        /// <summary>
        /// 上一帧真实时间（秒）（不受TimeScale影响）
        /// </summary>
        public static float RealtimeDeltaSeconds => (float)RealtimeDeltaTime.TotalSeconds;

        /// <summary>
        /// 上一帧时间
        /// </summary>
        public static TimeSpan DeltaTime => RealtimeDeltaTime * timeScale;
        /// <summary>
        /// 上一帧时间（秒）
        /// </summary>
        public static float DeltaSeconds => (float)DeltaTime.TotalSeconds;

        /// <summary>
        /// 时间缩放
        /// </summary>
        public static float TimeScale => timeScale;
        #endregion

        #region 私有变量
        /// <summary>
        /// 时间缩放
        /// </summary>
        private static float timeScale = 1.0f;
        #endregion

        #region 事件
        private static void Update(object? sender, EventArgs e)
        {
            UpdateEventArgs args = (UpdateEventArgs)e;
            RealtimeDeltaTime = args.DeltaTime;
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Initialize()
        {
            EventSystem.OnUpdate += Update;
        }

        /// <summary>
        /// 设置时间缩放
        /// </summary>
        /// <param name="scale">缩放倍数</param>
        public static void SetTimeScale(float scale)
        {
            if (scale < 0) scale = 0;
            if (scale > 10) scale = 10;
            timeScale = scale;
        }
        #endregion

        #region 私有方法

        #endregion
    }
}
