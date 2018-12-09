using System;
using System.Linq;
using AvorionDataConverter.Entities;
using AvorionDataConverter.ViewModels;

namespace AvorionDataConverter.Services {
    public class SectorCalculationHelper {
        public static double GetDistance(Sector s1, Sector s2) => Math.Sqrt(Math.Pow(s1.X - s2.X, 2) + Math.Pow(s1.Y - s2.Y, 2));

        public static (Sector sector, double distance)[] GetSectorsWithDistance(Sector[] sectors, Sector center, int distance) =>
            sectors.Select(x => (sector: x, distance: GetDistance(center, x)))
                   .Where(x => x.distance < distance)
                   .ToArray();

        public static (Product, Station)[] GetProductsBySector(Sector sector, Product[] products, Func<Product, StationType, bool> productSelector) {
            var sectorStations = sector.Stations;
            return sectorStations.SelectMany(station => products.Where(x => productSelector(x, station.Type)).Select(x => (x, station))).ToArray();
        }

        public static SectorWithProducts[] GetSectorsWithProductsInRadius(Sector[] allSectors, Sector center, Product[] productsToSold, int maxDistance) {
            var nearestSectors = GetSectorsWithDistance(allSectors, center, maxDistance);
            var sectorsWithProducts =
                nearestSectors.Select(sector => new SectorWithProducts(
                                          sector.sector, GetProductsBySector(sector.sector, productsToSold, (x, y) => x.BoughtBy.Contains(y)),
                                          distance: sector.distance));
            return sectorsWithProducts
                   .Where(x => x.ProductInfos.Any())
                   .ToArray();
        }
    }
}