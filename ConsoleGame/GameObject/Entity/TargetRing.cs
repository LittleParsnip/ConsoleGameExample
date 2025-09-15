using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    /// <summary>
    /// 目标环
    /// </summary>
    public class TargetRing : GameObject
    {
        protected override List<int> CollideTargetSortingOrder => new List<int>()
        {
            Renderer.SORTING_ORDER_PLAYER
        };

        #region 重写
        protected override void Init()
        {
            base.Init();

            List<Image> images = new List<Image>()
            {
                Image.FromResource("TargetRing_1"),
                Image.FromResource("TargetRing_2"),
            };
            foreach (var i in images)
                i.SetForegroundColor(ConsoleColor.Yellow);

            SetAnimation(new Animation(images, 0.4f));
            SetSortingOrder(Renderer.SORTING_ORDER_TARGET_RING);
        }

        protected override void Update(float deltaSeconds)
        {
            base.Update(deltaSeconds);

        }

        protected override void OnCollide(GameObject other)
        {
            base.OnCollide(other);

            // 碰到玩家就消失
            Dispose();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

        }
        #endregion

        #region 公共方法

        #endregion

        #region 私有方法

        #endregion
    }
}
