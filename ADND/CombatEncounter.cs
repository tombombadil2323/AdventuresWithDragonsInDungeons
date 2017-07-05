using System;
using System.Collections.Generic;
using System.Linq;
//test
namespace ADND
{
    public class CombatEncounter : IEncounter
    {

        private int xp;
        private int partyGold;
        private int partyXp;
        private int roundCounter;

        private bool hasRunAway;
        private bool endOfCombat;

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

	        message = new MessageConsole();
            characterChecker = new CharacterChecker();

			combatantList = new List<ICharacters>();
			monsterList = new List<ICharacters>();
			playerList = new List<ICharacters>();
			sortedInitiativeICharacterList = new List<ICharacters>();

			combatantDictionary = new SortedDictionary<int, ICharacters>();
			sortedCharacterDictionary = new SortedDictionary<int, ICharacters>();
        }

        //constructor for unit testing purposes only
        public CombatEncounter(IList<ICharacters> characterList)
        {
            combatantList = characterList;
            characterChecker = new CharacterChecker();
            message = new MessageConsole();
        }

        public void EncounterFlow(IList<ICharacters> characterList)
        {
            combatantList = characterList;

            foreach (ICharacters characters in combatantList)
            {
                if (characters.characterTypeName == "Monster")
                {
                    monsterList.Add(characters);

                }
                if (characters.characterTypeName == "Player")
                {
                    playerList.Add(characters);
                }
            }

            for (int i = 0; i < combatantList.Count; i++)
            {
                sortedCharacterDictionary.Add(-i, combatantList[i]);
            }

            InitiateFirstRound();

            while (!hasRunAway && !endOfCombat)
            {
                ExecuteRound();
            }
        }

