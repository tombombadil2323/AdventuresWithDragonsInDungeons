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

            FillCharacterCollections();

            InitiateFirstRound();

            while (!hasRunAway && !endOfCombat)
            {
                ExecuteRound();
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

		private void InitiateFirstRound()
		{
			roundCounter = 0;
			{
				message.MessagePush(string.Format("You have been ambushed by {0} monster(s):", monsterList.Count()));
				foreach (ICharacters monsterName in monsterList)
				{
					message.MessagePush(string.Format("{0}", monsterName.characterClassName));
				}
			}
		}

		public void ExecuteRound()
		{
			if (!endOfCombat || !hasRunAway)
			{
				roundCounter = roundCounter + 1;
				message.MessagePush("Round " + roundCounter + ":");

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

        //todo bug: index out of range exception at end of combat if all players dead
        private void DetermineMonsterActions(ICharacters actionTaker)
        {
            if(playerList.Count()>0)
            {
				int playerID = randomDice.Next(0, playerList.Count());
				ICharacters actionReceiver = playerList[playerID];
				actionTaker.currentAction = new CombatAttackAction(actionTaker, actionReceiver, this);
            }
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
			string actionName = Enum.GetName(typeof(FighterOptions), choice);
			if (actionName == "Attack")
			{
				AttackActionOption(actionTaker);
			}
			if (actionName == "Run")
			{
				RunActionOption(actionTaker);
			}
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

        //refactor with other ActionOptions
        private void AttackActionOption(ICharacters actionTaker)
		{
			message.MessagePush("Who do you want to Attack?");
			foreach (ICharacters monsterCharacter in monsterList)
			{
				message.MessagePush(string.Format("[{0}] {1}", monsterList.IndexOf(monsterCharacter), monsterCharacter.name));
			}

            //try catch inputs out of range of monsterlist.count
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

        private void SetIsDeadFlagForDeadCharacters()
        {
            foreach(KeyValuePair<int,ICharacters> kvp in sortedCharacterDictionary)
            {
                if(kvp.Value.hitpoints<0)
                {
                    sortedCharacterDictionary[kvp.Key].isDead=true;
                }
            }
        }

        private void CheckIfAnyCharactersAreDead()
        {
            foreach(ICharacters character in sortedCharacterDictionary.Values)
            {
                //if (characterChecker.CheckIfCharacterIsDead(character))
                //todo delete characterchecker class
                //todo add deadPlayerCharacterList and delete from playerList
                if (character.isDead==true)
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
            int damage = spell.CastSpell(attacker);
            defender.hitpoints -= damage;
            message.MessagePush(string.Format("{3}'s {0} has done {1} points of damage to {2}.", spell.name,damage,defender.name, attacker.name));
            Console.ReadLine();
        }

        public void ApplyDamage(ICharacters attacker, ICharacters damageTaker)
		{
			int damage = 0;

			damage = randomDice.Next(1, attacker.weapon.damage);
			int finalDamage = damage + attacker.damageBonus;
			damageTaker.hitpoints -= finalDamage;

            message.MessagePush(string.Format("{0} did {1} damage to {2}.", attacker.name, finalDamage, damageTaker.name));
		    Console.ReadLine();
        }

        private void CleanUpListsAtEndOfRound()
        {
            combatantList.Clear();
            monsterList.Clear();
            playerList.Clear();
            foreach(ICharacters characters in sortedCharacterDictionary.Values)
            {
                if (characters.isDead!=true)
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
				message.MessagePush(string.Format("All monsters are dead and you have won!"));
                message.MessagePush(string.Format("Your party has gained {0} experience and found {1} gold",partyXp, partyGold));


				foreach (ICharacters playerCharacter in playerList)
				{
                    message.MessagePush(string.Format("{0} has {1} hitpoints of {2} left\n",playerCharacter.name, playerCharacter.hitpoints, playerCharacter.maxHitpoints));
				}
				endOfCombat = true;
			}
            if(playerList.Count()==0)
            {
                message.MessagePush(string.Format("All party members have been killed by monsters! Better luck next time.."));
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

            SetIsDeadFlagForDeadCharacters();
		}
	}
}
