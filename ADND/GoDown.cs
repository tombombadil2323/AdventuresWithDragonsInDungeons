using System;
using System.Collections.Generic;

namespace ADND
{
	public class GoDown : IMapMovement
	{
		private IMapNavigator mapNavigator;
		private IList<ICharacters>  playerCharacterList;
		private bool needsToBeAdded;
		private IList<IMapTile> mapTileList;
		private IMapTile newDummyMapTile;
        private IMapTile oldMapTile;

		public GoDown(IMapNavigator gameMap,IList<ICharacters> partyList)
		{
			playerCharacterList = partyList;
			mapNavigator = gameMap;
			mapTileList = mapNavigator.GetMapTileCollection();
			newDummyMapTile = new MapTile(playerCharacterList);
			oldMapTile = new MapTile(playerCharacterList);
		}

		public void Move()
		{
			CheckCurrentMapTile();
			CheckIfNewMapTile();
			AddMapTileIfNew();
		}

		private void CheckCurrentMapTile()
		{
			needsToBeAdded = true;

			//check which maptile is current
			foreach (IMapTile mapTiles in mapTileList)
			{
				if (mapTiles.IsCurrent == true)
				{
					mapTiles.IsCurrent = false;
					mapTiles.IsLast = true;
					newDummyMapTile.positionX = mapTiles.positionX;
					newDummyMapTile.positionY = mapTiles.positionY;
					newDummyMapTile.positionZ = mapTiles.positionZ - 1;
					break;
				}
			}
		}

		private void CheckIfNewMapTile()
		{
			//check if mapTile is new?
			foreach (IMapTile mapTiles in mapTileList)
			{
				if (newDummyMapTile.positionX == mapTiles.positionX && newDummyMapTile.positionY == mapTiles.positionY && newDummyMapTile.positionZ == mapTiles.positionZ)
				{
					mapTiles.IsCurrent = true;
					needsToBeAdded = false;
					break;
				}
			}
		}

		private void AddMapTileIfNew()
		{
			//manage case were maptile is new and set IsCurrent to true.
			if (needsToBeAdded == true)
			{
				newDummyMapTile.IsCurrent = true;
				mapTileList.Add(newDummyMapTile);
				needsToBeAdded = false;
			}
		}

	}
}
