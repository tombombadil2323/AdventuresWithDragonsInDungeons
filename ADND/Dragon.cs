using System;
using System.Collections.Generic;
namespace ADND
{
	public class Dragon : CharacterTemplate, ICharacters, IMagicUsers
	{
		public int level { get; set; }
		public int toHitAC0 { get; set; }

		public IWeapon weapon { get; set; }
		public IArmor armor { get; set; }

		public int hitPointFactor { get; }
		public string characterClassName { get; }
        public string characterTypeName { get; }

        public IList<ISpells> spellBook { get; set; }
        public bool wasInterruptedWhileCasting { get; set; }


		public Dragon()
		{
            characterTypeName = "Monster";
            characterClassName = "Dragon";
			name = "Dragon";
			level = 10;
			weapon = new Sword();
			armor = new ChainMail();
			hitPointFactor = 11;
            baseArmorClass = 10;
			toHitAC0 = 20 - level/2;

			ISpells spell = new MagicMissile();
			spellBook = new List<ISpells>();
			spellBook.Add(spell);
			wasInterruptedWhileCasting = false;

            isDead = false;

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
