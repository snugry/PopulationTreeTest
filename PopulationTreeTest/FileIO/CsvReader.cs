using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection.PortableExecutable;

namespace PopulationTreeTest.FileIO
{
    public class CsvReader
    {
        private string _filename;

        public CsvReader(string filename)
        {
            _filename = filename;
        }

        public string[] ReadCsvValues(int column, bool skipHeader = true)
        {
            List<string> valueList = new List<string>();
            using (StreamReader stream = new StreamReader(_filename))
            {
                bool gotHeader = true;
                while (!stream.EndOfStream)
                {
                    var line = stream.ReadLine();
                    if (line != null)
                    {
                        var values = line.Split(';');

                        if (!skipHeader || gotHeader)
                            valueList.Add(values[column]);
                        else
                            gotHeader = true;
                    }
                }
            }

            return valueList.ToArray();
        }
    }
}
