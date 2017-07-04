using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ADND.test
{
    [TestFixture()]
    public class GoNorthTest
    {
		[Test()]
		public void ShouldBeCurrentTileNorthAndDifferntToInitialTile()
		{
			ICharacters character = new MockCharacter();
			IMapNavigator mapNavigator = new MapNavigator(character);
            IMapMovement moveDirection = new GoNorth(mapNavigator, character);
			
			IList<IMapTile> mapTileList = mapNavigator.GetMapTileCollection();
			Assert.AreEqual(true, mapTileList[0].IsCurrent);
            moveDirection.Move();

			Assert.AreEqual(2, mapTileList.Count);
			Assert.AreNotEqual(true, mapTileList[0].IsCurrent);
			Assert.AreEqual(true, mapTileList[1].IsCurrent);
			Assert.AreEqual(mapTileList[0].positionY + 1, mapTileList[1].positionY);
			Assert.AreEqual(mapTileList[0].positionX, mapTileList[1].positionX);
			Assert.AreEqual(mapTileList[0].positionZ, mapTileList[1].positionZ);
		}

		public class MockMapTile : IMapTile
		{
			public int positionX { get; set; }
			public int positionY { get; set; }
			public int positionZ { get; set; }
			public string description { get; set; }
			public bool IsCurrent { get; set; }
			public bool IsLast { get; set; }
			public enum State { visited, current }
			public IList<IEncounterType> encounterType { get; set; }
			Random r = new Random();
			public ICharacters player;
            public bool HadEncounter { get; set; }

			public MockMapTile(ICharacters character)
			{
				positionX = 0;
				positionY = 0;
				positionZ = 0;
				IsCurrent = false;
				player = character;

				//TODO: initialise encounterType List
			}
			public void GenerateEncounterType()
			{
				IEncounterType encounter = new MapTileEncounter(player);
			}
			public void ExecuteEncounterType()
			{
				IEncounterType encounter = new MapTileEncounter(player);
			}
		}
	}
}
