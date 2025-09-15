using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    /// <summary>
    /// 前景
    /// </summary>
    public class Foreground : GameObject
    {
        protected override void Init()
        {
            base.Init();
            Position = new Vector2();
            SetImage(Image.FromResource("Foreground"));
            SetSortingOrder(Renderer.SORTING_ORDER_FOREGROUND);
            Image.SetBackgroundColor(ConsoleColor.Black);
        }
    }
}
