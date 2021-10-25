using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Hanoi_Towers
{
    class Hoop
    {
        public int Ind { get; }
        public Rectangle Rec { get; } = new Rectangle();
        public Hoop(int ind)
        {
            Ind = ind;
            Rec = GetRectangle();
        }
        private Rectangle GetRectangle()
        {
            return new Rectangle
            {
                Height = 10,
                Width = 100 + 10 * Ind,
                Stroke = Brushes.Black
            };
        }
    }
}
