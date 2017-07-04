using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ADND.test
{
    [TestFixture()]
    public class CharacterSheetParserTest
    {
        [Test()]
        public void ShouldBeFilledSheet()
        {
            ICharacters character = new MockCharacter();
			character.name = "TestDummy";
			character.strength = 18;
			character.dexterity = 18;
			character.constitution = 18;
			character.intelligence = 18;
			character.wisdom = 18;
			character.charisma = 18;
			character.hitpoints = 10;
			character.maxHitpoints = 11;
			character.toHitAC0 = 16;
			character.damageBonus = 4;
			character.baseArmorClass = 10;
			character.armor = new MockArmor();
			character.weapon = new MockWeapon();

            ICharacterSheetParser parserWriter = new CharacterSheetParser();

            IList<string> characterSheet = parserWriter.ParseSheet(character);
            CollectionAssert.AllItemsAreNotNull(characterSheet);
            IWriteSheet consoleWriter = new WriteSheetConsole();
            consoleWriter.WriteSheet(characterSheet);

        }


        public class MockArmor : IArmor
        {
            public string name { get; }
            public int armorClass { get; }
            public int initiativeFactor { get; }

            public MockArmor()
            {
                name = "MockChainmail";
                armorClass = 5;
                initiativeFactor = 4;
            }

        }
        public class MockWeapon : IWeapon
		{
			public string name { get; }
			public int damage { get; }
			public int initiativeFactor { get; }
            public bool isMagic { get; }

			public MockWeapon()
			{
				 name = "MockSword";
				 initiativeFactor = 5;
				 damage = 6;
                 isMagic = false;
			}

		}
    }
}



