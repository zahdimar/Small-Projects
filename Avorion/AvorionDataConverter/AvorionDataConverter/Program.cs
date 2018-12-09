using System;
using System.IO;
using System.Linq;
using AvorionDataConverter.Entities;
using AvorionDataConverter.Services;
using AvorionDataConverter.Services.XmlDataProcessor;
using AvorionDataConverter.ViewModels;

namespace AvorionDataConverter {
    class Program {
        private const string PathToSectorFiles = "C:/Users/zahdi/Dropbox/SharedData/Avorion";
        private const string PathToGoodsFile = "C:/Users/zahdi/Documents/GitHub/Small-Projects/Avorion/goods.json";

        private const int MyX = -149;
        private const int MyY = 127;

        private const int Radius = 50;

        private static (Product[], Sector[]) ReadAllData() {
            var (products, stations) = GoodsReaderHelper.ReadProductsInfo(PathToGoodsFile);
            var sectors = XmlDataReader.ReadAllSectorsData(PathToSectorFiles, stations).ToArray();
            return (products, sectors);
        }

        static void Main(string[] args) {
            var (products, sectors) = ReadAllData();

            var mySector = sectors.Single(x => x.X == MyX && x.Y == MyY);
            var myProduction = SectorCalculationHelper.GetProductsBySector(mySector, products, (x, y) => x.SoldBy.Contains(y)).Select(x => x.Item1).ToArray();

            var usefulSectorInfos = SectorCalculationHelper.GetSectorsWithProductsInRadius(sectors, mySector, myProduction, Radius);

            var rows = usefulSectorInfos.OrderBy(x => x.Distance)
                                                          .SelectMany(sectorInfo => sectorInfo.ProductInfos
                                                                                              .OrderBy(x => x.station.Name)
                                                                                              .Select(productInfo => FormatInfoToTsv(sectorInfo, productInfo))).ToArray();
            SaveDataToFile("output.tsv", rows);
        }

        private static void SaveDataToFile(string path, string[] rows) {
            File.WriteAllText(path, $"Coordinates\tDistance\tStation\tProduct\tPrice\tVolume\n{string.Join("\n", rows)}");
        }

        private static string FormatInfoToTsv(SectorWithProducts sectorInfo, (Product product, Station station) productInfo) {
            return $"[{sectorInfo.Sector.X}:{sectorInfo.Sector.Y}]\t{Math.Round(sectorInfo.Distance, 1)}\t{productInfo.station.Name}\t{productInfo.product.Name}\t{productInfo.product.AvgPrice}\t{productInfo.product.Volume}";
        }
    }
}