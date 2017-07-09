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
            ICharacters mockCharacter = new MockCharacter();
			IMapNavigator mapNavigator = new MapNavigator();
			IList<ICharacters> mockParty = new List<ICharacters>();
			mockParty.Add(mockCharacter);
            IMapMovement moveDirection = new GoNorth(mapNavigator, mockParty);
			
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
	}
}
