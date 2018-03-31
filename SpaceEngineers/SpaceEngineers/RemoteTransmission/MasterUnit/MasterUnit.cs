using Sandbox.ModAPI.Ingame;
using IMyRadioAntenna = Sandbox.ModAPI.IMyRadioAntenna;

namespace SpaceEngineers.RemoteTransmission.MasterUnit {
    public sealed class Program : MyGridProgram {
        private readonly IMyRadioAntenna _antenna;

        public Program() {
            _antenna = GridTerminalSystem.GetBlockWithName("Antenna") as IMyRadioAntenna;
        }
            
        public void Main(string args) {
            if (_antenna == null) {
                Echo("Antenna wasn't found");
                return;
            }

            var posString = _antenna.GetPosition().ToString();
                
            if (_antenna.TransmitMessage($"AntennaPosition: {posString}")) {
                Echo($"\n--{posString}--\nData was sent");
            }
        }
    }
}