namespace AvorionDataConverter.Entities {
    public class Station : AbstractEntity {
        public Station(string name, StationType type) {
            Name = name;
            Type = type;
        }

        public string Name { get; set; }

        public StationType Type { get; set; }
    }
}