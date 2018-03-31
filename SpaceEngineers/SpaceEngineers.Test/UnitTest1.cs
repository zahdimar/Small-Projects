using System;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using Sandbox.Game.Entities;
using Sandbox.Game.GameSystems;
using Sandbox.ModAPI.Ingame;
using SpaceEngineers.AutoDoorCloser;
using IMyTerminalBlock = Sandbox.ModAPI.IMyTerminalBlock;

namespace SpaceEngineers.Test {
    [TestFixture]
    public class UnitTest1 {
        private Program.DoorManager doorManager;
        private IMyGridTerminalSystem gridTerminalSystem;

        [Test]
        public void TestAutoDoorCloser() {
            gridTerminalSystem = MockRepository.GenerateStub<IMyGridTerminalSystem>();
            doorManager = new Program.DoorManager(gridTerminalSystem);
            var terminalDoors = new List<IMyTerminalBlock>();
            var door = new MyDoor();
            gridTerminalSystem.Stub(x=>x.GetBlocksOfType<IMyTerminalBlock>( new List<IMyTerminalBlock>(), Program.DoorManager.IsAutoclosedDoor)).Do(
                new Action<object>(x => terminalDoors.Add(door)));

            Assert.AreEqual(1, doorManager.GetStoredDoorsCount);

        }
    }
}