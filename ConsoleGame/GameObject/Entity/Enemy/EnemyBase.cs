using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public abstract class EnemyBase : GameObject, IHurtable
    {
        public int Hp
        {
            get => hp;
            set
            {
                int deltaValue = value - hp;
                hp = value;

                if (hp <= 0)
                {
                    hp = 0;
                    // 死亡
                    Die();
                }

                OnHpChanged(deltaValue);
            }
        }

        private int hp = 100;
        /// <summary>
        /// 受伤动画计数器
        /// </summary>
        private int hurtAnimCounter = 0;
        /// <summary>
        /// 原颜色
        /// </summary>
        protected ConsoleColor originalColor = ConsoleColor.Magenta;

        #region 虚函数
        protected virtual void Die()
        {
            Dispose();
        }

        protected virtual void OnHpChanged(int deltaValue)
        {

        }
        #endregion

        #region 重写
        protected override void Init()
        {
            base.Init();
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
        public void Hurt(int damage)
        {
            Hp -= damage;

            HurtAnimStart();
            Timer hurtAnimTimer = new Timer(0.05f, HurtAnimOver);
            hurtAnimTimer.Start();
        }
        #endregion

        #region 私有方法
        private void HurtAnimStart()
        {
            hurtAnimCounter++;
            foreach (var i in Animation.GetAllFrames())
                i.SetForegroundColor(ConsoleColor.White);
        }

        private void HurtAnimOver()
        {
            hurtAnimCounter--;
            if (hurtAnimCounter <= 0)
            {
                foreach (var i in Animation.GetAllFrames())
                    i.SetForegroundColor(originalColor);
            }
        }
        #endregion
    }
}
