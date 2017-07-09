using System;
namespace ADND
{
	public class Giant : CharacterTemplate, ICharacters
	{
		public int level { get; set; }
		public int toHitAC0 { get; set; }

		public IWeapon weapon { get; set; }
		public IArmor armor { get; set; }

		public int hitPointFactor { get; }
		public string characterClassName { get; }
		public string characterTypeName { get; }

		private RandomSingleton randomDice;

		public Giant()
		{
            characterTypeName = "Monster";
            characterClassName = "Giant";
            name = "Giant";
			level = 6;
			weapon = new Sword();
			armor = new LeatherArmor();
            baseArmorClass = 10;
			hitPointFactor = 10;
			toHitAC0 = 20 - level;

			randomDice = RandomSingleton.Instance();
			xp = randomDice.Next(300, 500);
			gold = randomDice.Next(100, 500);

			CharacterBuilder monsterBuilder = new CharacterBuilder();
			monsterBuilder.GenerateStats(this);
		}

		public bool CheckIfDead()
		{
			if (this.hitpoints <= 0)
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
