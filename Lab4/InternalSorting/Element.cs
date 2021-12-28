using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace InternalSorting
{
    public class Element: ICloneable
    {
        public Rectangle Rec;
        public int Num;

        public Element(int x)
        {
            Rec = new Rectangle { Width = 2, Height = 100 + x, Stroke = Brushes.Black, Margin = new System.Windows.Thickness(5, 0, 0, 0) };
            Num = x;
        }

        public object Clone()
        {
            return new Element(this.Num);
        }
        public override bool Equals(object obj)
        {
            return ((Element)obj).Num == this.Num;
        }
    }
}
