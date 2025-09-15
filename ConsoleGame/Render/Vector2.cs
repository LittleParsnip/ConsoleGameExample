using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    /// <summary>
    /// 二维向量
    /// </summary>
    public struct Vector2
    {
        public static readonly Vector2 Right = new Vector2(1, 0);
        public static readonly Vector2 Left = new Vector2(-1, 0);
        public static readonly Vector2 Up = new Vector2(0, -1);
        public static readonly Vector2 Down = new Vector2(0, 1);

        public float X { get; set; }
        public float Y { get; set; }

        public Vector2()
        {

        }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X + right.X, left.Y + right.Y);
        }

        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X - right.X, left.Y - right.Y);
        }

        public static Vector2 operator -(Vector2 vec)
        {
            return new Vector2(-vec.X, -vec.Y);
        }

        public static Vector2 operator *(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X * right.X, left.Y * right.Y);
        }

        public static Vector2 operator /(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X / right.X, left.Y / right.Y);
        }

        public static Vector2 operator *(Vector2 left, float right)
        {
            return new Vector2(left.X * right, left.Y * right);
        }

        public static Vector2 operator /(Vector2 left, float right)
        {
            return new Vector2(left.X / right, left.Y / right);
        }

        /// <summary>
        /// 隐式转换到IntVector2
        /// </summary>
        public static implicit operator ScreenVector2(Vector2 vector)
        {
            return vector.ToScreenVector2();
        }

        public static bool operator ==(Vector2 left, Vector2 right)
        {
            if (left.X == right.X &&
                left.Y == right.Y)
                return true;
            else
                return false;
        }

        public static bool operator !=(Vector2 left, Vector2 right)
        {
            return !(left == right);
        }

        public override bool Equals(object? obj)
        {
            return obj is Vector2 vector &&
                   X == vector.X &&
                   Y == vector.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public ScreenVector2 ToScreenVector2()
        {
            // return new IntVector2((int)Math.Round(X, 0), (int)Math.Round(Y, 0));
            ScreenVector2 screenVector = new ScreenVector2((int)X, (int)Y);
            if (!Program.IS_SQUARE_FONT) // 与屏幕坐标以2:1换算
                screenVector.Y /= 2;
            return screenVector;
        }

        /// <summary>
        /// 根据角度获取单位向量
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        public static Vector2 FromDegrees(float degrees)
        {
            float radians = degrees * (float)(Math.PI / 180);
            return new Vector2((float)Math.Cos(radians), -(float)Math.Sin(radians));
        }
    }

    /// <summary>
    /// 屏幕二维向量（整数）（如果字体是长宽2:1的话，与Vector在Y坐标的换算也是2:1）
    /// </summary>
    public struct ScreenVector2
    {
        public static readonly ScreenVector2 Right = new ScreenVector2(1, 0);
        public static readonly ScreenVector2 Left = new ScreenVector2(-1, 0);
        public static readonly ScreenVector2 Up = new ScreenVector2(0, -1);
        public static readonly ScreenVector2 Down = new ScreenVector2(0, 1);

        public int X { get; set; }
        public int Y { get; set; }

        public ScreenVector2()
        {

        }

        public ScreenVector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static ScreenVector2 operator +(ScreenVector2 left, ScreenVector2 right)
        {
            return new ScreenVector2(left.X + right.X, left.Y + right.Y);
        }

        public static ScreenVector2 operator -(ScreenVector2 left, ScreenVector2 right)
        {
            return new ScreenVector2(left.X - right.X, left.Y - right.Y);
        }

        public static ScreenVector2 operator -(ScreenVector2 vec)
        {
            return new ScreenVector2(-vec.X, -vec.Y);
        }

        public static ScreenVector2 operator *(ScreenVector2 left, ScreenVector2 right)
        {
            return new ScreenVector2(left.X * right.X, left.Y * right.Y);
        }

        public static ScreenVector2 operator /(ScreenVector2 left, ScreenVector2 right)
        {
            return new ScreenVector2(left.X / right.X, left.Y / right.Y);
        }

        public static ScreenVector2 operator *(ScreenVector2 left, int right)
        {
            return new ScreenVector2(left.X * right, left.Y * right);
        }

        public static ScreenVector2 operator /(ScreenVector2 left, int right)
        {
            return new ScreenVector2(left.X / right, left.Y / right);
        }

        /// <summary>
        /// 隐式转换到Vector2
        /// </summary>
        public static implicit operator Vector2(ScreenVector2 intVector)
        {
            return new Vector2(intVector.X, intVector.Y);
        }

        public static bool operator ==(ScreenVector2 left, ScreenVector2 right)
        {
            if (left.X == right.X &&
                left.Y == right.Y)
                return true;
            else
                return false;
        }

        public static bool operator !=(ScreenVector2 left, ScreenVector2 right)
        {
            return !(left == right);
        }

        public override bool Equals(object? obj)
        {
            return obj is ScreenVector2 vector &&
                   X == vector.X &&
                   Y == vector.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
