using System;
using System.Collections.Generic;

namespace ADND
{
    public class MapNavigator : IMapNavigator
    {
        private IList<IMapTile> mapTileList;
        private IMapTile firstTile;

        public MapNavigator()
        {
            mapTileList = new List<IMapTile>();
            firstTile = new FirstTile();
            mapTileList.Add(firstTile);
        }

        public IList<IMapTile> GetMapTileCollection()
        {
            return mapTileList;
        }

    }
}
