using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AvorionDataConverter.Entities;

namespace AvorionDataConverter.Services.XmlDataProcessor {
    public static class XmlDataReader {
        public static IEnumerable<Sector> ReadAllSectorsData(string path, StationType[] stationTypes) {
            var sectorFiles = new DirectoryInfo(path).GetFiles();
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(View));
            foreach (var sectorFile in sectorFiles) {
                using (var reader = new StreamReader(sectorFile.FullName)) {
                    var content = (View) serializer.Deserialize(reader);

                    var sector = new Sector(int.Parse(content.Coordinates.X), int.Parse(content.Coordinates.Y));
                    var fastTravels = content.Gates.Coordinates
                                             .Select(coordinates => new Sector(int.Parse(coordinates.X), int.Parse(coordinates.Y)))
                                             .Concat(content.Wormholes.Coordinates.Select(coordinates =>
                                                                                              new Sector(int.Parse(coordinates.X), int.Parse(coordinates.Y))));
                    var generalStation = stationTypes.Single(x => x.CommonName == "Unspecified");

                    var stations = content.Titles
                                          .Title
                                          .Select(x => new Station(x.GetformattedName, stationTypes.FirstOrDefault(y => x.GetformattedName.StartsWith(y.CommonName)) ?? generalStation))
                                          .ToArray();

                    sector.FastTravelSectors.AddRange(fastTravels);
                    sector.Stations.AddRange(stations);

                    yield return sector;
                }
            }
        }
    }
}