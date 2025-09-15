using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    /// <summary>
    /// 背景
    /// </summary>
    public class Background : GameObject
    {
        protected override void Init()
        {
            base.Init();
            Position = new Vector2();
            SetImage(Image.FromResource("Background"));
            SetSortingOrder(Renderer.SORTING_ORDER_BACKGROUND);
            Image.SetBackgroundColor(ConsoleColor.Black);
        }
    }
}
