using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ADND.test
{
    [TestFixture()]
    public class CombatEncounterTest
    {
        [Test()]
        public void ShouldBeSortedCharacterDictionary2()
        {
            ICharacterGenerator generator = new CharacterGenerator();
            IList<int> sortedList = new List<int>();

            ICharacters fighter = generator.CharacterGeneratorEngine(new Fighter());
            ICharacters monster = generator.CharacterGeneratorEngine(new Goblin());
            IList<ICharacters> combatantList = new List<ICharacters> { fighter, monster };

            Assert.AreEqual(2, combatantList.Count);

            CombatEncounter sut = new CombatEncounter(combatantList);
            sut.CreateSortedInitiativeListFromSortedCharacterDirectory();
           // Assert.AreEqual(2, sut.sortedCharacterDictionary.Count);
            foreach(int key in sut.sortedCharacterDictionary.Keys)
            {
                sortedList.Add(key);
            }
            if (sortedList.Count>1)
            {
				Assert.Less(sortedList[0], sortedList[1]);
			}
        }


    }
}
