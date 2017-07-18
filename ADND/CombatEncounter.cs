using System;
using System.Collections.Generic;
using System.Linq;

namespace ADND
{
    public class CombatEncounter : IEncounter
    {

        private int xp;
        private int partyGold;
        private int partyXp;
        private int roundCounter;


        private bool endOfCombat;

        private CombatRound currentRound;
        private IMessageChannel message;
		private RandomSingleton randomDice;
		private ICharacterChecker characterChecker;

		private IList<ICharacters> combatantList = new List<ICharacters>();
        private IList<ICharacters> monsterList = new List<ICharacters>();
        private IList<ICharacters> playerList = new List<ICharacters>();
        private IList<ICharacters> sortedInitiativeICharacterList = new List<ICharacters>();

        private IDictionary<int, ICharacters> combatantDictionary = new SortedDictionary<int, ICharacters>();
		//public for unit testing reasons only:
		public SortedDictionary<int, ICharacters> sortedCharacterDictionary = new SortedDictionary<int, ICharacters>();

        public CombatEncounter()
        {
			xp = 0;
            partyGold = 0;
            partyXp = 0;
            randomDice = RandomSingleton.Instance();
			roundCounter = 0;
			hasRunAway = false;
			endOfCombat = false;

            currentRound = new CombatRound();
	        message = new MessageConsole();
            //todo do I need this?
            characterChecker = new CharacterChecker();

			combatantList = new List<ICharacters>();
			monsterList = new List<ICharacters>();
			playerList = new List<ICharacters>();
			sortedInitiativeICharacterList = new List<ICharacters>();

			combatantDictionary = new SortedDictionary<int, ICharacters>();
			sortedCharacterDictionary = new SortedDictionary<int, ICharacters>();
        }

        public void EncounterFlow(IList<ICharacters> characterList)
        {
            combatantList = characterList;

            FillCharacterCollections();

            currentRound.InitiateFirstRound();
            roundCounter = 1;

			while (!hasRunAway || !endOfCombat)
            {
				message.MessagePush("Round " + roundCounter + ":");

				currentRound.ExecuteRound(playerList,monsterList);

				if (monsterList.Count() == 0)
				{
					message.MessagePush(string.Format("All monsters are dead and you have won!"));
					message.MessagePush(string.Format("Your party has gained {0} experience and found {1} gold", partyXp, partyGold));


					foreach (ICharacters playerCharacter in playerList)
					{
						message.MessagePush(string.Format("{0} has {1} hitpoints of {2} left\n", playerCharacter.name, playerCharacter.hitpoints, playerCharacter.maxHitpoints));
					}
					endOfCombat = true;
				}

				if (playerList.Count() == 0)
				{
					message.MessagePush(string.Format("All party members have been killed by monsters! Better luck next time.."));
					endOfCombat = true;
				}

                if (currentRound.CheckIfHasRun)
                {
                    
                }

				else
				{
					message.MessagePush(string.Format("After round {0} the following party members are still alive and have the following hitpoints:", roundCounter));
					foreach (ICharacters playerCharacter in playerList)
					{
						message.MessagePush(string.Format("{0} has {1} hitpoints left.", playerCharacter.name, playerCharacter.hitpoints));
					}

					message.MessagePush(string.Format("And the following monsters are still alive:"));
					foreach (ICharacters monsterCharacter in monsterList)
					{
						message.MessagePush(string.Format("{0}", monsterCharacter.name));
					}
				}

                roundCounter++;
            }
        }

        private void FillCharacterCollections()
        {
			foreach (ICharacters characters in combatantList)
			{
				if (characters.characterTypeName == "Monster")
				{
					monsterList.Add(characters);
				}

				if (characters.characterTypeName == "Player")
				{
                    //todo not needed if dead characters are discarded between encounters
                    if(characters.isDead!=true)
                    {
                       playerList.Add(characters); 
                    }
				}
			}

			for (int i = 0; i < combatantList.Count; i++)
			{
				sortedCharacterDictionary.Add(-i, combatantList[i]);
			}
        }

	}
}
