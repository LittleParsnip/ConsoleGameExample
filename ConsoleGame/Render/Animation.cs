using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public class Animation : IDisposable
    {
        public readonly static Animation Empty = new Animation();

        #region 属性
        /// <summary>
        /// 每帧持续时间（秒）
        /// </summary>
        public float FrameSeconds
        {
            get => frameSeconds;
            private set
            {
                frameSeconds = value;
                frameTimer.SetTimerSeconds(frameSeconds);
            }
        }
        #endregion

        #region 私有字段
        /// <summary>
        /// 当前帧索引
        /// </summary>
        private int frameIndex = 0;
        /// <summary>
        /// 帧图片列表
        /// </summary>
        private List<Image> frameImages = new List<Image>();
        /// <summary>
        /// 每帧持续时间（秒）
        /// </summary>
        private float frameSeconds;
        /// <summary>
        /// 帧计时器
        /// </summary>
        private Timer frameTimer;
        #endregion

        public Animation()
        {
            frameTimer = new Timer(FrameSeconds, NextFrame);
            frameTimer.IsLooped = true;
        }

        public Animation(Image image) : this()
        {
            this.frameImages.Add(image);
        }

        public Animation(List<Image> frameImages) : this()
        {
            this.frameImages = frameImages;
        }

        public Animation(List<Image> frameImages, float frameSeconds) : this(frameImages)
        {
            this.FrameSeconds = frameSeconds;
        }

        #region 公共方法
        /// <summary>
        /// 添加帧图片
        /// </summary>
        /// <param name="frameImage"></param>
        public void AddFrame(Image frameImage)
        {
            frameImages.Add(frameImage);
        }

        /// <summary>
        /// 设置帧图片
        /// </summary>
        /// <param name="frameImage"></param>
        public void SetFrame(int index, Image frameImage)
        {
            if (index < 0 || index >= frameImages.Count)
                throw new ArgumentOutOfRangeException("帧索引超出范围！");

            frameImages[index].Dispose();
            frameImages[index] = frameImage;
        }

        /// <summary>
        /// 清除所有帧图片
        /// </summary>
        public void ClearFrames()
        {
            for (int i = 0; i < frameImages.Count; i++)
                frameImages[i].Dispose();
            frameImages.Clear();
            frameIndex = 0;
        }

        /// <summary>
        /// 获取所有帧图片
        /// </summary>
        public List<Image> GetAllFrames()
        {
            // 克隆一份返回，防止外部修改该列表内容
            return frameImages.ToList();
        }

        /// <summary>
        /// 获取当前帧图片
        /// </summary>\
        public Image GetCurrentFrame()
        {
            if (frameImages.Count <= 0)
                return Image.Empty;
            return frameImages[frameIndex];
        }

        /// <summary>
        /// 重新设置每帧持续时间（秒）
        /// </summary>
        public void SetFrameSeconds(float frameSeconds)
        {
            if (frameSeconds <= 0)
                throw new ArgumentOutOfRangeException("每帧持续时间必须大于0秒！");

            FrameSeconds = frameSeconds;
        }

        /// <summary>
        /// 播放
        /// </summary>
        public void Play()
        {
            frameTimer.Start();
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            for (int i = 0; i < frameImages.Count; i++)
                frameImages[i].Dispose();
            frameImages.Clear();

            GC.SuppressFinalize(this);
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 下一帧
        /// </summary>
        private void NextFrame()
        {
            if (frameImages.Count <= 0) // 没有动画，只有一帧图片
                return;

            frameIndex++;
            if (frameIndex >= frameImages.Count)
                frameIndex = 0;
        }
        #endregion
    }
}