        private void InitiateFirstRound()
        {
            roundCounter = 0;
            {
                message.MessagePush(string.Format("You have been ambushed by {0} monster(s):",monsterList.Count()));
                foreach(ICharacters monsterName in monsterList)
                {
                    message.MessagePush(string.Format("{0}", monsterName.characterClassName));
                }
            }
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
                DetermineMonsterActions(actionTaker);
            }
        }

        private void ActionCharacterClassChecker(ICharacters actionTaker)
        {
            if (actionTaker.characterClassName == "Fighter")
            {
                AskForFighterActions(actionTaker);
            }

            if (actionTaker.characterClassName == "Wizard")
            {
                AskForWizardActions(actionTaker);
            }
        }

        private void DetermineMonsterActions(ICharacters actionTaker)
        {
                int playerID= randomDice.Next(0,playerList.Count());
				ICharacters actionReceiver = playerList[playerID];
				actionTaker.currentAction = new CombatAttackAction(actionTaker, actionReceiver, this);
        }

        private void AskForFighterActions(ICharacters actionTaker)
		{
            int choice;

            message.MessagePush(string.Format("What action does the {0} {1} want to take?",actionTaker.characterClassName, actionTaker.name));
            foreach (int value in Enum.GetValues(typeof(FighterOptions)))
            {
                string optionName = Enum.GetName(typeof(FighterOptions), value);
                message.MessagePush(string.Format("[{0}] {1}", value, optionName));
            }

            int.TryParse(message.MessagePull(), out choice);
            string actionName = Enum.GetName(typeof(FighterOptions),choice);
            if (actionName=="Attack")
            {
               AttackActionOption(actionTaker);
            }
            if (actionName=="Run")
            {
                RunActionOption(actionTaker);
			}
        }

        //refactor with other ActionOptions
        private void AttackActionOption(ICharacters actionTaker)
		{
			message.MessagePush("Who do you want to Attack?");
			foreach (ICharacters monsterCharacter in monsterList)
			{
				message.MessagePush(string.Format("[{0}] {1}", monsterList.IndexOf(monsterCharacter), monsterCharacter.name));
			}

			int.TryParse(message.MessagePull(), out int monsterChoiceID);
			ICharacters actionReceiver = monsterList[monsterChoiceID];
			actionTaker.currentAction = new CombatAttackAction(actionTaker, actionReceiver, this);
		}

        private void SpellCastingActionOption(ICharacters actionTaker)
        {
			message.MessagePush("Which spell do you wish to cast?");
			Wizard wizardActionTaker = (Wizard)actionTaker;

			foreach (ISpells spell in wizardActionTaker.spellBook)
			{
				message.MessagePush(string.Format("[{0}] {1}", wizardActionTaker.spellBook.IndexOf(spell), spell.name));
			}

			int.TryParse(message.MessagePull(), out int spellChoiceID);
			message.MessagePush("Who do you want to Attack?");

			foreach (ICharacters monsterCharacter in monsterList)
			{
				message.MessagePush(string.Format("[{0}] {1}", monsterList.IndexOf(monsterCharacter), monsterCharacter.name));
			}

			int.TryParse(message.MessagePull(), out int monsterChoiceID);
			ICharacters actionReceiver = monsterList[monsterChoiceID];

            actionTaker.currentAction = new CombatSpellCastingAction(actionTaker, actionReceiver, this, wizardActionTaker.spellBook[spellChoiceID]);
		}

	    private void RunActionOption(ICharacters actionTaker)
        {
			actionTaker.currentAction = new CombatRunAction(actionTaker, monsterList[0], this);
        }

        private void AskForWizardActions(ICharacters actionTaker)
		{
			int choice;
			message.MessagePush(string.Format("What action does the {0} {1} want to take?", actionTaker.characterClassName, actionTaker.name));
            foreach (int value in Enum.GetValues(typeof(WizardOptions)))
            {
                string optionName = Enum.GetName(typeof(WizardOptions), value);
                message.MessagePush(string.Format("[{0}] {1}", value, optionName));
            }

			int.TryParse(message.MessagePull(), out choice);
			string actionName = Enum.GetName(typeof(WizardOptions), choice);

			if (actionName == "Attack")
			{
                AttackActionOption(actionTaker);
			}

			if (actionName == "Spellcasting")
			{
               SpellCastingActionOption(actionTaker);
			}

			if (actionName == "Run")
			{
			    RunActionOption(actionTaker);	
            }
		}

        public void ExecuteRound()
		{
            if (!endOfCombat ||!hasRunAway)
            {
				roundCounter = roundCounter + 1;
				message.MessagePush("Round " + roundCounter + ":");

				foreach (ICharacters players in playerList)
				{
					CycleThroughCharactersForCurrentRoundActions();
				}

				CreateSortedInitiativeListFromSortedCharacterDirectory();

				message.MessagePush(string.Format("{0} has the initiative and strikes first!", sortedInitiativeICharacterList[0].name));

				foreach (ICharacters characters in sortedCharacterDictionary.Values)
				{
					if (!characters.isDead && !endOfCombat)
					{
						characters.currentAction.TriggerAction();
						CheckIfAnyCharactersAreDead();
					}
                    CheckIfCombatHasEnded();
				}
                CheckIfAnyCharactersAreDead();
				CleanUpListsAtEndOfRound();
            }
		}
        private void CheckIfAnyCharactersAreDead()
        {
            foreach(KeyValuePair<int,ICharacters> kvp in sortedCharacterDictionary)
            {
                if(kvp.Value.hitpoints<0)
                {
                    sortedCharacterDictionary[kvp.Key].isDead=true;
                }
            }
        }

        private void CheckIfCombatHasEnded()
        {
            foreach(ICharacters characters in sortedCharacterDictionary.Values)
            {
                if (characterChecker.CheckIfCharacterIsDead(characters))
                {
                    if (characters.characterTypeName == "Player")
                    {
                        message.MessagePush(string.Format("You are dead!"));
                        Game game = new Game();
                    }

					if (characters.characterTypeName == "Monster")
					{
						foreach (ICharacters playerCharacter in playerList)
						{
							playerCharacter.gold += characters.gold;
                            partyGold += characters.gold;

							playerCharacter.xp += characters.xp;
                            partyXp += characters.xp;
						}
                    }
                }
			}
            //refactor to seperate function
            if (hasRunAway)
            {
                foreach(ICharacters playerCharacter in playerList)
                {
					xp = randomDice.Next(1, 5*playerCharacter.level);
					playerCharacter.xp += xp;
                    message.MessagePush(string.Format("{3} gained {0} experience and has {1} hitpoints of {2} left\n", xp, playerCharacter.hitpoints, playerCharacter.maxHitpoints, playerCharacter.name));
                    partyXp += xp;
                    xp = 0;
                }
                endOfCombat = true;
            }
        }

        public void CreateSortedInitiativeListFromSortedCharacterDirectory()
		{
            int characterInitiativeRoll=-1;
            SortedDictionary<int,ICharacters> previousRoundSortedCharacterDictionary = new SortedDictionary<int, ICharacters>(sortedCharacterDictionary);

			foreach(KeyValuePair<int,ICharacters> kvp in previousRoundSortedCharacterDictionary)
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
                sortedCharacterDictionary.Add(characterInitiativeRoll,kvp.Value);
                sortedCharacterDictionary.Remove(kvp.Key);
            }

            foreach(ICharacters character in sortedCharacterDictionary.Values)
            {
                sortedInitiativeICharacterList.Add(character);
            }
		}

		public void ExecuteMeleeAttack(ICharacters attacker, ICharacters defender)
		{
			int target = attacker.toHitAC0 - defender.armorClass;
			int dieCast = randomDice.Next(1, 21);

			if (dieCast >= target)
			{
				message.MessagePush(string.Format("{0} has hit!", attacker.name));
				ApplyDamage(attacker, defender);
			}
			else
			{
				Console.WriteLine(string.Format("{0} has missed!\n", attacker.name));
				Console.ReadLine();
			}
		}

        public void ExecuteSpellAttack (Wizard attacker, ICharacters defender, ISpells spell)
        {
            int damage= spell.CastSpell(attacker);
            defender.hitpoints -= damage;
            message.MessagePush(string.Format("{3}'s {0} has done {1} points of damage to {2}.", spell.name,damage,defender.name, attacker.name));

        }

        public void ApplyDamage(ICharacters attacker, ICharacters damageTaker)
		{
			int damage = 0;

			damage = randomDice.Next(1, attacker.weapon.damage);
			int finalDamage = damage + attacker.damageBonus;
			damageTaker.hitpoints -= finalDamage;

            message.MessagePush(string.Format("{0} did {1} damage to {2}.", attacker.name, finalDamage, damageTaker.name));
		}

        private void CleanUpListsAtEndOfRound()
        {
            combatantList.Clear();
            monsterList.Clear();
            playerList.Clear();
            foreach(ICharacters characters in sortedCharacterDictionary.Values)
            {
                if (!characters.isDead)
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

			if (monsterList.Count() == 0)
			{
				foreach (ICharacters playerCharacter in playerList)
				{
					message.MessagePush(string.Format("All monsters are dead and you have won, found {0} gold and gained {1} experience and have {2} hitpoints of {3} left\n", partyGold, partyXp, playerCharacter.hitpoints, playerCharacter.maxHitpoints));
				}
				endOfCombat = true;
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
        }

        public void ExecuteRunAction(ICharacters player, ICharacters monster)
		{
			message.MessagePush(string.Format("During your flight the {0} has one last chance to hit you...", monster.name));

			ExecuteMeleeAttack(monster, player);

            hasRunAway = true;

            CheckIfAnyCharactersAreDead();
		}
	}
}
