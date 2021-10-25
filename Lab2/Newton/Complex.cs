using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newton
{
	class Complex
	{
		public double re = 0.0, im = 0.0;
		public Complex(double R, double I)
		{
			re = R;
			im = I;
		}
		public Complex(Complex c)
		{
			re = c.re;
			im = c.im;
		}
		public Complex() { }
		public static Complex operator *(Complex x, Complex y)
		{
			return new Complex(x.re * y.re - x.im * y.im, x.re * y.im + x.im * y.re);
		}
		public static Complex operator +(Complex x, Complex y)
		{
			return new Complex(x.re + y.re, x.im + y.im);
		}
		public static Complex operator -(Complex x, Complex y)
		{
			return new Complex(x.re - y.re, x.im - y.im);
		}
		public static Complex operator /(Complex x, Complex y)
		{
			Complex ch = new Complex(y);
			Complex t2 = new Complex(x);
			ch.im *= -1;
			t2 = x * ch;
			double div = (y * ch).re;
			return new Complex(t2.re / div, t2.im / div);
		}
		public static Complex operator +(Complex x, double y)
		{
			return new Complex(x.re + y, x.im);
		}
		public static Complex operator *(Complex x, double y)
		{
			return new Complex(x.re * y, x.im * y);
		}
		public Complex pow(int p)
		{
			Complex res = new Complex(this);
			for (int i = 1; i < p; i++)
				res = res * this;
			return res;
		}
		public double abs()
		{
			return Math.Sqrt(re * re + im * im);
		}
	}
}
