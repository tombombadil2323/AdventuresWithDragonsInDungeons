using System;
using System.Collections.Generic;

namespace ADND
{
    public class MapNavigator : IMapNavigator
    {
        private IList<IMapTile> mapTileList;
        private IMapTile firstTile;
        private IMapTile newMapTile;
        private ICharacters player;

        public MapNavigator(ICharacters character)
        {
            mapTileList = new List<IMapTile>();
            player = character;
            newMapTile = new MapTile(player);
            firstTile = new FirstTile(player);
            mapTileList.Add(firstTile);
        }

        public IList<IMapTile> GetMapTileCollection()
        {
            return mapTileList;
        }

    }
}
