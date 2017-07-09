using System;
namespace ADND
{
	public class Spider : CharacterTemplate, ICharacters
	{
		public int level { get; set; }
		public int toHitAC0 { get; set; }

		public IWeapon weapon { get; set; }
		public IArmor armor { get; set; }

		public int hitPointFactor { get; }
		public string characterClassName { get; }
		public string characterTypeName { get; }
		private RandomSingleton randomDice;

		public Spider()
		{
			characterTypeName = "Monster";
			characterClassName = "Spider";
			name = "Spider";
			level = 1;
			weapon = new BiteWeapon();
			armor = new HideArmor();
			hitPointFactor = 6;
			baseArmorClass = 10;
			toHitAC0 = 20 - level;

			randomDice = RandomSingleton.Instance();
			xp = randomDice.Next(5, 15);
			gold = 0;

			CharacterBuilder monsterBuilder = new CharacterBuilder();
			monsterBuilder.GenerateStats(this);
		}
	}
}
