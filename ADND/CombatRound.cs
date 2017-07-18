using System;
using System.Collections.Generic;

namespace ADND
{
    public class CombatRound
    {
        private IMessageChannel message;
        private IList<ICharacters> monsterList;
        private IList<ICharacters> playerList;
		private IList<ICharacters> combatantList;
		private IList<ICharacters> sortedInitiativeICharacterList;
		public SortedDictionary<int, ICharacters> sortedCharacterDictionary = new SortedDictionary<int, ICharacters>();
		private bool endOfCombat;
		private RandomSingleton randomDice;
        private CombatRoundActions actions;
        public CombatRound();
        private int partyGold;
		private int partyXp;

        {
			randomDice = RandomSingleton.Instance();
			actions = new CombatRoundActions(playerList, monsterList);
		}

        public void InitiateFirstRound()
        {
            {
                message.MessagePush(string.Format("You have been ambushed by {0} monster(s):", monsterList.Count));
                foreach (ICharacters monsterName in monsterList)
                {
                    message.MessagePush(string.Format("{0}", monsterName.characterClassName));
                }
            }
        }
        public void ExecuteRound(IList<ICharacters> players, IList<ICharacters> monsters)
        {
            playerList = players;
            monsterList = monsters;

            CycleThroughCharactersForCurrentRoundActions();

            CreateSortedInitiativeListFromSortedCharacterDirectory();

            message.MessagePush(string.Format("{0} has the initiative and strikes first!", sortedInitiativeICharacterList[0].name));

            foreach (ICharacters characters in sortedCharacterDictionary.Values)
            {
                if (!characters.isDead && !endOfCombat)
                {
                    characters.currentAction.TriggerAction();
                    SetIsDeadFlagForDeadCharacters();
                }
                CheckIfAnyCharactersAreDead();
            }
            CleanUpListsAtEndOfRound();
        }

        private void CycleThroughCharactersForCurrentRoundActions()
        {
            foreach (ICharacters playerCharacter in playerList)
            {
                CheckCharacterTypeTakingAction(playerCharacter);
            }
            foreach (ICharacters monsterCharacter in monsterList)
            {
                CheckCharacterTypeTakingAction(monsterCharacter);
            }
        }

        private void CheckCharacterTypeTakingAction(ICharacters actionTaker)
        {
            if (actionTaker.characterTypeName == "Player")
            {
                ActionCharacterClassChecker(actionTaker);
            }
            if (actionTaker.characterTypeName == "Monster")
            {
                actions.DetermineMonsterActions(actionTaker);
            }
        }

        private void ActionCharacterClassChecker(ICharacters actionTaker)
        {
            if (actionTaker.characterClassName == "Fighter")
            {
                actions.AskForFighterActions(actionTaker);
            }

            if (actionTaker.characterClassName == "Wizard")
            {
                actions.AskForWizardActions(actionTaker);
            }
        }

        public void CreateSortedInitiativeListFromSortedCharacterDirectory()
        {
            int characterInitiativeRoll = -1;
            SortedDictionary<int, ICharacters> previousRoundSortedCharacterDictionary = new SortedDictionary<int, ICharacters>(sortedCharacterDictionary);

            foreach (KeyValuePair<int, ICharacters> kvp in previousRoundSortedCharacterDictionary)
            {
                characterInitiativeRoll = randomDice.Next(1, 11)
                                                   - kvp.Value.initiativeBonus
                                                   + kvp.Value.weapon.initiativeFactor
                                                   + kvp.Value.armor.initiativeFactor;

                while (sortedCharacterDictionary.ContainsKey(characterInitiativeRoll))
                {
                    characterInitiativeRoll = randomDice.Next(1, 11)
                                                  - kvp.Value.initiativeBonus
                                                   + kvp.Value.weapon.initiativeFactor
                                                   + kvp.Value.armor.initiativeFactor;
                }
                sortedCharacterDictionary.Add(characterInitiativeRoll, kvp.Value);
                sortedCharacterDictionary.Remove(kvp.Key);
            }

            foreach (ICharacters character in sortedCharacterDictionary.Values)
            {
                sortedInitiativeICharacterList.Add(character);
            }
        }

        private void SetIsDeadFlagForDeadCharacters()
        {
            foreach (KeyValuePair<int, ICharacters> kvp in sortedCharacterDictionary)
            {
                if (kvp.Value.hitpoints < 0)
                {
                    sortedCharacterDictionary[kvp.Key].isDead = true;
                }
            }
        }

        private void CheckIfAnyCharactersAreDead()
        {
            foreach (ICharacters character in sortedCharacterDictionary.Values)
            {
                //if (characterChecker.CheckIfCharacterIsDead(character))
                //todo delete characterchecker class
                //todo add deadPlayerCharacterList and delete from playerList
                if (character.isDead == true)
                {
                    if (character.characterTypeName == "Player")
                    {
                        //todo display this message only once for each player and at the end of the combat
                        message.MessagePush(string.Format("{0} is dead!", character.name));
                    }

                    if (character.characterTypeName == "Monster")
                    {
                        message.MessagePush(string.Format("You killed {0}!", character.name));

                        foreach (ICharacters playerCharacter in playerList)
                        {
                            playerCharacter.gold += character.gold;
                            partyGold += character.gold;

                            playerCharacter.xp += character.xp;
                            partyXp += character.xp;
                        }
                    }
                }
            }
        }

        private void CleanUpListsAtEndOfRound()
        {
            combatantList.Clear();
            monsterList.Clear();
            playerList.Clear();
            foreach (ICharacters characters in sortedCharacterDictionary.Values)
            {
                if (characters.isDead != true)
                {
                    combatantList.Add(characters);

                    if (characters.characterTypeName == "Player")
                    {
                        playerList.Add(characters);
                    }
                    if (characters.characterTypeName == "Monster")
                    {
                        monsterList.Add(characters);
                    }
                }
            }
            sortedInitiativeICharacterList.Clear();
        }

        public bool CheckIfHasRun()
        {
            CombatRoundActions runAction = new CombatRoundActions(playerList, monsterList);
            if(runAction.ExecuteRunAction())
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
