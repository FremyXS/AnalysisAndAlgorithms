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
		public double size = 2.0;
		public double rmin = 0.01;
		public double minx = -1.0, miny = -1.0;

		public int W = 600;
		public int N = 3;

		public Bitmap bmp;
		public int kmax = 50;
		public int[,] M;

		public System.Drawing.Color color = System.Drawing.Color.Purple;
		public MainWindow()
		{
			InitializeComponent();

			bmp = new Bitmap(W, W);
			M = new int[W, W];
			Calc();
			Draw();
		}
		private void Calc()
		{
			Complex c = new Complex();
			Complex cn = new Complex();
			Complex ct;
			int k;

			for (int i = 0; i < W; i++)
			{
				for (int j = 0; j < W; j++)
				{
					c.Re = minx + (i * size) / (double)W;
					c.Im = miny + (j * size) / (double)W;

					for (k = 0; k < kmax; k++)
					{
						cn = c.Pow(N) + (1.0);
						cn = cn / ((c.Pow(N - 1)) * (double)N);
						cn = c - cn;
						ct = cn.Pow(N);


						if (Math.Abs(ct.Re * ct.Re + ct.Im * ct.Im - 1) < rmin * rmin) //условие остановки
							break;

						c = new Complex(cn);
					}

					M[i, j] = k;
				}

			}
		}
		private void Draw()
		{
			Graphics g = Graphics.FromImage(bmp);
			g.Clear(System.Drawing.Color.Pink);
			int col;

			for (int i = 0; i < W; i++)
			{
				for (int j = 0; j < W; j++)
				{
					col = M[i, j] * 255 / kmax;
					System.Drawing.Pen p = new System.Drawing.Pen(System.Drawing.Color.FromArgb(col, color));
					g.DrawRectangle(p, i, j, 1, 1);
				}
			}
		}
		private void content_Loaded(object sender, RoutedEventArgs e)
		{
			img.Source = BmpImageFromBmp(bmp);
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