using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public interface IHurtable
    {
        public int Hp { get; set; }

        public void Hurt(int damage);
    }
}
