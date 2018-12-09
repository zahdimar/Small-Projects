using System.IO;
using System.Linq;
using AvorionDataConverter.Entities;
using AvorionDataConverter.ViewModels;
using Newtonsoft.Json;

namespace AvorionDataConverter.Services {
    public static class GoodsReaderHelper {
        private static Product GoodsToProducts(GoodInfoViewModel goodInfoViewModel, StationType[] stations) {
            var soldBy = goodInfoViewModel.SoldBy.Select(goods => stations.First(station => goods.StartsWith(station.CommonName))).ToArray();
            var boughtBy = goodInfoViewModel.BoughtBy.Select(goods => stations.First(station => goods.StartsWith(station.CommonName))).ToArray();
            return new Product(goodInfoViewModel.Name, goodInfoViewModel.Volume, goodInfoViewModel.AvgPrice, goodInfoViewModel.Illegal,
                               goodInfoViewModel.Dangerous, soldBy, boughtBy);
        }

        public static (Product[] products, StationType[] stations) ReadProductsInfo(string filePath) {
            var content = File.ReadAllText(filePath);
            var goods = JsonConvert.DeserializeObject<GoodInfoViewModel[]>(content);
            var stations = goods.SelectMany(x => x.BoughtBy)
                                .Concat(goods.SelectMany(x => x.SoldBy))
                                .Distinct()
                                .Concat(new[] { "Unspecified" })
                                .Select(x => new StationType(x))
                                .ToArray();
            var products = goods.Select(x => GoodsToProducts(x, stations)).ToArray();
            return (products, stations);
        }
    }
}