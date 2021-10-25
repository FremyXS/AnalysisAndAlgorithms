using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
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

namespace Newton
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
		public Bitmap bmp = new Bitmap(1, 1);
		public int W = 600;
		public double rmin = 0.01;
		public int kmax = 80;
		public int[,] M;
		public double size = 2.0;
		public double minx = -1.0, miny = -1.0;
		public int N = 5;
		public System.Drawing.Color color = System.Drawing.Color.Blue;
		public System.Drawing.Color back = System.Drawing.Color.White;
		public MainWindow()
        {
            InitializeComponent();

			bmp = new Bitmap(W, W);
			M = new int[W, W];
			calc(); //рассчитать
			draw(); //нарисовать
		}
		void calc()
		{
			double xt, yt;
			Complex c = new Complex();
			Complex cn = new Complex();
			Complex ct;
			int k;
			for (int i = 0; i < W; i++)
			{
				for (int j = 0; j < W; j++)
				{ //начальные значения
					c.re = minx + (i * size) / (double)W;
					c.im = miny + (j * size) / (double)W;
					for (k = 0; k < kmax; k++)
					{
						//Вычисление c - (c^N - 1) / (c^(N - 1) * N)
						cn = c.pow(N) + (-1.0);
						cn = cn / ((c.pow(N - 1)) * ((double)N));
						cn = c - cn;
						ct = cn.pow(N);
						xt = ct.re;
						yt = ct.im;
						if (Math.Abs(xt * xt + yt * yt - 1) < rmin * rmin) //условие остановки
							break;
						c = new Complex(cn);
					}
					M[i, j] = k;
				}

			}
		}

        private void content_Loaded(object sender, RoutedEventArgs e)
        {
			img.Source = BmpImageFromBmp(bmp);
		}

        void draw()
		{
			Graphics g = Graphics.FromImage(bmp);
			g.Clear(back); //заливка фона
			int col;
			for (int i = 0; i < W; i++)
				for (int j = 0; j < W; j++)
				{
					col = (M[i, j]*255)/kmax; //вычисление прозрачности цвета в текущей точке
                    System.Drawing.Pen p = new System.Drawing.Pen(System.Drawing.Color.FromArgb(col, color));
					g.DrawRectangle(p, i, j, 1, 1);
					p.Dispose();
				}
			g.Dispose();

		}		
		private BitmapImage BmpImageFromBmp(Bitmap bmp)
		{
			using (var memory = new System.IO.MemoryStream())
			{
				bmp.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
				memory.Position = 0;

				var bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				bitmapImage.StreamSource = memory;
				bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
				bitmapImage.EndInit();
				bitmapImage.Freeze();

				return bitmapImage;
			}
		}
	}
}
