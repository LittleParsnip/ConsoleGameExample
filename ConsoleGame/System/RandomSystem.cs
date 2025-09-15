using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public static class RandomSystem
    {
        private static Random random = new Random();

        /// <summary>
        /// 获取一个随机整数，范围在 [min, max) 之间
        /// </summary>
        /// <param name="min">最小值（包含）</param>
        /// <param name="max">最大值（不包含）</param>
        /// <returns>随机整数</returns>
        public static int Range(int min, int max)
        {
            return random.Next(min, max);
        }

        /// <summary>
        /// 获取一个随机浮点数，范围在 [min, max) 之间
        /// </summary>
        /// <param name="min">最小值（包含）</param>
        /// <param name="max">最大值（不包含）</param>
        /// <returns>随机浮点数</returns>
        public static float Range(float min, float max)
        {
            return (float)(random.NextDouble() * (max - min) + min);
        }
    }
}
