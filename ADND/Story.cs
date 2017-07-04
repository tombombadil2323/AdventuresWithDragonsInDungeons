using System;
namespace ADND
{
    public class Story : IStory
    {
        private IMapNavigator gameMapNavigator;
        private ICharacters player;
        private IMapTile currentMapTile;
        private RandomSingleton randomDice = RandomSingleton.Instance();
        private MapMovement mapMovementManager;

        public Story(IMapNavigator currentGameMapNavigator, ICharacters character)
        {
            
            gameMapNavigator = currentGameMapNavigator;
            player = character;
            currentMapTile = new MapTile(player);
            mapMovementManager = new MapMovement();
            mapMovementManager.AsksNextStep(currentGameMapNavigator,player);
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
