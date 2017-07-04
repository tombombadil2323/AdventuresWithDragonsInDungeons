using System;
namespace ADND
{
    public class CombatAttackAction : IAction
    {
        private CombatEncounter currentSource;
        private Action<ICharacters, ICharacters> currentAction;
        private ICharacters currentDefender;
        private ICharacters currentAttacker;

        public CombatAttackAction(ICharacters attacker, ICharacters defender, CombatEncounter source)
        {
			currentAttacker = attacker;
			currentDefender = defender;
			currentSource = source;
            LoadAction();
        }

        public void LoadAction()
        {
			currentAction = new Action<ICharacters, ICharacters>(currentSource.ExecuteMeleeAttack);
        }

        public void TriggerAction()
        {
            currentAction(currentAttacker, currentDefender);
        }

    }
}