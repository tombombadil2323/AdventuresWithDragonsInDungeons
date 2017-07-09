using System;
using System.Collections.Generic;

namespace ADND
{
    public class Game
    {
        private IMessageChannel message;
        private ICharacters playerCharacter;
        private IList<ICharacters> playerParty;
        public const int playerMaxPerParty= 3;

        public Game()
        {
            message = new MessageConsole();
            playerParty = new List<ICharacters>();

			message.MessagePush("First we need to generate a party of three player characters..");
            GenerateParty();

            message.MessagePush("OK, now let's get started!");
            GenerateStory();
        }
        private void GenerateParty()
        {
            int playerCounter = 0;
            while(playerCounter<playerMaxPerParty)
            {
				CharacterBuilder characterBuilder = new CharacterBuilder();
				playerCharacter = characterBuilder.CharacterBuilderExecution();
                playerParty.Add(playerCharacter);
                playerCounter++;
            }
        }

        private void GenerateStory()
        {
			IMapNavigator gameMapNavigator = new MapNavigator();
			do
			{
				IStory story = new Story(gameMapNavigator, playerParty);
			} while (playerCharacter.hitpoints > 0);  
        }

    }
}
