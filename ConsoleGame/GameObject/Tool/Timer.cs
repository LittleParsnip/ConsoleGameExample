using ConsoleGame.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    /// <summary>
    /// 定时器，需通过Start方法启动，时间到后触发事件OnFinished
    /// </summary>
    public class Timer : IDisposable
    {
        /// <summary>
        /// 时间到事件
        /// </summary>
        public event Action? OnFinished;
        /// <summary>
        /// 是否循环
        /// </summary>
        public bool IsLooped { get; set; } = false;
        /// <summary>
        /// 是否使用真实时间（不受TimeScale影响）
        /// </summary>
        public bool UseRealtime { get; set; } = false;

        private float initialTimer = 0;
        private float timer = 0;
        private bool isRunning = false;

        public Timer(float timerSeconds)
        {
            EventSystem.OnUpdate += Update;

            initialTimer = timerSeconds;
        }

        public Timer(float timerSeconds, Action onFinished) : this(timerSeconds)
        {
            OnFinished += onFinished;
        }

        #region 事件
        private void Update(object? sender, EventArgs e)
        {
            if (isRunning)
            {
                if (UseRealtime)
                    timer -= Time.RealtimeDeltaSeconds;
                else
                    timer -= Time.DeltaSeconds;

                if (timer < 0)
                    Finish();
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {
            timer = initialTimer;
            isRunning = true;
        }

        /// <summary>
        /// 重新设置时间（秒）
        /// </summary>
        /// <param name="seconds"></param>
        public void SetTimerSeconds(float seconds)
        {
            initialTimer = seconds;
            timer = seconds;
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            EventSystem.OnUpdate -= Update;

            GC.SuppressFinalize(this);
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 时间到
        /// </summary>
        private void Finish()
        {
            OnFinished?.Invoke();

            if (IsLooped)
                Start();
            else
            {
                isRunning = false;
                // 自动销毁
                Dispose();
            }
        }
        #endregion
    }
}
