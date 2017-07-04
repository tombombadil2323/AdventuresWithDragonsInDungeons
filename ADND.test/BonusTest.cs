using NUnit.Framework;
using System;

//refactored 25.7.17
namespace ADND.test
{
    [TestFixture()]
    public class BonusTest
    {
        [Test()]
        public void ShouldBeDamageBonusResultAndThac0AdjustmentOfFive()
        {
            ICharacters mockCharacter = new MockCharacter();
            Bonus bonus = new Bonus(mockCharacter);

            mockCharacter.strength = 18;
            bonus.CheckStrengthBonus();
			
            Assert.AreEqual(5,mockCharacter.damageBonus);
            Assert.AreEqual(5,mockCharacter.toHitAC0Bonus);
        }

        [Test()]
		public void ShouldBeInitiativeFactorResultOfSevenAndBaseArmorClassOfThree()
		{
			ICharacters mockCharacter = new MockCharacter();
			Bonus bonus = new Bonus(mockCharacter);

            mockCharacter.dexterity = 18;
			bonus.CheckDexterityBonus();

			Assert.AreEqual(7, mockCharacter.initiativeBonus);
			Assert.AreEqual(3, mockCharacter.baseArmorClass);
		}

		[Test()]
		public void ShouldBeHitPointAdjustmentOf()
		{
			ICharacters mockCharacter = new MockCharacter();
			Bonus bonus = new Bonus(mockCharacter);

			mockCharacter.constitution = 18;
			bonus.CheckConstitutionBonus();

			Assert.AreEqual(3, mockCharacter.hitPointBonus);
		}
		
    }
}
