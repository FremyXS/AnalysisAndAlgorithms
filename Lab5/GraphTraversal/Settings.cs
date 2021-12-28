using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GraphTraversal
{
    public class Settings
    {
        public static void SaveGraph(string path)
        {
            string hostes = JsonConvert.SerializeObject(Host.Hostes, Formatting.Indented);
            File.WriteAllText(path + ".json", hostes);
        }
        public static void LoadGraph(string path)
        {
            var tes = JsonConvert.DeserializeObject<List<Host>>(File.ReadAllText(path));
            Host.SetMatrix(tes);
            GraphModel.GetGrapgModels();

            foreach(var el in GraphModel.GraphModels)
            {
                var temp = Host.Hostes.Single(t => t.Name == el.Name.Text);
                el.SetPosition(temp.X, temp.Y);
            }

            ConnectionModel.GetConnections();
        }
    }
}
