using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationTreeTest.FileIO
{
    internal class GraphWriter
    {
        public void WriteGraph(List<PersonData> persons)
        {
            List<string> lines = new List<string>();
            foreach(PersonData person in persons) { 
                if(person.Children != null)
                {
                    foreach (PersonData child in person.Children)
                    {
                        lines.Add(person.ToString() + "->" + child.ToString());
                    }
                }
                if(person.Partner != null)
                {
                    lines.Add(person.ToString() + "->" + person.Partner.ToString());
                }
            }

            ExportFile("Persons.Graph", lines);
        }

        public void WriteGraph(Community community)
        {
            List<string> lines = new List<string>();

            foreach(PersonData parent in community.Adults)
            {
                AddPersonsRecursively(parent, lines);
            }
            ExportFile("Community.Graph", lines);
        }

        private void AddPersonsRecursively(PersonData person, List<string> lines)
        {
            if (person.Partner != null)
            {
                lines.Add(person.ToString() + "->" + person.Partner.ToString());
            }
            if (person.Children != null)
            {
                foreach (PersonData child in person.Children)
                {
                    lines.Add(person.ToString() + "->" + child.ToString());
                    AddPersonsRecursively(child, lines);
                }
            }
        }

        private void ExportFile(string filename, List<string> lines)
        {
            string docPath =
             Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, filename)))
            {
                foreach (string line in lines)
                    outputFile.WriteLine(line);
            }
        }
    }
}
