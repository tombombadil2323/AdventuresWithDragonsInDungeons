using System;
namespace ADND
{
    public class Game
    {
        IMessageChannel message;
        public ICharacters character { get; }

        public Game()
        {
            message = new MessageConsole();

			message.MessagePush("First we need to generate a player character..");
            CharacterBuilder characterBuilder = new CharacterBuilder();
            character = characterBuilder.CharacterBuilderExecution();

            message.MessagePush("OK, now let's get started!");

            IMapNavigator gameMapNavigator = new MapNavigator(character);
            do
            {
               IStory story = new Story(gameMapNavigator, character);
            } while (character.hitpoints > 0);

        }

    }
}
