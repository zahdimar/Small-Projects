namespace AvorionDataConverter.ViewModels
{
    public class Sector
    {
        public int X { get; }

        public int Y { get; }

        public string Id => $"{X:###}:{Y:###}";
    }
}