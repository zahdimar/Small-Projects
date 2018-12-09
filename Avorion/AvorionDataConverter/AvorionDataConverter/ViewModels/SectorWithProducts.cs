using AvorionDataConverter.Entities;

namespace AvorionDataConverter.ViewModels {
    public class SectorWithProducts {
        public SectorWithProducts(Sector sector, (Product, Station)[] productInfos, double distance) {
            Sector = sector;
            ProductInfos = productInfos;
            Distance = distance;
        }
        public Sector Sector { get; }

        public (Product product, Station station)[] ProductInfos { get; }

        public double Distance { get; }
    }
}