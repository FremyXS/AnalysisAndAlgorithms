using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newton
{
	class Complex
	{
		public double Re = 0.0;
		public double Im = 0.0;
		public Complex(double re, double im)
		{
			Re = re;
			Im = im;
		}
		public Complex(Complex c)
		{
			Re = c.Re;
			Im = c.Im;
		}
		public Complex() { }
		public static Complex operator *(Complex x, Complex y)
		{
			return new Complex(x.Re * y.Re - x.Im * y.Im, x.Re * y.Im + x.Im * y.Re);
		}
		public static Complex operator -(Complex x, Complex y)
		{
			return new Complex(x.Re - y.Re, x.Im - y.Im);
		}
		public static Complex operator /(Complex x, Complex y)
		{
			Complex ch = new Complex(y);
			Complex t2 = new Complex(x);
			ch.Im *= -1;
			t2 = x * ch;
			double div = (y * ch).Re;
			return new Complex(t2.Re / div, t2.Im / div);
		}
		public static Complex operator +(Complex x, double y)
		{
			return new Complex(x.Re + y, x.Im);
		}
		public static Complex operator *(Complex x, double y)
		{
			return new Complex(x.Re * y, x.Im * y);
		}
		public Complex Pow(int p)
		{
			Complex res = new Complex(this);

			for (int i = 1; i < p; i++)
				res = res * this;

			return res;
		}
	}
}
