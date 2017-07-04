//using NUnit.Framework;
//using System.Collections.Generic;
//using System;
//namespace ADND.test
//{
    
//    [TestFixture()]
  
//    public class StoryTest
//    {
//        [Test()]
//        public void ShouldBeMapTile()
//        {
//            IMapNavigator map = new MockMap();
//            Story story = new Story(map);

//            story.AsksNextStep();

//        }
//		public class MockMap : IMapNavigator
//	    {
//			public IList<IMapTile> mapTileList { get; set; }
//			IMapTile mapTile { get; set; }

//			public MockMap()
//			{
//				mapTileList = new List<IMapTile>();
//				mapTile = new MockMapTile();

//				mapTile.positionX = 0;
//				mapTile.positionY = 0;
//				mapTile.positionZ = 0;
//				mapTile.description = "You were wandering " +
//					"through the deep dark wood looking for mushrooms. " +
//					"Suddendly the earth beneath your feet gives way and you fall into darkness!" +
//					"You scramble to your feet, checking that you have not hurt yourself. " +
//					"Then your eyes adjust to the darkness and you see that you are in a small " +
//					"hallway of some sort of a dungeon and the hole in the roof is too high to reach." +
//					"You will need to look for another exit. Strangely the hallway is dimly lit by the " +
//					"fluourescent moss that seems to be very common down here.";
//                mapTileList.Add(mapTile);
//			}

//			public void GoNorth()
//			{
//				IMapTile newMapTile = new MockMapTile();
//				IMapTile oldMapTile = mapTileList[mapTileList.Count];
//				newMapTile.positionY = oldMapTile.positionY + 1;
//				mapTileList.Add(newMapTile);
//			}
//			public void GoEast()
//			{
//				IMapTile newMapTile = new MapTile();
//				IMapTile oldMapTile = mapTileList[mapTileList.Count];
//				newMapTile.positionX = oldMapTile.positionX + 1;
//				mapTileList.Add(newMapTile);
//			}
//			public void GoSouth()
//			{
//				IMapTile newMapTile = new MapTile();
//				IMapTile oldMapTile = mapTileList[mapTileList.Count];
//				newMapTile.positionY = oldMapTile.positionY - 1;
//				mapTileList.Add(newMapTile);
//			}
//			public void GoWest()
//			{
//				IMapTile newMapTile = new MapTile();
//				IMapTile oldMapTile = mapTileList[mapTileList.Count];
//				newMapTile.positionX = oldMapTile.positionX - 1;
//				mapTileList.Add(newMapTile);
//			}
//	    }

//        public class MockMapTile : IMapTile
//        {
//		public int positionX { get; set; }
//		public int positionY { get; set; }
//		public int positionZ { get; set; }
//		public string description { get; set; }

//            public bool IsCurrent { get; set; }
//        }
//    }

//}
