using System;
namespace ADND
{
    internal class CombatSpellCastingAction : IAction
    {
		private CombatEncounter currentSource;
		private Action<Wizard, ICharacters, ISpells> currentAction;
		private ICharacters currentDefender;
		private ICharacters currentAttacker;
        private ISpells currentSpell;

        public CombatSpellCastingAction(ICharacters attacker, ICharacters defender, CombatEncounter source, ISpells spell)
        {
			currentAttacker = attacker;
			currentDefender = defender;
			currentSource = source;
            currentSpell = spell;
			LoadAction();
        }

		public void LoadAction()
		{
            currentAction = new Action<Wizard, ICharacters, ISpells>(currentSource.ExecuteSpellAttack);
		}

		public void TriggerAction()
		{
			currentAction((Wizard)currentAttacker, currentDefender, currentSpell);
		}

	}
}