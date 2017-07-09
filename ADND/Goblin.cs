using System;
namespace ADND
{
    public class Goblin : CharacterTemplate, ICharacters
    {
		public int level { get; set; }
		public int toHitAC0 { get; set; }

		public IWeapon weapon { get; set; }
		public IArmor armor { get; set; }

		public int hitPointFactor { get; }
		public string characterClassName { get; }
		public string characterTypeName { get; }

        private RandomSingleton randomDice;
		
        public Goblin()
        {
            characterTypeName = "Monster";
            characterClassName = "Goblin";
			name = "Goblin";
			level = 2;
			weapon = new Dagger();
			armor = new LeatherArmor();
			hitPointFactor = 8;
            baseArmorClass = 10;
            toHitAC0 = 20 - level;

            randomDice = RandomSingleton.Instance();
            xp = randomDice.Next(25,100);
            gold = randomDice.Next(5, 25);

			CharacterBuilder monsterBuilder = new CharacterBuilder();
			monsterBuilder.GenerateStats(this);
        }
    }
}
