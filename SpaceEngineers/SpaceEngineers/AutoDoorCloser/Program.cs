using System.Collections.Generic;
using Sandbox.ModAPI.Ingame;
using IMyDoor = Sandbox.ModAPI.IMyDoor;
using IMyTerminalBlock = Sandbox.ModAPI.IMyTerminalBlock;

namespace SpaceEngineers.AutoDoorCloser {
    public sealed class Program: MyGridProgram {
        /*
        [Close] - door will be closed in 3 cycles
        [Close time = 5] - door wil closed in 5 cycles */

        private const int DefaultTimeToClose = 3; //How many cycles door will be opened
        private const string MarkerWord = "Close"; //Keyword to work with autoclosed doors

        private DoorManager doorManager;
        
        public class Door {
            private readonly IMyDoor door;
            private int howLongDoorIsOpen;
            private readonly int timeToClose;

            public Door(IMyDoor door, int timeToClose = 3) {
                this.door = door;
                this.timeToClose = timeToClose;
                howLongDoorIsOpen = 0;
            }

            public void Close() => door.GetActionWithName("Open_Off").Apply(door);

            public void Open() => door.GetActionWithName("Open_On").Apply(door);

            public bool IsEqualsTo(IMyDoor anotherDoor) => door.GetId() == anotherDoor.GetId();

            public bool IsOpen => door.Status == DoorStatus.Open;

            public long GetDoorId => door.GetId();

            public void Update() {
                if (!IsOpen) {
                    return;
                }
                if (howLongDoorIsOpen < timeToClose) {
                    howLongDoorIsOpen++;
                }
                else {
                    Close();
                    howLongDoorIsOpen = 0;
                }
            }
        }
        
        public void Main(string argument) {
            if (doorManager == null || argument.Contains("Restart")) {
                doorManager = new DoorManager(GridTerminalSystem);
            }
            var openedDoorsCount = doorManager.Update();
            Echo($"Processed doors count: {openedDoorsCount}");
        }

        public interface IDoorManager {
            IEnumerable<IMyDoor> GetAllOpenedAutomatedDoors();
            int Update();
        }

        public sealed class DoorManager : IDoorManager {
            private readonly List<Door> storedDoors = new List<Door>();
            private readonly IMyGridTerminalSystem gts;
            private const string TimeToClosePattern = @"time\s*=\s*(\d+)";

            public DoorManager(IMyGridTerminalSystem gts) {
                this.gts = gts;
            }

            public int GetStoredDoorsCount => storedDoors.Count;

            public IEnumerable<IMyDoor> GetAllOpenedAutomatedDoors() {
                var terminalDoors = new List<IMyTerminalBlock>();
                gts.GetBlocksOfType(terminalDoors, IsAutoclosedDoor);
                var openedDoors = new List<IMyDoor>();
                terminalDoors.ForEach(x => openedDoors.Add(x as IMyDoor));
                return openedDoors;
            }

            private static System.Text.RegularExpressions.Match Parse(string inputLine, string pattern){
                return System.Text.RegularExpressions.Regex.Match(inputLine, pattern);
            }

            public static bool IsAutoclosedDoor(IMyTerminalBlock door) {
                var myDoor = door as IMyDoor;
                if (myDoor == null) {
                    return false;
                }
                return myDoor.Status == DoorStatus.Open && Parse(door.CustomData, $@"{MarkerWord}.*?").Success;
            }

            public int Update() {
                var terminalDoors = GetAllOpenedAutomatedDoors();
                foreach (var door in terminalDoors) {
                    var parsedTime = 0;
                    var groups = Parse(door.CustomData, TimeToClosePattern).Groups;
                    if (groups.Count > 1) {
                        int.TryParse(groups[1].Value, out parsedTime);
                    }

                    var doorIds = new List<long>();
                    storedDoors.ForEach(x => doorIds.Add(x.GetDoorId));

                    var tempDoor = new Door(door, parsedTime > 0 ? parsedTime : DefaultTimeToClose);
                    if (!doorIds.Contains(door.GetId())) {
                        storedDoors.Add(tempDoor);
                    }
                }
                for (var i = 0; i < storedDoors.Count; i++) { 
                    storedDoors[i].Update();
                    if (!storedDoors[i].IsOpen) {
                        storedDoors.RemoveAt(i);
                    }
                }
                return storedDoors.Count;
            }
        }
    }
}