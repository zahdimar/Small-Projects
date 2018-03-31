using Sandbox.ModAPI.Ingame;
using VRageMath;

namespace SpaceEngineers.RemoteTransmission.LocatorUnit {
    public sealed class Program : MyGridProgram {
        private readonly IMyRemoteControl remoteControl;

        private const int NotDisturbDistance = 15;

        private static System.Text.RegularExpressions.Match Parse(string inputLine, string pattern){
            return System.Text.RegularExpressions.Regex.Match(inputLine, pattern);
        }
            
        public Program() {
            remoteControl = GridTerminalSystem.GetBlockWithName("RC") as IMyRemoteControl;
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
                    var currentPos = remoteControl.GetPosition();
                        
                    if ((currentPos - coords).Length() < NotDisturbDistance) {
                        remoteControl.SetAutoPilotEnabled(false);
                    }
                    remoteControl.ClearWaypoints();
                    remoteControl.AddWaypoint(coords, "Destination");
                    Echo($"Move to {coords.ToString()}");
                    remoteControl.SetAutoPilotEnabled(true);
                }
            }
        }
    }
}