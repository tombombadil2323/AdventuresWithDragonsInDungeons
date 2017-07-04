using System;
using System.Collections.Generic;

namespace ADND
{
    

    public interface IMapNavigator
    {
        IList<IMapTile> GetMapTileCollection();
    }
}