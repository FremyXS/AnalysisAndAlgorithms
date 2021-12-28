using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GraphTraversal
{
    public class Algorithm
    {
        public void SearchShortWay(string name)
        {
            SearchShortWay(Host.Hostes.Single(el => el.Name == name));
        }
        public async Task SearchShortWay(Host host, string fl = "0")
        {
            if (!host.IsVisited)
            {
                if(GraphModel.GraphModels.Single(el => el.Name.Text == host.Name).MinWay.Text == "")
                    GraphModel.GraphModels.Single(el => el.Name.Text == host.Name).ChangeMinWay(fl);

                var list = new List<Host>();

                foreach(var el in host.Connections)
                {
                    if (GraphModel.GraphModels.Single(t => t.Name.Text == el.EndHost.Name).MinWay.Text == "" 
                    ||int.Parse(el.Flow) + int.Parse(GraphModel.GraphModels.Single(t2 => t2.Name.Text == host.Name).MinWay.Text) < int.Parse(GraphModel.GraphModels.Single(t => t.Name.Text == el.EndHost.Name).MinWay.Text))
                    {
                        GraphModel.GraphModels.Single(t => t.Name.Text == el.EndHost.Name)
                            .ChangeMinWay((int.Parse(el.Flow) + int.Parse(GraphModel.GraphModels.Single(t2 => t2.Name.Text == host.Name).MinWay.Text)).ToString());

                        MainWindow.DG.AddLog($"{host.Name} > {el.EndHost.Name} = {(int.Parse(el.Flow) + int.Parse(GraphModel.GraphModels.Single(t2 => t2.Name.Text == host.Name).MinWay.Text)).ToString()}");
                        await MainWindow.DG.Drawing();
                    }

                    list.Add(el.EndHost);
                }

                var t = Host.Hostes.Where(el2 => el2.Connections.Any(el3 => el3.EndHost.Name == host.Name));

                foreach(var el in Host.Hostes)
                {
                    if(el.Connections.Any(t => t.EndHost.Name == host.Name))
                    {
                        var el2 = el.Connections.Single(t => t.EndHost.Name == host.Name);
                        var dwel = GraphModel.GraphModels.Single(t => t.Name.Text == el.Name);


                        if (dwel.MinWay.Text == ""
                        || int.Parse(el2.Flow) + int.Parse(GraphModel.GraphModels.Single(t2 => t2.Name.Text == host.Name).MinWay.Text) < int.Parse(dwel.MinWay.Text))
                        {
                            GraphModel.GraphModels.Single(t => t.Name.Text == el.Name)
                                .ChangeMinWay((int.Parse(el2.Flow) + int.Parse(GraphModel.GraphModels.Single(t2 => t2.Name.Text == host.Name).MinWay.Text)).ToString());

                            MainWindow.DG.AddLog($"{host.Name} > {el.Name} = {(int.Parse(el2.Flow) + int.Parse(GraphModel.GraphModels.Single(t2 => t2.Name.Text == host.Name).MinWay.Text)).ToString()}");

                            await MainWindow.DG.Drawing();
                        }

                        list.Add(el);
                    }
                }

                host.Visit();

                await MainWindow.DG.Drawing();

                foreach(var el in list)
                {
                    await SearchShortWay(el);
                }


            }
        }
        private int sum;
        public async Task MaximumFlow(string start, string end)
        {
            sum = 0;
            var list = new List<ConnectionModel>();

            await MaximumFlow (Host.Hostes.Single(el => el.Name == start), Host.Hostes.Single(el => el.Name == end), list);

            MainWindow.DG.AddLog("Max Flow = " + sum.ToString());
            MainWindow.DG.Drawing();

        }
        public async Task MaximumFlow(Host start, Host end, List<ConnectionModel> list, string text = "")
        {
            await MainWindow.DG.Drawing();

            text += start.Name + "-";

            if (start == end)
            {
                start.Visit();
                Calc(list, text);
                list.Last().Visited();
                await MainWindow.DG.Drawing();
                start.NotVisit();
                list.Last().NotVisited();
                list.Remove(list.Last());
            }
            else
            {
                if (!start.IsVisited)
                {
                    start.Visit();

                    if(list.Count > 0)
                     list.Last().Visited();

                    foreach (var el in start.Connections)
                    {
                        if (list.Any(t => t.Flow.Text == "0"))
                        {
                            MainWindow.DG.AddLog($"Connection {text} contains 0");
                            MainWindow.DG.AddLog("Step Back");
                            break;
                        }


                        MainWindow.DG.AddLog($"{el.EndHost.Name}'s flow contains 0?");
                        if (ConnectionModel.Connections.Any(t => t.StartHost.Name.Text == start.Name && t.EndHost.Name.Text == el.EndHost.Name)
                        && ConnectionModel.Connections.Single(t => t.StartHost.Name.Text == start.Name && t.EndHost.Name.Text == el.EndHost.Name).Flow.Text is not "0")
                        {
                            MainWindow.DG.AddLog("No");
                            list.Add(ConnectionModel.Connections.Single(t => t.StartHost.Name.Text == start.Name && t.EndHost.Name.Text == el.EndHost.Name));
                                                     
                            await MaximumFlow(el.EndHost, end, list, text);
                        }
                        else
                        {
                            MainWindow.DG.AddLog("Yes");
                            MainWindow.DG.AddLog("Step on next Connect");
                        }
                    }

                    start.NotVisit();

                    if (list.Count > 0)
                    {
                        list.Last().NotVisited();
                        list.Remove(list.Last());
                    }
                }                
            }

           
        }
        public void Calc(List<ConnectionModel> list, string text)
        {
            var t = list.Select(el => int.Parse(el.Flow.Text)).Min();

            foreach (var el in list)
                el.ChangeFlow(t);

            sum += t;
            MainWindow.DG.AddLog(text);
            MainWindow.DG.AddLog("Min Flow: " + t.ToString());
        }
        public async Task DFS(string name)
        {
            DFS(Host.Hostes.Single(el => el.Name == name));
        }
        public async Task DFS(Host host)
        {
            if (host.IsVisited == false)
            {
                host.Visit();

                await MainWindow.DG.Drawing();

                foreach (var host2 in host.Connections)
                {
                    await DFCAlg(host, host2.EndHost);
                }

                foreach(var host2 in Host.Hostes.Where(el => el.Connections.Any(el2 => el2.EndHost.Name == host.Name)))
                {
                    await DFCAlg(host, host2);
                }
            }
        }
        private async Task DFCAlg(Host host, Host host2)
        {
            MainWindow.DG.AddLog($"{host2.Name} is visited?");
            if (host2.IsVisited == false)
            {
                MainWindow.DG.AddLog("No");
                MainWindow.DG.AddLog($"{host.Name} -> {host2.Name}");
                ConnectionModel.SearchConnect(host.Name, host2.Name);
                await DFS(host2);
            }
            else
            {
                MainWindow.DG.AddLog("Yes");
            }
        }
        public void BFS(string host)
        {
            BFS(Host.Hostes.Single(el => el.Name == host));
        }
        public async Task BFS(Host host)
        {
            if (host.IsVisited == false)
            {
                host.Visit();
                await MainWindow.DG.Drawing();

                await BFS(new Dictionary<string, List<Connection>>() { { host.Name, host.Connections } });
            }
        }
        private async Task BFS(Dictionary<string , List<Connection>> hosts)
        {
            if (hosts.Count == 0)
                return;

            var dic = new Dictionary<string, List<Connection>>();

            foreach(var el in hosts)
            {
                foreach(var el2 in el.Value)
                {
                    MainWindow.DG.AddLog($"{el2.EndHost.Name} is visited?");

                    if (el2.EndHost.IsVisited == false)
                    {
                        ConnectionModel.SearchConnect(el.Key, el2.EndHost.Name);
                        MainWindow.DG.AddLog("No");
                        var list = new List<Connection>();
                        el2.EndHost.Visit();
                        list.AddRange(el2.EndHost.Connections);
                        var temp1 = Host.Hostes.Where(t1 => t1.Connections.Any(t2 => t2.EndHost.Name == el.Key)).ToList();
                        var temp2 = temp1.Select(t1 => t1.Connections).ToList();

                        if(temp2.Count > 0)
                            list.AddRange(temp2.Last());

                        dic.Add(el2.EndHost.Name, list);

                        MainWindow.DG.AddLog($"{el.Key} > {el2.EndHost.Name}");
                    }
                    else
                    {
                        MainWindow.DG.AddLog("Yes");
                    }
                }
            }

            await MainWindow.DG.Drawing();
            await BFS(dic);

        }
        public void Clear()
        {
            foreach (var el in Host.Hostes)
            {
                el.NotVisit();                
            }
            GraphModel.NulWay();
            ConnectionModel.GetConnections();
            MainWindow.DG.Drawing();
            MainWindow.DG.LogClear();
        }
    }
}

