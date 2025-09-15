using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public abstract class BulletBase : GameObject
    {
        public float Speed { get; set; } = 80f;
        public Vector2 Direction
        {
            get => direction;
            set
            {
                direction = value;

                /* 有方向的字符
                
                // 计算角度（0-360度）
                float angle = (float)(Math.Atan2(-direction.Y, direction.X) * 180 / Math.PI);
                if (angle < 0) angle += 360;

                // 根据角度选择对应的方向字符
                string str = GetDirectionCharacter(angle);

                ConsoleColor originalColor = Image.GetPixels()[0].ForegroundColor;
                SetImage(Image.FromString(str));
                Image.SetForegroundColor(originalColor);
                */
            }
        }

        private Vector2 direction = Vector2.Right;

        #region 重写
        protected override void Init()
        {
            base.Init();

        }

        protected override void Update(float deltaSeconds)
        {
            base.Update(deltaSeconds);
            Position += Direction * Speed * deltaSeconds;

            // 出界销毁
            if (IsOutOfBounds)
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
        private string GetDirectionCharacter(float angle)
        {
            // 使用ASCII字符表示8个方向
            if (angle >= 337.5f || angle < 22.5f) return ">";  // 右
            if (angle >= 22.5f && angle < 67.5f) return "/";   // 右上
            if (angle >= 67.5f && angle < 112.5f) return "^";  // 上
            if (angle >= 112.5f && angle < 157.5f) return "\\"; // 左上
            if (angle >= 157.5f && angle < 202.5f) return "<";  // 左
            if (angle >= 202.5f && angle < 247.5f) return "/";  // 左下（注意：与右上相同）
            if (angle >= 247.5f && angle < 292.5f) return "v";  // 下
            if (angle >= 292.5f && angle < 337.5f) return "\\"; // 右下（注意：与左上相同）

            return "-"; // 默认字符
        }
        #endregion
    }
}
