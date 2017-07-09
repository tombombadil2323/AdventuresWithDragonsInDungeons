using System;
using System.Collections.Generic;

namespace ADND
{
    public class Story : IStory
    {
        private IMapNavigator gameMapNavigator;
        private IList<ICharacters> playerCharacterList;
        private IMapTile currentMapTile;
        private RandomSingleton randomDice = RandomSingleton.Instance();
        private MapMovement mapMovementManager;

        public Story(IMapNavigator currentGameMapNavigator,IList<ICharacters>  partyList)
        {
            gameMapNavigator = currentGameMapNavigator;
            playerCharacterList = partyList;
            currentMapTile = new MapTile(playerCharacterList);
            mapMovementManager = new MapMovement();
            mapMovementManager.AsksNextStep(currentGameMapNavigator,playerCharacterList);
            TriggerEncounter();

        }

        public void TriggerEncounter()
        {
            IdentifyCurrentMapTile();
            if (!currentMapTile.HadEncounter)
            {
				currentMapTile.GenerateEncounterType();
				currentMapTile.ExecuteEncounterType();
            }
            //else to check if there is an existing encounter and monsters are alive
        }

		private void IdentifyCurrentMapTile()
		{
			foreach (IMapTile mapTile in gameMapNavigator.GetMapTileCollection())
			{
				if (mapTile.IsCurrent)
				{
					currentMapTile = mapTile;
				}
			}
		}

    }
}
