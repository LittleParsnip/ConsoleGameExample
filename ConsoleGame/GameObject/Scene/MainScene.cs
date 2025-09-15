using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    /// <summary>
    /// 主场景，一切的开始（主函数默认创建，建议将游戏内容写到这里而不是Program.cs）
    /// </summary>
    public class MainScene : GameObject
    {
        private enum LevelState
        {
            None = 0,
            EnemyState = 1,
            TargetRingState = 2,
            BossState = 3,
            Over = 4
        }

        private Background? background;
        private Foreground? foreground;
        private Player? player;

        private Text pauseText = new Text(new Vector2(WORLD_WIDTH / 2 - 6, WORLD_HEIGHT / 2), "");

        private LevelState levelState = LevelState.None;
        private List<EnemyBase> enemies = new List<EnemyBase>();
        private List<TargetRing> targetRings = new List<TargetRing>();
        private Boss? boss;

        protected override void Init()
        {
            base.Init();
            background = new Background();
            foreground = new Foreground();
            player = new Player();

            pauseText.TextColor = ConsoleColor.Yellow;
            pauseText.BackgroundColor = ConsoleColor.DarkGray;

            SwitchLevelState(LevelState.EnemyState);
        }

        protected override void Update(float deltaSeconds)
        {
            base.Update(deltaSeconds);

            // 暂停功能
            if (InputSystem.IsKeyDown(ConsoleKey.P))
            {
                Time.SetTimeScale(1 - Time.TimeScale);
                if (Time.TimeScale > 0)
                    pauseText.Content = "";
                else
                    pauseText.Content = "---Pausing---";
            }

            FSM();
        }

        /// <summary>
        /// 状态检测
        /// </summary>
        private void FSM()
        {
            // 自动清理列表
            for (int i = enemies.Count - 1; i >= 0; i--)
                if (enemies[i] == null || !GameObjectPool.GetGameObjectList().Contains(enemies[i]))
                    enemies.RemoveAt(i);
            for (int i = targetRings.Count - 1; i >= 0; i--)
                if (targetRings[i] == null || !GameObjectPool.GetGameObjectList().Contains(targetRings[i]))
                    targetRings.RemoveAt(i);
            if (boss != null && !GameObjectPool.GetGameObjectList().Contains(boss))
                boss = null;

            switch (levelState)
            {
                case LevelState.EnemyState:
                    if (enemies.Count <= 0)
                    {
                        SwitchLevelState(LevelState.TargetRingState);
                    }
                    break;
                case LevelState.TargetRingState:
                    if (targetRings.Count <= 0)
                    {
                        // 清除所有敌人
                        for (int i = enemies.Count - 1; i >= 0; i--)
                            enemies[i].Dispose();

                        SwitchLevelState(LevelState.BossState);
                    }
                    break;
                case LevelState.BossState:
                    if (boss == null)
                    {
                        // 清除所有敌人
                        for (int i = enemies.Count - 1; i >= 0; i--)
                            enemies[i].Dispose();

                        SwitchLevelState(LevelState.Over);
                    }
                    break;
            }
        }

        /// <summary>
        /// 切换游戏状态
        /// </summary>
        /// <param name="levelState"></param>
        private void SwitchLevelState(LevelState levelState)
        {
            this.levelState = levelState;
            switch (levelState)
            {
                case LevelState.EnemyState:
                    CreateEnemy(new Vector2(45, 20));
                    CreateEnemy(new Vector2(50, 20));
                    CreateEnemy(new Vector2(45, 35));
                    break;
                case LevelState.TargetRingState:
                    // 随机生成敌人和目标环
                    for (int i = 0; i < 10; i++)
                    {
                        int randX = RandomSystem.Range(0, WORLD_WIDTH);
                        int randY = RandomSystem.Range(0, WORLD_HEIGHT);
                        Vector2 createPos = new Vector2(randX, randY);
                        CreateEnemy(createPos);
                    }
                    for (int i = 0; i < 6; i++)
                    {
                        int randX = RandomSystem.Range(5, WORLD_WIDTH-5);
                        int randY = RandomSystem.Range(5, WORLD_HEIGHT-5);
                        Vector2 createPos = new Vector2(randX, randY);
                        CreateTargetRing(createPos);
                    }
                    break;
                case LevelState.BossState:
                    CreateEnemy(new Vector2(50, 20));
                    CreateEnemy(new Vector2(45, 30));
                    CreateEnemy(new Vector2(50, 40));
                    CreateEnemy(new Vector2(40, 20));
                    CreateEnemy(new Vector2(40, 40));

                    Boss boss = new Boss();
                    boss.Position = new Vector2(60, 35);
                    this.boss = boss;
                    break;
                case LevelState.Over:
                    Text levelOverText = new Text(new Vector2(WORLD_WIDTH / 2 - 13, WORLD_HEIGHT / 2), "---Thanks~For~Playing!---");
                    levelOverText.TextColor = ConsoleColor.Yellow;
                    levelOverText.Image.AddPixel(new Pixel(14, 0, 'P', ConsoleColor.Blue));
                    levelOverText.BackgroundColor = ConsoleColor.DarkGray;
                    break;
            }
        }

        /// <summary>
        /// 创建敌人
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private EnemyBase CreateEnemy(Vector2 pos)
        {
            EnemyBase enemy = new EnemyBasic();
            enemy.Position = pos;
            enemies.Add(enemy);

            return enemy;
        }

        /// <summary>
        /// 创建目标环
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private TargetRing CreateTargetRing(Vector2 pos)
        {
            TargetRing ring = new TargetRing();
            ring.Position = pos;
            targetRings.Add(ring);

            return ring;
        }
    }
}
