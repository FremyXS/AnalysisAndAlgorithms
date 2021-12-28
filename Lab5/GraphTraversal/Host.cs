using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTraversal
{
    public class Host
    {
        public string Name { get; set; }
        public List<Connection> Connections { get; set; } = new List<Connection>();
        public bool IsVisited { get; set; } = false; 
        public double X { get; set; }
        public double Y { get; set; }
        public Host(int ind)
        {
            Name = "x" + ind.ToString();
        }
        public static void ChangeName(string old, string val)
            => Hostes.Single(el => el.Name == old).Name = val;
        public void ChangePosition(double x, double y)
        {
            X = x;
            Y = y;
        }
        public void Visit()
            => IsVisited = true;
        public void NotVisit()
            => IsVisited = false;
        public void GetConnection(string[] array)
        {
            string not_connection = "0";

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == "") continue;
                if (array[i] != not_connection)
                {
                    Connections.Add(new Connection(Hostes.Single(el => el.Name == "x" + i.ToString()), array[i], false));
                }
            }
        }
        public static List<Host> Hostes { get; private set; }
        public static void ChangeFlowOfConncet(string left, string right, string value)
        {
            Hostes.Single(el => el.Name == left).Connections.Single(el2 => el2.EndHost.Name == right).ChangeFlow(value);
        }
        public static void DelHost(string name)
        {
            Hostes.Remove(Hostes.Single(el => el.Name == name));

            foreach(var el in Hostes)
            {
                if(el.Connections.Any(t => ((Connection)t).EndHost.Name == name))
                {
                    el.Connections.Remove(el.Connections.Single(t => ((Connection)t).EndHost.Name == name));
                }
            }
        }
        public static void DelConnection(string namestart, string nameend)
        {
            Hostes.Single(el => el.Name == namestart).Connections
                .Remove(Hostes.Single(el => el.Name == namestart)
                .Connections.Single(el2 => ((Connection)el2).EndHost.Name == nameend));
        }
        public static void SetMatrix(List<string> array)
        {
            for (int i = 0; i < array.Count; i++)
            {
                if (array[i] == "")
                {
                    array.RemoveAt(i);
                    i--;
                }
                else
                {
                    if (i == 0)
                    {
                        GetHosts(array[i].Trim(' ').Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length);
                    }

                    Hostes.Single(el => el.Name == "x" + i.ToString()).GetConnection(array[i].Split(' '));
                }
            }
        }
        public static void SetMatrix(List<Host> hosts)
        {
            Hostes = hosts;

            foreach(var el in Hostes)
            {
                for(int i = 0; i < el.Connections.Count; i++)
                {
                    if(Hostes.Any(t => t.Name == el.Connections[i].EndHost.Name))
                    {
                        el.Connections[i].EndHost = Hostes.Single(t => t.Name == el.Connections[i].EndHost.Name);
                    }
                }
            }
        }
        private static void GetHosts(int count)
        {
            Hostes = new List<Host>();

            for (int i = 0; i < count; i++)
            {
                Hostes.Add(new Host(i));
            }
        }
        public static void AddHost()
        {
            Hostes.Add(new Host(Hostes.Count));
            GraphModel.AddGraphModel(Hostes.Last().Name);
            MainWindow.DG.Drawing();
        }
        public static void AddConnect(string start, string end, string flow, bool? isOr)
        {
            Hostes.Single(el => el.Name == start).Connections
                .Add(new Connection(Hostes.Single(el2 => el2.Name == end), flow, (bool)isOr));

            ConnectionModel.AddConnect(start, end, flow, (bool)isOr);

            MainWindow.DG.Drawing();
        }
    }
    public class Connection
    {
        public string Flow { get;  set; }
        public Host EndHost { get; set; }
        public bool IsOriented { get; set; } = false;
        public Connection(Host endHost, string flow, bool isOriented)
        {
            EndHost = endHost;
            Flow = flow;
            IsOriented = isOriented;
        }
        public void ChangeFlow(string val)
            => Flow = val;
        public override string ToString()
        {
            return EndHost.Name;
        }
    }
}
