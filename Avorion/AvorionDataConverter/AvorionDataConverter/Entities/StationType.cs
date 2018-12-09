using System.ComponentModel.DataAnnotations.Schema;

namespace AvorionDataConverter.Entities {
    [Table("StationTypes")]
    public class StationType : AbstractEntity {
        public StationType(string commonName) {
            CommonName = commonName;
        }

        public string CommonName { get; set; }

        // public List<Product> Sell { get; set; } = new List<Product>();

        //public List<Product> Bought{ get; set; } = new List<Product>();
    }
}