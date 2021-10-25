using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanoi_Towers
{
    class Tower
    {
        public int Ind { get; }
        private int N { get; }
        public Stack<Hoop> Hoops { get; set; } = new Stack<Hoop>();
        public Tower(int ind, int n)
        {
            Ind = ind;
            N = n;
        }
        public void GetStartHoops()
        {
            for (var i = N; i > 0; i--)
                Hoops.Push(new Hoop(i));
        }
    }
}
