using ConsoleGame.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public class Player : GameObject, IHurtable
    {
        private const float MOVE_SPEED = 20.0f;
        private const int MAX_HP = 120;
        private const float SHOOT_COOLDOWN_SECONDS = 0.1f;

        public int Hp
        {
            get => hp;
            set
            {
                hp = value;
                if (hp <= 0)
                {
                    hp = 0;
                    // 玩家死亡
                    Die();
                }
                playerHpBar?.SetValue((float)hp / MAX_HP);
            }
        }

        private int hp = MAX_HP;
        private HpBar? playerHpBar;
        private Timer? shootTimer;

        #region 重写
        protected override void Init()
        {
            base.Init();
            Position = new Vector2(10, 10);
            List<Image> images = new List<Image>()
            {
                Image.FromResource("Player_1"),
                Image.FromResource("Player_2"),
            };
            foreach (var i in images)
            {
                i.SetForegroundColor(ConsoleColor.Blue);
            }
            SetAnimation(new Animation(images, 0.5f));
            SetSortingOrder(Renderer.SORTING_ORDER_PLAYER);

            playerHpBar = new HpBar();
            playerHpBar.Position = new Vector2(15, 0);
            playerHpBar.BarColor = ConsoleColor.Green;

            Hp = MAX_HP;
        }

        protected override void Update(float deltaSeconds)
        {
            base.Update(deltaSeconds);

            ProcessInput();
        }
        #endregion

        #region 公共方法
        public void Hurt(int damage)
        {
            Hp -= damage;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 处理输入
        /// </summary>
        private void ProcessInput()
        {
            Move();
            Shoot();
        }

        private void Move()
        {
            // 确保刚按下时能够移动，不损坏手感
            if (InputSystem.IsKeyDown(ConsoleKey.RightArrow))
            {
                Vector2 pos = Position;
                pos.X = (float)Math.Ceiling(pos.X);
                Position = pos;
            }
            if (InputSystem.IsKeyDown(ConsoleKey.LeftArrow))
            {
                Vector2 pos = Position;
                pos.X = (float)Math.Floor(pos.X);
                Position = pos;
            }
            if (InputSystem.IsKeyDown(ConsoleKey.UpArrow))
            {
                Vector2 pos = Position;
                pos.Y = (float)Math.Floor(pos.Y);
                Position = pos;
            }
            if (InputSystem.IsKeyDown(ConsoleKey.DownArrow))
            {
                Vector2 pos = Position;
                pos.Y = (float)Math.Ceiling(pos.Y);
                Position = pos;
            }

            // 按住移动
            float moveDistance = MOVE_SPEED * Time.DeltaSeconds;
            if (InputSystem.IsKeyPressed(ConsoleKey.RightArrow))
            {
                Position += Vector2.Right * moveDistance;
            }
            if (InputSystem.IsKeyPressed(ConsoleKey.LeftArrow))
            {
                Position += Vector2.Left * moveDistance;
            }
            if (InputSystem.IsKeyPressed(ConsoleKey.UpArrow))
            {
                Position += Vector2.Up * moveDistance;
            }
            if (InputSystem.IsKeyPressed(ConsoleKey.DownArrow))
            {
                Position += Vector2.Down * moveDistance;
            }

            // 边界检测
            if (Position.X < 0) Position = new Vector2(0, Position.Y);
            if (Position.Y < 0) Position = new Vector2(Position.X, 0);
            if (Position.X >= WORLD_WIDTH) Position = new Vector2(WORLD_WIDTH - 1, Position.Y);
            if (Position.Y >= WORLD_HEIGHT) Position = new Vector2(Position.X, WORLD_HEIGHT - 1);
        }

        private void Shoot()
        {
            if (shootTimer != null)
                return;

            // 方向按键
            Vector2 direction = new Vector2(0, 0);
            if (InputSystem.IsKeyPressed(ConsoleKey.D))
                direction = Vector2.Right;
            else if (InputSystem.IsKeyPressed(ConsoleKey.A))
                direction = Vector2.Left;
            else if (InputSystem.IsKeyPressed(ConsoleKey.W))
                direction = Vector2.Up;
            else if (InputSystem.IsKeyPressed(ConsoleKey.S))
                direction = Vector2.Down;

            // 没有按按键
            if (direction == new Vector2(0, 0))
                return;

            // 发射子弹
            BulletBase bullet = new PlayerBullet();
            bullet.Position = this.Position;
            bullet.Direction = direction;

            // 开启冷却
            shootTimer = new Timer(SHOOT_COOLDOWN_SECONDS, ResetShootCooldown);
            shootTimer.Start();
        }

        /// <summary>
        /// 重置冷却
        /// </summary>
        private void ResetShootCooldown()
        {
            shootTimer = null;
        }

        private void Die()
        {
            MessageBoxHelper.ShowMessage("游戏结束，你失败了！", "失败");
            Dispose();
        }
        #endregion
    }
}
