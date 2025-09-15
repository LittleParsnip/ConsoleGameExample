using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public class Text : GameObject
    {
        #region 属性
        /// <summary>
        /// 文本内容
        /// </summary>
        public string Content
        {
            get => content;
            set
            {
                content = value;
                SetImage(Image.FromString(content));
                Image.SetForegroundColor(TextColor);
                Image.SetBackgroundColor(BackgroundColor);
            }
        }
        /// <summary>
        /// 字体颜色
        /// </summary>
        public ConsoleColor TextColor
        {
            get => textColor;
            set
            {
                textColor = value;
                Image.SetForegroundColor(TextColor);
            }
        }
        /// <summary>
        /// 背景颜色
        /// </summary>
        public ConsoleColor BackgroundColor
        {
            get => backgroundColor;
            set
            {
                backgroundColor = value;
                Image.SetBackgroundColor(BackgroundColor);
            }
        }
        #endregion

        #region 私有字段
        private string content = "";
        private ConsoleColor textColor;
        private ConsoleColor backgroundColor;
        #endregion

        public Text(string content) : base()
        {
            Content = content;
        }

        public Text(Vector2 pos) : base()
        {
            SetPosition(pos);
        }

        public Text(Vector2 pos, string content) : this(content)
        {
            SetPosition(pos);
        }

        #region 重写
        protected override void Init()
        {
            base.Init();

            SetSortingOrder(Renderer.SORTING_ORDER_UI);
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

        #endregion

        #region 私有方法

        #endregion
    }
}
