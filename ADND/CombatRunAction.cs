using System;
namespace ADND
{
    public class CombatRunAction : IAction
    {
		private CombatEncounter currentSource;
		private Action<ICharacters, ICharacters> currentAction;
		private ICharacters currentAttacker;
		private ICharacters currentRunner;

        public CombatRunAction(ICharacters runner, ICharacters attacker, CombatEncounter source)
        {
			currentRunner = runner;
			currentAttacker = attacker;
			currentSource = source;
			LoadAction();
        }

		public void LoadAction()
		{
			currentAction = new Action<ICharacters, ICharacters>(currentSource.ExecuteRunAction);
		}

		public void TriggerAction()
		{
			currentAction(currentRunner, currentAttacker);
		}
    }
}
