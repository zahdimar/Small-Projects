using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace AvorionDataConverter
{
    class Program
    {
        private const string PathToSectorFiles = "C:/Users/zahdi/Dropbox/SharedData/Avorion";

        class ProcessedData
        {
            public ProcessedData(string coordinates, string[] gates, string[] stations)
            {
                Coordinates = coordinates;
                Gates = gates;
                Stations = stations;
            }

            public string Coordinates { get; }
            public string[] Gates { get; }
            public string[] Stations { get; }
        }

        static void Main(string[] args)
        {
            var result = ReadAllData(PathToSectorFiles);
            File.WriteAllText("result.json", Newtonsoft.Json.JsonConvert.SerializeObject(result, Formatting.Indented));
        }

        static private IEnumerable<ProcessedData> ReadAllData(string path)
        {
            var sectorFiles = new DirectoryInfo(PathToSectorFiles).GetFiles();
            var serialiser = new System.Xml.Serialization.XmlSerializer(typeof(ViewModels.View));
            foreach (var sectorFile in sectorFiles)
            {
                using (var reader = new StreamReader(sectorFile.FullName))
                {
                    var content = (ViewModels.View)serialiser.Deserialize(reader);
                    var gates = content.Gates.Coordinates.Select(x=>x.ToString()).ToArray();
                    var stations = content.Titles.Title.Select(x=>x.GetformattedName).ToArray();
                    yield return new ProcessedData(content.Coordinates.ToString(), gates, stations);
                }
            }
        }
    }
}
