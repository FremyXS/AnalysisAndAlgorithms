using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace SortingTables
{
    public class Table
    {
        public Table(string Column1, string Column2, string Column3, string Column4, string Column5)
        {
            this.Column1 = Column1;
            this.Column2 = Column2;
            this.Column3 = Column3;
            this.Column4 = Column4;
            this.Column5 = Column5;
        }
        public Table(string Column1, string Column2, string Column3)
        {
            this.Column1 = Column1;
            this.Column2 = Column2;
            this.Column3 = Column3;
        }
        public Table(string Column1)
        {
            this.Column1 = Column1;
        }
        public string Column1 { get; set; }
        public string Column2 { get; set; }
        public string Column3 { get; set; }
        public string Column4 { get; set; }
        public string Column5 { get; set; }
        public override string ToString()
        {
            string txt = "";
            txt += Column1;
            if (Column2 != null)
            {
                txt += ";" + Column2 + ";" + Column3;
            }

            if (Column4 != null)
            {
                txt += ";" + Column4 + ";" + Column5;
            }

            return txt;
        }
        public static Table GetTable(string[] array)
        {
            if (array.Length == 5)
                return new Table(array[0], array[1], array[2], array[3], array[4]);
            else if(array.Length == 3)
                return new Table(array[0], array[1], array[2]);
            else
                return new Table(array[0]);
        }
        public static bool operator <(Table left, Table right)
        {
            switch (MainWindow.sortname)
            {
                case "Country":
                    return Sorting(right, left);
                case "Chemicals":
                    return Sorting(right, left);
                case "Words":
                    return CompareStrings(right.Column1, left.Column1);
                default:
                    throw new InvalidOperationException();
            }
        }
        public static bool operator >(Table left, Table right)
        {
            switch (MainWindow.sortname)
            {
                case "Country":
                    return Sorting(left, right);
                case "Chemicals":
                    return Sorting(left, right);
                case "Words":
                    return CompareStrings(left.Column1, right.Column1);
                default:
                    throw new InvalidOperationException();
            }
        }
        private static bool Sorting(Table left, Table right)
        {
            foreach(var el in MainWindow.SortColumns)
            {
                if (!GetColumn(left, el).Equals(GetColumn(right, el)))
                {
                    
                    if(MainWindow.sortname == "Country")
                    {
                        if (el <= 2)
                            return CompareStrings(GetColumn(left, el), GetColumn(right, el));
                        else
                            return long.Parse(GetColumn(left, el)) > long.Parse(GetColumn(right, el));
                    }
                    else
                    {
                        if (el <= 1)
                            return CompareStrings(GetColumn(left, el), GetColumn(right, el));
                        else
                            return double.Parse(GetColumn(left, el).Replace('.', ',')) > double.Parse(GetColumn(right, el).Replace('.', ','));
                    }

                }
                else
                    continue;
            }

            return false;
        }
        private static string GetColumn(Table tbl, int i)
        {
            switch (i)
            {
                case 0:
                    return tbl.Column1;
                case 1:
                    return tbl.Column2;
                case 2:
                    return tbl.Column3;
                case 3:
                    return tbl.Column4;
                case 4:
                    return tbl.Column5;
                default:
                    throw new Exception();
            }    
        }
        
        private static bool CompareStrings(string left, string right)
        {
            for(int i = 0; i < Math.Min(left.Length, right.Length); i++)
            {
                if (left[i] > right[i])
                    return true;
                else if (left[i] < right[i])
                    return false;
            }

            if (left.Length > right.Length)
                return true;
            else
                return false;
        }
        public static StackPanel GetStack(Table table, Brush color)
        {
            var st = new StackPanel();
            st.Orientation = Orientation.Horizontal;
            st.Background = color;
            st.Margin = new System.Windows.Thickness(5, 5, 5, 5);
            st.Children.Add(new TextBlock() { Text = table.Column1 + ";" });
            if(MainWindow.sortname == "Chemicals")
            {
                st.Children.Add(new TextBlock() { Text = table.Column2 + ";" });
                st.Children.Add(new TextBlock() { Text = table.Column3 + ";" });
            }
            if (MainWindow.sortname == "Country")
            {
                st.Children.Add(new TextBlock() { Text = table.Column2 + ";" });
                st.Children.Add(new TextBlock() { Text = table.Column3 + ";" });
                st.Children.Add(new TextBlock() { Text = table.Column4 + ";" });
                st.Children.Add(new TextBlock() { Text = table.Column5 + ";" });
            }

            return st;
        }
    }
}
