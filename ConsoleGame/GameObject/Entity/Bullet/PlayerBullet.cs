using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public class PlayerBullet : BulletBase
    {
        protected override List<int> CollideTargetSortingOrder => new List<int>()
        {
            Renderer.SORTING_ORDER_ENEMY,
            Renderer.SORTING_ORDER_BOSS
        };

        #region 重写
        protected override void Init()
        {
            base.Init();

            Speed = 80.0f;
            SetImage(Image.FromResource("PlayerBullet"));
            // 增大碰撞箱
            Image.AddPixel(new Pixel(-1, 0, ' '));
            Image.AddPixel(new Pixel(1, 0, ' '));
            Image.AddPixel(new Pixel(0, 1, ' '));
            Image.AddPixel(new Pixel(0, -1, ' '));
            Image.AddPixel(new Pixel(-1, 1, ' '));
            Image.AddPixel(new Pixel(1, 1, ' '));
            Image.AddPixel(new Pixel(-1, 1, ' '));
            Image.AddPixel(new Pixel(-1, -1, ' '));

            Image.SetForegroundColor(ConsoleColor.White);
            SetSortingOrder(Renderer.SORTING_ORDER_PLAYERBULLET);
        }

        protected override void Update(float deltaSeconds)
        {
            base.Update(deltaSeconds);

        }

        protected override void OnCollide(GameObject other)
        {
            base.OnCollide(other);

            EnemyBase enemy = (EnemyBase)other;
            HitEnemy(enemy);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

        }
        #endregion

        #region 公共方法

        #endregion

        #region 私有方法
        private void HitEnemy(EnemyBase enemy)
        {
            enemy.Hurt(10);
            Dispose();
        }
        #endregion
    }
}
