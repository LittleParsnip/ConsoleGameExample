using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public class HpBar : GameObject
    {
        public ConsoleColor BarColor { get; set; } = ConsoleColor.Red;

        private float percentage = 1.0f;

        #region 重写
        protected override void Init()
        {
            base.Init();

            SetSortingOrder(Renderer.SORTING_ORDER_UI);
            SetValue(percentage);
        }

        protected override void Update(float deltaSeconds)
        {
            base.Update(deltaSeconds);

        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 设置数值
        /// </summary>
        /// <param name="newPercentage">新数值</param>
        public void SetValue(float newPercentage)
        {
            percentage = Math.Clamp(newPercentage, 0f, 1f);

            int totalWidth = 20; // 总宽度
            int filledWidth = (int)Math.Ceiling(totalWidth * percentage); // 填充宽度
            int emptyWidth = totalWidth - filledWidth; // 空白宽度
            string filledPart = new string('█', filledWidth);
            string emptyPart = new string(' ', emptyWidth);
            string percentText = (percentage * 100.0f).ToString("0.0");
            string fullStr = $"{filledPart}{emptyPart} {percentText}%";

            SetImage(Image.FromString(fullStr));
            Image.SetForegroundColor(BarColor);
        }
        #endregion

        #region 私有方法

        #endregion
    }
}
