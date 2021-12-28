using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphTraversal
{
    public class GraphModel
    {
        public TextBlock Name { get; }
        public Ellipse Ellipse { get; private set; }
        public double X { get; private set; }
        public double Y { get; private set; }
        public TextBlock MinWay { get; private set; } = new TextBlock();
        public GraphModel(string name)
        {
            Name = new TextBlock { Text = name };
            Ellipse = new Ellipse { Height = 30, Width = 30, Fill = Brushes.Red};
            MinWay.Foreground = Brushes.Red;
            Ellipse.MouseRightButtonUp += MenuClick;
            Ellipse.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            Ellipse.MouseMove += Ellipse_MouseMove;
            Ellipse.MouseLeftButtonUp += Ellipse_MouseLeftButtonUp;
            Name.MouseRightButtonUp += MenuClick;
            Name.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            Name.MouseMove += Ellipse_MouseMove;
            Name.MouseLeftButtonUp += Ellipse_MouseLeftButtonUp;
        }
        public void ChangeMinWay(string val)
            => MinWay.Text = val;
        public void SetPosition(double x, double y)
        {
            this.AddCoor(x, y);
            Canvas.SetTop(this.Ellipse, Y);
            Canvas.SetLeft(this.Ellipse, X);
            Canvas.SetTop(this.Name, Y + 5);
            Canvas.SetLeft(this.Name, X + 10);
            Canvas.SetTop(this.MinWay, Y + 30);
            Canvas.SetLeft(this.MinWay, X + 10);
            Host.Hostes.Single(el => el.Name == Name.Text).ChangePosition(x, y);
        }
        private void Ellipse_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _movePoint = null;
            this.Ellipse.ReleaseMouseCapture();
        }

        private void Ellipse_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (_movePoint == null)
                return;

            var p = e.GetPosition(null) - (Vector)_movePoint.Value;
            this.SetPosition(p.X - 200, p.Y);

            foreach(var el in ConnectionModel.Connections.Where(t => t.StartHost == this || t.EndHost == this))
            {
                if(el.StartHost == this)
                {
                    el.ChangeStart(p.X - 200, p.Y);
                }
                else
                {
                    el.ChangeEnd(p.X - 200, p.Y);
                }
            }
        }

        private Point? _movePoint;
        private void Ellipse_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _movePoint = e.GetPosition(this.Ellipse);
            this.Ellipse.CaptureMouse();
        }

        public static List<GraphModel> GraphModels { get; private set; } = new List<GraphModel>();
        public static void NulWay()
        {
            foreach (var el in GraphModels)
                el.MinWay.Text = "";
        }
        public static void GetGrapgModels()
        {
            GraphModels.Clear();

            foreach (var el in Host.Hostes)
                GraphModels.Add(new GraphModel(el.Name));
        }
        private void MenuClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ContextMenu cm = new ContextMenu();

            MenuItem del = new MenuItem() { Header = "Delete", };
            del.Click += Del_Click;
            cm.Items.Add(del);
            MenuItem reName = new MenuItem() { Header = "ReName", };
            reName.Click += ReName_Click;
            cm.Items.Add(reName);


            cm.PlacementTarget = sender as Button;
            cm.IsOpen = true;
        }

        private void ReName_Click(object sender, RoutedEventArgs e)
        {
            br = new Border
            {
                Name = "set",
                Background = Brushes.SlateGray,
                Margin = new Thickness(5, 5, 5, 5),
                Padding = new Thickness(5, 5, 5, 5)
            };
            var st = new StackPanel();
            text = new TextBox
            {
                Width = 100,
                Name = "nameP"
            };
            var bt = new Button() { Content = "OK", Width = 100 };
            bt.Click += Bt_Click;
            st.Children.Add(text);
            st.Children.Add(bt);
            br.Child = st;

            MainWindow.DG.Content.Children.Add(br);
        }
        private Border br;
        private TextBox text;
        private void Bt_Click(object sender, RoutedEventArgs e)
        {
            Host.ChangeName(Name.Text, text.Text);
            Name.Text = text.Text;            
            MainWindow.DG.Content.Children.Remove(br);
        }
        private void Del_Click(object sender, RoutedEventArgs e)
        {
            GraphModels.Remove(GraphModels.Single(el => el.Name == this.Name));
            Host.DelHost(this.Name.Text);
            ConnectionModel.GetConnections();
            MainWindow.DG.Drawing();
        }

        public void AddCoor(double x, double y)
        {
            X = x;
            Y = y;
        }
        public void Visit()
            => Ellipse.Fill = Brushes.Blue;
        public void NotVisit()
            => Ellipse.Fill = Brushes.Red;
        public static void AddGraphModel(string name)
        {
            GraphModels.Add(new GraphModel(name));
        }
    }
    public class ConnectionModel
    {
        public bool IsOrientir { get; private set; } = false;
        public GraphModel EndHost { get; private set; }
        public GraphModel StartHost { get; private set; }
        public Line Connect { get; private set; }
        public Line Arrow { get; private set; }
        public TextBlock Flow { get; private set; } = new TextBlock();
        public ConnectionModel(double x1, double y1, double x2, double y2, GraphModel endHost, GraphModel startHost, string flow, bool isOr)
        {
            IsOrientir = isOr;
            Flow.Text = flow;

            ChangePositionFlow(x1, y1, x2, y2);

            Connect = new Line
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Stroke = Brushes.Black,
            };

            Connect.MouseRightButtonUp += MenuClick;
            Flow.MouseRightButtonUp += MenuClick;

            EndHost = endHost;
            StartHost = startHost;

            if (IsOrientir)
            {
                Arrow = new Line
                {
                    X1 = x1 == x2 ? x2 : (x1 > x2 ? x2 + (x1 - x2) / 8 : x2 - (x2 - x1) / 8),
                    Y1 = y1 == y2 ? y2 : (y1 > y2 ? y2 + (y1 - y2) / 8 : y2 - (y2 - y1) / 8),
                    X2 = x2,
                    Y2 = y2,
                    Stroke = Brushes.Purple,
                    StrokeThickness = 5,
                };
            }
            else
                Arrow = new Line();


        }
        public static void SearchConnect(string start, string end)
            => Connections.Single(el => el.StartHost.Name.Text == start && el.EndHost.Name.Text == end).Visited();
        public void Visited()
            => Connect.Stroke = Brushes.Blue;
        public void NotVisited()
            => Connect.Stroke = Brushes.Black;
        private void ChangePositionFlow(double x1, double y1, double x2, double y2)
        {
            Canvas.SetLeft(Flow, x1 == x2 ? x1 : (x1 > x2 ? x1 - ((x1 - x2) / 2) : x2 - ((x2 - x1) / 2)));
            Canvas.SetTop(Flow, y1 == y2 ? y1 : (y1 > y2 ? y1 - ((y1 - y2) / 2) : y2 - ((y2 - y1) / 2)));
        }
        private void ChangePositionArrow(double x1, double y1, double x2, double y2)
        {
            Arrow.X1 = x1 == x2 ? x2 : (x1 > x2 ? x2 + (x1 - x2) / 8 : x2 - (x2 - x1) / 8);
            Arrow.Y1 = y1 == y2 ? y2 : (y1 > y2 ? y2 + (y1 - y2) / 8 : y2 - (y2 - y1) / 8);
            Arrow.X2 = x2;
            Arrow.Y2 = y2;
        }
        private void MenuClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ContextMenu cm = new ContextMenu();
            MenuItem mt = new MenuItem() { Header = "Delete", };
            mt.Click += Delete_Click;
            MenuItem rt = new MenuItem() { Header = "ReFlow", };
            rt.Click += ReFlow_Click;

            cm.Items.Add(mt);
            cm.Items.Add(rt);

            cm.PlacementTarget = sender as Button;
            cm.IsOpen = true;
        }

        private void ReFlow_Click(object sender, RoutedEventArgs e)
        {
            br = new Border
            {
                Name = "set",
                Background = Brushes.SlateGray,
                Margin = new Thickness(5, 5, 5, 5),
                Padding = new Thickness(5, 5, 5, 5)
            };
            var st = new StackPanel();
            text = new TextBox
            {
                Width = 100,
                Name = "FlowP"
            };
            var bt = new Button() { Content = "OK", Width = 100 };
            bt.Click += Bt_Click;
            st.Children.Add(text);
            st.Children.Add(bt);
            br.Child = st;
            
            MainWindow.DG.Content.Children.Add(br);
        }
        private Border br;
        private TextBox text;
        private void Bt_Click(object sender, RoutedEventArgs e)
        {
            Flow.Text = text.Text;
            Host.ChangeFlowOfConncet(StartHost.Name.Text, EndHost.Name.Text, Flow.Text);
            MainWindow.DG.Content.Children.Remove(br);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Host.DelConnection(StartHost.Name.Text, EndHost.Name.Text);
            Connections.Remove(this);
            MainWindow.DG.Drawing();
        }

        public static List<ConnectionModel> Connections { get; private set; } = new List<ConnectionModel>();
        public static void GetConnections()
        {
            Connections.Clear();

            foreach (var el in Host.Hostes)
            {
                foreach (var el2 in el.Connections)
                {
                    var temp1 = GraphModel.GraphModels.Single(t => t.Name.Text == el.Name);
                    var temp2 = GraphModel.GraphModels.Single(t => t.Name.Text == ((Connection)el2).EndHost.Name);
                    var connect = new ConnectionModel(temp1.X + 15, temp1.Y + 10, temp2.X + 15, temp2.Y + 10, temp2, temp1, ((Connection)el2).Flow, el2.IsOriented);
                    Connections.Add(connect);
                }
            }
        }
        public void ChangeStart(double x, double y)
        {
            Connect.X1 = x + 15;
            Connect.Y1 = y + 10;

            ChangePositionFlow(Connect.X1, Connect.Y1, Connect.X2, Connect.Y2);
            ChangePositionArrow(Connect.X1, Connect.Y1, Connect.X2, Connect.Y2);
        }
        public void ChangeEnd(double x, double y) //X1 = x1 == x2 ? x2 : (x1 > x2 ? x2 + 25 : x2 - 25),
        {
            Connect.X2 = x + 15;
            Connect.Y2 = y + 10;

            ChangePositionFlow(Connect.X1, Connect.Y1, Connect.X2, Connect.Y2);
            ChangePositionArrow(Connect.X1, Connect.Y1, Connect.X2, Connect.Y2);
        }
        public void ChangeFlow(int value)
        {
            Flow.Text = (int.Parse(Flow.Text) - value).ToString();
        }
        public static void AddConnect(string start, string end, string flow, bool isOr)
        {
            var temp1 = GraphModel.GraphModels.Single(t => t.Name.Text == start);
            var temp2 = GraphModel.GraphModels.Single(t => t.Name.Text == end);
            var connect = new ConnectionModel(temp1.X + 15, temp1.Y + 10, temp2.X + 15, temp2.Y + 10, temp2, temp1, flow, isOr);
            Connections.Add(connect);
        }
        public override string ToString()
        {
            return StartHost.Name.Text + "->" + EndHost.Name.Text;
        }
    }
}
