using System;
using System.Collections.Generic;

namespace ADND
{
    public class Wizard: CharacterTemplate, ICharacters, IMagicUsers
    {
		public int hitPointFactor { get; }
		public string characterClassName { get; }
		public IWeapon weapon { get; set; }
		public IArmor armor { get; set; }
        public IList<ISpells> spellBook { get; set; }
        public int level { get; set; }
        public int toHitAC0 { get; set; }
		public string characterTypeName { get; }
        public bool wasInterruptedWhileCasting { get; set; }

		public Wizard()
		{
            characterTypeName = "Player";
			hitPointFactor = 5;
			characterClassName = "Wizard";
			weapon = new Dagger();
			armor = new Robe();
            level = 5;
            toHitAC0 = 20;
            toHitAC0 -= 20 - level / 3;

            ISpells spell = new MagicMissile();
            spellBook = new List<ISpells>();
            spellBook.Add(spell);

            wasInterruptedWhileCasting = false;
		}
    }
}
