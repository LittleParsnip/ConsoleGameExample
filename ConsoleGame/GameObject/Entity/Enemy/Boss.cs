using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public class Boss : EnemyBase
    {
        /// <summary>
        /// 初始血量
        /// </summary>
        private const int MAX_HP = 1500;
        /// <summary>
        /// 移动速度
        /// </summary>
        private const float MOVE_SPEED = 3.0f;
        /// <summary>
        /// 切换移动方向间隔（秒）
        /// </summary>
        private const float CHANGE_MOVE_DIRECTION_INTERVAL = 4.0f;
        /// <summary>
        /// 发射间隔（秒）
        /// </summary>
        private const float SHOOT_INTERVAL = 2.5f;
        /// <summary>
        /// 爆炸间隔（秒）
        /// </summary>
        private const float BOOM_INTERVAL = 4.5f;

        private HpBar? bossHpBar;
        private Timer? shootTimer;
        private Timer? boomTimer;

        private Vector2 moveDirection = Vector2.Up;
        private Timer? changeDirectionTimer;

        #region 重写
        protected override void Init()
        {
            base.Init();

            List<Image> images = new List<Image>()
            {
                Image.FromResource("Boss_1"),
                Image.FromResource("Boss_2"),
            };
            foreach (var i in images)
            {
                i.SetForegroundColor(originalColor);
            }
            SetAnimation(new Animation(images, 0.6f));
            SetSortingOrder(Renderer.SORTING_ORDER_BOSS);

            bossHpBar = new HpBar();
            bossHpBar.Position = new Vector2(50, 0);

            Hp = MAX_HP;

            shootTimer = new Timer(SHOOT_INTERVAL, Shoot);
            shootTimer.IsLooped = true;
            shootTimer.Start();

            boomTimer = new Timer(BOOM_INTERVAL, CreateBoom);
            boomTimer.IsLooped = true;
            boomTimer.Start();

            changeDirectionTimer = new Timer(CHANGE_MOVE_DIRECTION_INTERVAL, ChangeMoveDirection);
            changeDirectionTimer.IsLooped = true;
            changeDirectionTimer.Start();
        }

        protected override void Update(float deltaSeconds)
        {
            base.Update(deltaSeconds);

            Position += moveDirection * MOVE_SPEED * Time.DeltaSeconds;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            shootTimer?.Dispose();
            boomTimer?.Dispose();
            changeDirectionTimer?.Dispose();
        }

        protected override void OnHpChanged(int deltaValue)
        {
            base.OnHpChanged(deltaValue);
            bossHpBar?.SetValue((float)Hp / MAX_HP);
        }

        protected override void Die()
        {
            base.Die();
            FinalShoot();
        }
        #endregion

        #region 公共方法

        #endregion

        #region 私有方法
        private void ChangeMoveDirection()
        {
            moveDirection = -moveDirection;
        }

        private void Shoot()
        {
            int totalCount = 45;
            float initialAngle = RandomSystem.Range(0, 360.0f);
            for (int i = 0; i < totalCount; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    float angle = initialAngle + 360.0f / totalCount * i;
                    Vector2 dir = Vector2.FromDegrees(angle);
                    EnemyBullet bullet = new EnemyBullet();
                    bullet.Position = Position + new Vector2(2, 1);
                    bullet.Direction = dir;
                    bullet.Speed -= j * 10;
                    if (j == 2)
                        bullet.Image.SetForegroundColor(ConsoleColor.DarkRed);
                }
            }
        }

        private void CreateBoom()
        {
            for (int i = 0; i < 5; i++)
            {
                float randX = RandomSystem.Range(0, (WORLD_WIDTH / 3));
                float randY = RandomSystem.Range(0, WORLD_HEIGHT - 5);
                Vector2 pos = new Vector2(randX, randY);
                Boom boom = new Boom();
                boom.Position = pos;
            }
        }

        private void FinalShoot()
        {
            int totalCount = 45;
            float initialAngle = RandomSystem.Range(0, 360.0f);
            for (int i = 0; i < totalCount; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    float angle = initialAngle + 360.0f / totalCount * i;
                    Vector2 dir = Vector2.FromDegrees(angle);
                    EnemyBullet bullet = new EnemyBullet();
                    bullet.Position = Position + new Vector2(2, 1);
                    bullet.Direction = dir;
                    bullet.Speed -= j * 3;
                    bullet.Image.SetForegroundColor(ConsoleColor.Green);
                    if (j == 2)
                        bullet.Image.SetForegroundColor(ConsoleColor.Yellow);
                    if (j == 3)
                        bullet.Image.SetForegroundColor(ConsoleColor.Cyan);
                    if (j == 4)
                        bullet.Image.SetForegroundColor(ConsoleColor.Cyan);
                    if (j == 5)
                        bullet.Image.SetForegroundColor(ConsoleColor.Blue);
                    if (j == 6)
                        bullet.Image.SetForegroundColor(ConsoleColor.Blue);
                }
            }
        }
        #endregion
    }
}
