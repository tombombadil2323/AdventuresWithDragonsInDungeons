using System;
using System.Collections.Generic;

namespace ADND
{
	public class GoSouth : IMapMovement
	{
		private IMapNavigator mapNavigator;
		private ICharacters player;
		private bool needsToBeAdded;
		private IList<IMapTile> mapTileList;
		private IMapTile newDummyMapTile;
		private IMapTile oldMapTile;

		public GoSouth(IMapNavigator gameMap, ICharacters character)
		{
			player = character;
			mapNavigator = gameMap;
			mapTileList = mapNavigator.GetMapTileCollection();
			newDummyMapTile = new MapTile(player);
			oldMapTile = new MapTile(player);
		}

		public void Move()
		{
			SetCurrentMapTile();
			CheckIfNewMapTile();
			AddMapTileIfNew();
		}

		private void SetCurrentMapTile()
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
					newDummyMapTile.positionY = mapTiles.positionY-1;
					newDummyMapTile.positionZ = mapTiles.positionZ;
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