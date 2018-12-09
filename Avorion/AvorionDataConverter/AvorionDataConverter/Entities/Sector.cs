using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AvorionDataConverter.Entities {
    [Table("Sectors")]
    public class Sector : AbstractEntity {
        public Sector() { }

        public Sector(int x, int y) {
            X = x;
            Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public List<Sector> FastTravelSectors { get; set; } = new List<Sector>();

        public List<Station> Stations { get; set; } = new List<Station>();
    }
}