using System;
using System.Collections.Generic;

namespace ADND
{
    public class CharacterChoser
    {
        IMessageChannel message;

        public CharacterChoser()
        {
             message = new MessageConsole();
        }
        public ICharacters ChoseCharacter(ICharacters character)
        {
            message.MessagePush("Do you want to keep this character?(Y/N)");

			switch (message.MessagePull())
			{
				case "y":
					 return character; 
					
				case "Y":
					return character; 
					
				default:
                    return ChoseCharacter(character);
			}
        }
		
    }
}
