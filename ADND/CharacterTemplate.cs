using System;
namespace ADND
{
    public abstract class CharacterTemplate
    {
		public string name { get; set; }

		public int strength { get; set; }
		public int dexterity { get; set; }
		public int constitution { get; set; }
        public int intelligence { get; set; }
        public int charisma { get; set; }
        public int wisdom { get; set; }

		public int maxHitpoints { get; set; }
		public int hitpoints { get; set; }

		public int baseArmorClass { get; set; }
		public int armorClass { get; set; }

		public int damageBonus { get; set; }
		public int initiativeBonus { get; set; }
		public int toHitAC0Bonus { get; set; }
		public int hitPointBonus { get; set; }

		public int gold { get; set; }
		public int xp { get; set; }

        public IAction currentAction { get; set; }
        public bool isDead { get; set; }

		public CharacterTemplate()
		{
            name = "";

            strength = 0;
            dexterity = 0;
            constitution = 0;
            intelligence = 0;
            charisma = 0;
            wisdom = 0;

            maxHitpoints = 0;
            hitpoints = 0;

            baseArmorClass = 10;
            armorClass = 10;

            damageBonus = 0;
            initiativeBonus = 0;
            toHitAC0Bonus = 0;
            hitPointBonus = 0;

            xp = 0;
            gold = 0;

            isDead = false;
		}
    }

}
