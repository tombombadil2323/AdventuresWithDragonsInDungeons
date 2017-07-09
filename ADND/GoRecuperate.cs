using System;
using System.Collections.Generic;

namespace ADND
{
    public class GoRecuperate : IMapMovement
    {
        private IMapNavigator mapNavigator;
		private IList<ICharacters> playerCharacterList;
        //private bool needsToBeAdded; //todo is this needed? check during next refactoring...
        private IList<IMapTile> mapTileList;
        private IMapTile newDummyMapTile;
        private IMapTile oldMapTile;
		private IMessageChannel message = new MessageConsole();
        private RandomSingleton randomDice = RandomSingleton.Instance();

        public GoRecuperate(IMapNavigator gameMap,IList<ICharacters> partyList)
        {
            playerCharacterList = partyList;
            mapNavigator = gameMap;
            mapTileList = mapNavigator.GetMapTileCollection();
            newDummyMapTile = new MapTile(playerCharacterList);
            oldMapTile = new MapTile(playerCharacterList);
            //needsToBeAdded = false;
        }

        public void Move()
        {
            RecuperateHitpoints();
        }
        private void RecuperateHitpoints()
        {
            int recuperate = randomDice.Next(1, 10);

            foreach(ICharacters player in playerCharacterList)
            {
				if ((player.hitpoints + recuperate) > player.maxHitpoints)
				{
					player.hitpoints = player.maxHitpoints;
				}
				else
				{
					player.hitpoints += recuperate;
				}
                message.MessagePush(string.Format("{2} has {0} hitpoints of {1} after resting.", player.hitpoints, player.maxHitpoints, player.name));
			}
        }
    }
}