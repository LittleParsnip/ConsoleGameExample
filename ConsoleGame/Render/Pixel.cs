using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    /// <summary>
    /// 像素点
    /// </summary>
    public struct Pixel
    {
        /// <summary>
        /// 横坐标
        /// </summary>
        public int X { get; private set; }
        /// <summary>
        /// 纵坐标
        /// </summary>
        public int Y { get; private set; }
        /// <summary>
        /// 字符
        /// </summary>
        public char Symbol { get; set; }
        /// <summary>
        /// 字符颜色
        /// </summary>
        public ConsoleColor ForegroundColor { get; set; } = ConsoleColor.Gray;
        /// <summary>
        /// 背景色
        /// </summary>
        public ConsoleColor BackgroundColor { get; set; } = ConsoleColor.Black;

        /// <summary>
        /// 位置向量
        /// </summary>
        public ScreenVector2 Position
        { 
            get => new ScreenVector2(X, Y);
        }

        public Pixel(int x, int y, char symbol)
        {
            X = x;
            Y = y;
            Symbol = symbol;
        }

        public Pixel(int x, int y, char symbol, ConsoleColor foregroundColor) : this(x, y, symbol)
        {
            ForegroundColor = foregroundColor;
        }

        public Pixel(int x, int y, char symbol, ConsoleColor foregroundColor, ConsoleColor backgroundColor) : this(x, y, symbol, foregroundColor)
        {
            BackgroundColor = backgroundColor;
        }

        /// <summary>
        /// 设置坐标
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        public void SetPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object? obj)
        {
            return obj is Pixel pixel &&
                   X == pixel.X &&
                   Y == pixel.Y &&
                   Symbol == pixel.Symbol &&
                   ForegroundColor == pixel.ForegroundColor &&
                   BackgroundColor == pixel.BackgroundColor &&
                   EqualityComparer<Vector2>.Default.Equals(Position, pixel.Position);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Symbol, ForegroundColor, BackgroundColor, Position);
        }

        public override string ToString()
        {
            return $"Pixel [X={X}, Y={Y}, Symbol={Symbol}, ForegroundColor={ForegroundColor}, BackgroundColor={BackgroundColor}]";
        }

        public static bool operator ==(Pixel left, Pixel right)
        {
            if (left.X == right.X &&
                left.Y == right.Y &&
                left.Symbol == right.Symbol &&
                left.ForegroundColor == right.ForegroundColor &&
                left.BackgroundColor == right.BackgroundColor)
                return true;
            else
                return false;
        }

        public static bool operator !=(Pixel left, Pixel right)
        {
            return !(left == right);
        }
    }
}
