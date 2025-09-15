using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public class Boom : GameObject
    {
        private bool isBooming = false;
        private bool haveHurtPlayer = false;

        private Timer? boomTimer;

        #region 重写
        protected override List<int> CollideTargetSortingOrder => new List<int>()
        {
            Renderer.SORTING_ORDER_PLAYER,
        };

        protected override void Init()
        {
            base.Init();

            SetImage(Image.FromResource("BoomWarning"));
            Image.SetForegroundColor(ConsoleColor.DarkRed);
            Image.SetBackgroundColor(ConsoleColor.DarkRed);
            SetSortingOrder(Renderer.SORTING_ORDER_WARNING_ZONE);

            boomTimer = new Timer(1.5f, StartBoom);
            boomTimer.Start();
        }

        protected override void Update(float deltaSeconds)
        {
            base.Update(deltaSeconds);

        }

        protected override void OnCollide(GameObject other)
        {
            base.OnCollide(other);

            if (isBooming && !haveHurtPlayer)
            {
                Player player = (Player)other;
                player.Hurt(25);
                haveHurtPlayer = true;
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

        }
        #endregion

        #region 公共方法

        #endregion

        #region 私有方法
        private void StartBoom()
        {
            isBooming = true;

            // 动画
            List<Image> images = new List<Image>()
            {
                Image.FromResource("Boom_1"),
                Image.FromResource("Boom_2"),
                Image.FromResource("Boom_3"),
                Image.FromResource("Boom_4"),
                Image.FromString(""),
            };
            foreach (var i in images)
                i.SetForegroundColor(ConsoleColor.Red);
            SetAnimation(new Animation(images, 0.2f));
            SetSortingOrder(Renderer.SORTING_ORDER_ENEMYBULLET);

            // 自动销毁
            Timer disposeTimer = new Timer(0.8f, Dispose);
            disposeTimer.Start();
        }
        #endregion
    }
}
