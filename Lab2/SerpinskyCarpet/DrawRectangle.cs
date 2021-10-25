using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SerpinskyCarpet
{    
    public class DrawRectangle
    {
        public Canvas Content { get; } = new Canvas();
        public int N { get; private set; }

        public DrawRectangle(Canvas content, int n)
        {
            Content = content;
            N = n;
        }
        public void DrawCarpet()
        {
            var cell = new Cell(1, 100, 100, 700, 100, 100, 700, 700, 700);
            DrawFrame(cell);
            DrawEnemyCell(cell);
            
        }
        private void DrawFrame(Cell cell)
        {
            Content.Children.Add(GetLine(cell.X1, cell.Y1, cell.X2, cell.Y2));
            Content.Children.Add(GetLine(cell.X1, cell.Y1, cell.X3, cell.Y3));
            Content.Children.Add(GetLine(cell.X2, cell.Y2, cell.X4, cell.Y4));
            Content.Children.Add(GetLine(cell.X3, cell.Y3, cell.X4, cell.Y4));
        }
        private void DrawEnemyCell(Cell cell)
        {
            if (N <= cell.Id) return;

            cell.AddRects();
            cell.AddRect();

            DrawFrame(cell.EnemyCell);

            foreach (var el in cell.Cells)
            {
                if (el == null) continue;
                DrawEnemyCell(el);
            }

        }
        private static Line GetLine(double x1, double y1, double x2, double y2)
            => new()
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Stroke = Brushes.Black,
                StrokeThickness = 2,
            };

    }
}
