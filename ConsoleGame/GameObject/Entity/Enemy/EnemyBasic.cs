using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    /// <summary>
    /// 普通敌人
    /// </summary>
    public class EnemyBasic : EnemyBase
    {
        private Timer? shootTimer;

        #region 重写
        protected override void Init()
        {
            base.Init();
            List<Image> images = new List<Image>()
            {
                Image.FromResource("Enemy_1"),
                Image.FromResource("Enemy_2"),
            };
            foreach (var i in images)
            {
                i.SetForegroundColor(originalColor);
            }
            SetAnimation(new Animation(images, 0.3f));
            SetSortingOrder(Renderer.SORTING_ORDER_ENEMY);

            shootTimer = new Timer(0.7f, Shoot);
            shootTimer.IsLooped = true;
            shootTimer.Start();
        }

        protected override void Update(float deltaSeconds)
        {
            base.Update(deltaSeconds);

        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            shootTimer?.Dispose();
        }
        #endregion

        #region 公共方法

        #endregion

        #region 私有方法
        private void Shoot()
        {
            float angle = RandomSystem.Range(0, 360.0f);
            Vector2 dir = Vector2.FromDegrees(angle);
            EnemyBullet bullet = new EnemyBullet();
            bullet.Position = Position;
            bullet.Direction = dir;
        }
        #endregion
    }
}
