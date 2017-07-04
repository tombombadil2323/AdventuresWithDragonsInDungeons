using NUnit.Framework;
using System.Collections.Generic;
using System;
namespace ADND.test
{
    [TestFixture()]
    public class WizardTest
    {
        [Test()]
        public void ShouldBeNotNull()
        {
            

            for (int i = 0; i < 10;i++)
			{
                Wizard wizard = new Wizard();
                int damage;
         
				foreach (ISpells spells in wizard.spellBook)
				{

				Assert.IsNotNull(spells.name);
                Console.WriteLine(spells.name);

                ICharacters character = new MockCharacter();
				spells.CastSpell(character);
				damage = spells.damage;

                Assert.IsNotNull(damage);
				Console.WriteLine(damage);
				}
			}
           
        }
		public class MockWizard : CharacterTemplate, ICharacters, IMagicUsers 
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
  
			public MockWizard()
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
}
