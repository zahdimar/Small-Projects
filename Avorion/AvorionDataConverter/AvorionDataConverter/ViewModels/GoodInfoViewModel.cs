namespace AvorionDataConverter.ViewModels {
    public class GoodInfoViewModel {
        public GoodInfoViewModel(string name, float volume, int avgPrice, string[] soldBy, string[] boughtBy, bool illegal, bool dangerous) {
            Name = name;
            Volume = volume;
            AvgPrice = avgPrice;
            SoldBy = soldBy;
            BoughtBy = boughtBy;
            Illegal = illegal;
            Dangerous = dangerous;
        }

        public string Name { get; }

        public float Volume { get; }

        public int AvgPrice { get; }

        public string[] SoldBy { get; }

        public string[] BoughtBy { get; }

        public bool Illegal { get; }

        public bool Dangerous { get; }
    }
}