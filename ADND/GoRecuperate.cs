using System;
using System.Collections.Generic;

namespace ADND
{
    public class GoRecuperate : IMapMovement
    {
        private IMapNavigator mapNavigator;
        private ICharacters player;
        //private bool needsToBeAdded; //todo is this needed? check during next refactoring...
        private IList<IMapTile> mapTileList;
        private IMapTile newDummyMapTile;
        private IMapTile oldMapTile;

        public GoRecuperate(IMapNavigator gameMap, ICharacters character)
        {
            player = character;
            mapNavigator = gameMap;
            mapTileList = mapNavigator.GetMapTileCollection();
            newDummyMapTile = new MapTile(player);
            oldMapTile = new MapTile(player);
            //needsToBeAdded = false;
        }

        public void Move()
        {
            RecuperateHitpoints();
        }
        private void RecuperateHitpoints()
        {
            IMessageChannel message = new MessageConsole();
            Random randomDice = new Random();
            int recuperate = randomDice.Next(1, 10);

            if ((player.hitpoints + recuperate) > player.maxHitpoints)
            {
                player.hitpoints = player.maxHitpoints;
            }
            else
            {
                player.hitpoints += recuperate;
            }

            message.MessagePush(string.Format("You have {0} hitpoints of {1} after resting.", player.hitpoints, player.maxHitpoints));

        }
    }
}