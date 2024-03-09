using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace PopulationTreeTest.FileIO
{
    internal class JsonReader
    {
        public List<EarthAge> ReadJson(string path)
        {
            string text = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<EarthAge>>(text);
        }
    }
}
