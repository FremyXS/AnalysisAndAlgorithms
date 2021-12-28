using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace GraphTraversal
{
    public class DrawGraph
    {
        public Canvas Content { get; private set; }
        public StackPanel Logs { get; private set; }
        public DrawGraph(Canvas content, StackPanel logs)
        {
            Content = content;
            Logs = logs;
        }
        public void StartPosition()
        {
            Content.Children.Clear();            

            GraphModel.GetGrapgModels();

            for(int i = 0; i < GraphModel.GraphModels.Count; i++)
            {

                if(i % 2 == 0)
                {
                    GraphModel.GraphModels[i].SetPosition(i * 100, 50);
                }
                else
                {
                    GraphModel.GraphModels[i].SetPosition((i - 1) * 100 , 200);
                }
            }

            ConnectionModel.GetConnections();
        }
        public async Task Drawing()
        {
            Content.Children.Clear();

            foreach (var el in ConnectionModel.Connections)
            {
                Content.Children.Add(el.Connect);
                Content.Children.Add(el.Arrow);
                Content.Children.Add(el.Flow);
            }

            foreach (var el in GraphModel.GraphModels)
            {
                if (Host.Hostes.Single(t => t.Name == el.Name.Text).IsVisited)
                    el.Visit();
                else
                    el.NotVisit();

                Content.Children.Add(el.Ellipse);
                Content.Children.Add(el.Name);
                Content.Children.Add(el.MinWay);
            }

            await Task.Delay(1000);
        }
        public void AddLog(string text)
        {
            Logs.Children.Add(new TextBlock { Text = text, Foreground = Brushes.LightGreen });
        }
        public void LogClear()
        {
            Logs.Children.Clear();
        }
    }
}
