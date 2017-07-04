using System;
namespace ADND
{
    public class MagicMissile : ISpells
    {
		public int duration { get; }
		public int initiative { get; }
        public int damage { get; set; }
		public string name { get; }
		public int descriptionOfEffect { get; }
        public int spellLevel { get; }
        public int damageFactor { get; }
        Random r;

        public MagicMissile()
        {
            duration = 1;
            initiative = 1;
            spellLevel = 1;
            damageFactor = 4;
            name = "Magic Missile";
            r = new Random();
        }


		public int CastSpell(ICharacters wizard)
        {
            damage = 0;
            int numberOfMissiles = wizard.level;
            for (int i = 0; i < numberOfMissiles; i++)
            {
                damage += r.Next(1, damageFactor);
            }
            return damage;
        }
    }
}
