using System;
namespace ADND
{
	public class Orc : CharacterTemplate, ICharacters
	{
		public int level { get; set; }
		public int toHitAC0 { get; set; }

		public IWeapon weapon { get; set; }
		public IArmor armor { get; set; }

		public int hitPointFactor { get; }
		public string characterClassName { get; }
		public string characterTypeName { get; }

		private RandomSingleton randomDice;


		public Orc()
		{
            characterTypeName = "Monster";
            characterClassName = "Orc";
			name = "Orc";
			level = 4;
			weapon = new Sword();
			armor = new LeatherArmor();
			hitPointFactor = 8;
			toHitAC0 = 20 - level;
            baseArmorClass = 10;

			randomDice = RandomSingleton.Instance();
			xp = randomDice.Next(100, 200);
			gold = randomDice.Next(20, 50);

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
