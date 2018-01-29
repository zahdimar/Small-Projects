using Sandbox.ModAPI.Ingame;
using VRageMath;
using IMyRadioAntenna = Sandbox.ModAPI.IMyRadioAntenna;

namespace Scripts {
    public class MasterUnit : MyGridProgram {
        private IMyRadioAntenna _antenna;

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


    public class LocatorUnit : MyGridProgram {
        private IMyRemoteControl _remoteControl;

        private const int NotDisturbDistance = 15;
            
            
        public static System.Text.RegularExpressions.Match Parse(string inputLine, string pattern){
            return System.Text.RegularExpressions.Regex.Match(inputLine, pattern);
        }
            
        public Program() {
            _remoteControl = GridTerminalSystem.GetBlockWithName("RC") as IMyRemoteControl;
        }
            
        private bool StringToVector3(string line, out Vector3 result) {
            result = new Vector3();
            float x, y, z;
                
            const string coordinatesWildcard= @".*{X:(-?\d+\.\d+)\sY:(-?\d+\.\d+)\sZ:(-?\d+\.\d+)}.*";
            var parsedData = Parse(line, coordinatesWildcard).Groups;
            if (parsedData.Count != 4) {
                return false;
            }
            if (!float.TryParse(parsedData[1].Value, out x) || 
                !float.TryParse(parsedData[2].Value, out y) ||
                !float.TryParse(parsedData[3].Value, out z)) {
                return false;
            } 
            result = new Vector3(x, y, z);
            return true;
        }
            
        public void Main(string args) {
            if (args.StartsWith("AntennaPosition")) {
                Echo("Recived message");
                Vector3 coords;
                if (StringToVector3(args, out coords)) {
                    var currentPos = _remoteControl.GetPosition();
                        
                    if ((currentPos - coords).Length() < NotDisturbDistance) {
                        _remoteControl.SetAutoPilotEnabled(false);
                    }
                    _remoteControl.ClearWaypoints();
                    _remoteControl.AddWaypoint(coords, "Destination");
                    Echo($"Move to {coords.ToString()}");
                    _remoteControl.SetAutoPilotEnabled(true);
                }
            }
        }
    }
}