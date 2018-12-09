using System.ComponentModel.DataAnnotations.Schema;

namespace AvorionDataConverter.Entities {
    [Table("Products")]
    public class Product : AbstractEntity {
        public Product() { }

        public Product(string name, float volume, int avgPrice, bool illegal, bool dangerous, StationType[] soldBy, StationType[] boughtBy) {
            Name = name;
            Volume = volume;
            AvgPrice = avgPrice;
            Illegal = illegal;
            Dangerous = dangerous;
            SoldBy = soldBy;
            BoughtBy = boughtBy;
        }

        public string Name { get; set; }

        public float Volume { get; set; }

        public int AvgPrice { get; set; }

        public bool Illegal { get; set; }

        public bool Dangerous { get; set; }

        public StationType[] SoldBy { get; set; }

        public StationType[] BoughtBy { get; set; }
    }
}