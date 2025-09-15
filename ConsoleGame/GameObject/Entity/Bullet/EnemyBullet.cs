using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public class EnemyBullet : BulletBase
    {
        protected override List<int> CollideTargetSortingOrder => new List<int>()
        {
            Renderer.SORTING_ORDER_PLAYER
        };

        #region 重写
        protected override void Init()
        {
            base.Init();

            Speed = 25.0f;
            SetImage(Image.FromResource("EnemyBullet"));
            Image.SetForegroundColor(ConsoleColor.Red);
            SetSortingOrder(Renderer.SORTING_ORDER_ENEMYBULLET);
        }

        protected override void Update(float deltaSeconds)
        {
            base.Update(deltaSeconds);

        }

        protected override void OnCollide(GameObject other)
        {
            base.OnCollide(other);

            Player player = (Player)other;
            HitPlayer(player);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

        }
        #endregion

        #region 公共方法

        #endregion

        #region 私有方法
        private void HitPlayer(Player player)
        {
            player.Hurt(10);
            Dispose();
        }
        #endregion
    }
}
