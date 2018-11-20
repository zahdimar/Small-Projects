namespace AvorionDataConverter.ViewModels
{
    public class Jumper
    {
        public Jumper(Sector sectorA, Sector sectorB)
        {
            SectorA = sectorA;
            SectorB = sectorB;
        }

        private Sector SectorA{ get; }

        private Sector SectorB{ get; }
    }
}