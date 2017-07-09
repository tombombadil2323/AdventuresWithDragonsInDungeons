using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ADND.test
{
    [TestFixture()]
    public class MapNavigatorTest
    {
		[Test()]
		public void ShouldReturnTileSetToInitialTile()
		{
            ICharacters mockCharacter = new MockCharacter();
            IList<ICharacters> mockParty = new List<ICharacters>();
            mockParty.Add(mockCharacter);
            IMapNavigator mapNavigator = new MapNavigator();
            IList<IMapTile> initialList = mapNavigator.GetMapTileCollection();

            Assert.AreEqual(1,initialList.Count);
            Assert.IsTrue(initialList[0].IsCurrent);
		}

 
		//[Test()]
		//public void ShouldBeCurrentTileSouthAndDifferntToInitialTile()
		//{
		//	ICharacters character = new MockCharacter();
		//	IMapNavigator mapNavigator = new MapNavigator(character);
  //          IList<IMapTile> mapTileList = mapNavigator.GetMapTileCollection();

		//	Assert.AreEqual(true, mapTileList[0].IsCurrent);
			
  //          IMapMovement moveDirection = new GoSouth(mapNavigator, character);
		
		//	moveDirection.Move();

		//	Assert.AreEqual(2, mapTileList.Count);
		//	Assert.AreNotEqual(true, mapTileList[0].IsCurrent);
		//	Assert.AreEqual(true, mapTileList[1].IsCurrent);
		//	Assert.AreEqual(mapTileList[0].positionY - 1, mapTileList[1].positionY);
		//	Assert.AreEqual(mapTileList[0].positionX, mapTileList[1].positionX);
		//	Assert.AreEqual(mapTileList[0].positionZ, mapTileList[1].positionZ);
		//}

		//[Test()]
		//public void ShouldBeCurrentTileEastAndDifferntToInitialTile()
		//{
		//	ICharacters character = new MockCharacter();
		//	IMapNavigator mapNavigator = new MapNavigator(character);
  //          IList<IMapTile> mapTileList = mapNavigator.GetMapTileCollection();

		//	Assert.AreEqual(true, mapTileList[0].IsCurrent);

		//	IMapMovement moveDirection = new GoEast(mapNavigator, character);
			
		//	moveDirection.Move();

		//	Assert.AreEqual(2, mapTileList.Count);
		//	Assert.AreNotEqual(true, mapTileList[0].IsCurrent);
		//	Assert.AreEqual(true, mapTileList[1].IsCurrent);
		//	Assert.AreEqual(mapTileList[0].positionX + 1, mapTileList[1].positionX);
		//	Assert.AreEqual(mapTileList[0].positionY, mapTileList[1].positionY);
		//	Assert.AreEqual(mapTileList[0].positionZ, mapTileList[1].positionZ);
		//}

		//[Test()]
		//public void ShouldBeCurrentTileWestAndDifferntToInitialTile()
		//{
		//	ICharacters character = new MockCharacter();
		//	IMapNavigator mapNavigator = new MapNavigator(character);
  //          IList<IMapTile> mapTileList = mapNavigator.GetMapTileCollection();

		//	Assert.AreEqual(true, mapTileList[0].IsCurrent);

		//	IMapMovement moveDirection = new GoWest(mapNavigator, character);

		//	moveDirection.Move();

		//	Assert.AreEqual(2, mapTileList.Count);
		//	Assert.AreNotEqual(true, mapTileList[0].IsCurrent);
		//	Assert.AreEqual(true, mapTileList[1].IsCurrent);
		//	Assert.AreEqual(mapTileList[0].positionX - 1, mapTileList[1].positionX);
		//	Assert.AreEqual(mapTileList[0].positionY, mapTileList[1].positionY);
		//	Assert.AreEqual(mapTileList[0].positionZ, mapTileList[1].positionZ);
		//}

    }
}
