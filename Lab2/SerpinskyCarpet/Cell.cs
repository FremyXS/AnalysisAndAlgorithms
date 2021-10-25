using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerpinskyCarpet
{
    class Cell
    {
        public int Id { get; }
        public double X1 { get; }
        public double Y1 { get; }
        public double X2 { get; }
        public double Y2 { get; }
        public double X3 { get; }
        public double Y3 { get; }
        public double X4 { get; }
        public double Y4 { get; }
        public Cell(int id, double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
        {
            Id = id;
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
            X3 = x3;
            Y3 = y3;
            X4 = x4;
            Y4 = y4;
        }

        public Cell EnemyCell { get; private set; }
        public static int N = 6;
        public Cell[,] Cells = new Cell[3, 3];

        public void AddRect()
            => EnemyCell = new Cell
            (
                0,
                Cells[1,0].X2, Cells[1, 0].Y2,
                Cells[1, 2].X1, Cells[1, 2].Y1,
                Cells[1, 0].X4, Cells[1, 0].Y4,
                Cells[1, 2].X3, Cells[1, 2].Y3
            );
        public void AddRects()
        {
            double x = (X2 - X1) / 3;

            for (var i = 0; i<3; i++)
            {
                for(var j = 0; j < 3; j++)
                {
                    if (i == 1 && j == 1) continue;

                    Cells[i, j] = new Cell
                        (
                            Id + 1,
                            X1 + (x * (j + 1) - x), (x * (i + 1) - x) + Y1,
                            (x * (j + 1)) + X1, (x * (i + 1) - x) + Y1,
                            (x * (j + 1) - x) + X1, (x * (i + 1)) + Y1,
                            (x * (j + 1)) + X1, (x * (i + 1)) + Y1
                        );
                }
            }
        }

    }
}
