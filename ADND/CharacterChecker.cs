using System;
namespace ADND
{
    public class CharacterChecker : ICharacterChecker
    {
        private ICharacters characterToBeChecked;

        public bool CheckIfCharacterIsDead(ICharacters character)
		{
			characterToBeChecked = character;

			if (characterToBeChecked.isDead)
			{
				return true;
			}
			else
			{
				return false;
			}

		}
    }
}
