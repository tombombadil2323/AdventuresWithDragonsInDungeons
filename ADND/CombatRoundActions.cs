using System;
using System.Collections.Generic;

namespace ADND
{
    public class CombatRoundActions
    {
        private IMessageChannel message;
		private RandomSingleton randomDice;
        private IList<ICharacters> playerList;
        private IList<ICharacters> monsterList;

        public CombatRoundActions(IList<ICharacters> players, IList<ICharacters> monsters)
        {
            playerList = players;
            monsterList = monsters;
			message = new MessageConsole();
			randomDice = RandomSingleton.Instance();

		}

		//todo bug: index out of range exception at end of combat if all players dead
		public void DetermineMonsterActions(ICharacters actionTaker)
		{
            int playerID = randomDice.Next(0, playerList.Count);
				ICharacters actionReceiver = playerList[playerID];
				actionTaker.currentAction = new CombatAttackAction(actionTaker, actionReceiver, this);
		}

		public void AskForFighterActions(ICharacters actionTaker)
		{
			message.MessagePush(string.Format("What action does the {0} {1} want to take?", actionTaker.characterClassName, actionTaker.name));

			foreach (int value in Enum.GetValues(typeof(FighterOptions)))
			{
				string optionName = Enum.GetName(typeof(FighterOptions), value);
				message.MessagePush(string.Format("[{0}] {1}", value, optionName));
			}

			int.TryParse(message.MessagePull(), out int choice);
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
		public void AskForWizardActions(ICharacters actionTaker)
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

		public void ExecuteSpellAttack(Wizard attacker, ICharacters defender, ISpells spell)
		{
			int damage = spell.CastSpell(attacker);
			defender.hitpoints -= damage;
			message.MessagePush(string.Format("{3}'s {0} has done {1} points of damage to {2}.", spell.name, damage, defender.name, attacker.name));
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

		public bool ExecuteRunAction()
		{
            int playerID=randomDice.Next(0,playerList.Count);
            int monsterID = randomDice.Next(0, monsterList.Count);
			message.MessagePush(string.Format("During your flight the {0} has one last chance to hit you...", monsterList[monsterID].name));

			ExecuteMeleeAttack(monsterList[monsterID], playerList[playerID]);

			return true;
		}

	}
}
