using System;
using System.Collections.Generic;

namespace ADND
{
    public class CombatEncounterAction : ICombatEncounterAction
    {
        ICharacters actionTaker;
        IList<ICharacters> targetCharacterList;
        IMessageChannel message = new MessageConsole();
        ICharacters target;
        CombatEncounter combatEncounterSource;


        public CombatEncounterAction()
        {
            
        }
        public ICombatEncounterAction AskForAction(ICharacters character, IList<ICharacters> characterList, CombatEncounter reference)
        {
            actionTaker = character;
            targetCharacterList = characterList;
            combatEncounterSource = reference;

            CharacterTypeChecker();

            return this;
        }
        public void CharacterTypeChecker()
        {
            if (actionTaker.characterTypeName=="Player")
            {
                CharacterClassChecker();
            }
            if (actionTaker.characterTypeName == "Monster")
			{
				MonsterActions();
			}
        }
        public void CharacterClassChecker()
        {
            if(actionTaker.characterClassName=="Fighter")
            {
                FighterActions();
            }

			if (actionTaker.characterClassName == "Wizard")
			{
                WizardActions();
			}
        }
        private void FighterActions()
        {
			message.MessagePush("What do you want to do? (F)ight or try to (r)un away?");
			string reply = message.MessagePull();
			if (reply == "r" || reply == "R")
			{
                combatEncounterSource.Run();
			}
            if (reply == "f" || reply == "F")
			{
				message.MessagePush("Who do you want to fight? Please enter number of opponent!");
                targetCharacterList.Remove(actionTaker);
                message.MessagePush(targetCharacterList);
                int targetID;
                int.TryParse(message.MessagePull(), out targetID);
                target = targetCharacterList[targetID];

			}
        }
		private void WizardActions()
		{

		}
		private void MonsterActions()
		{

		}
    }
}
