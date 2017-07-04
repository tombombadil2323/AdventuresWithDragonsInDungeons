using NUnit.Framework;
using System;
namespace ADND.test
{
    [TestFixture()]
    public class CharacterGeneratorTest
    {
        ICharacterGenerator sut = new CharacterGenerator();

        [TestFixtureSetUp]
        public void TestSetup()
        {
        }

        [Test()]
        public void ShouldGenerateAttributesWithinRange9To18()
        {
            ICharacters characterA = new MockCharacter();

            sut.CharacterGeneratorEngine(characterA);

            Assert.Greater(characterA.strength, 8);
            Assert.Less(characterA.strength, 19);
            Assert.Greater(characterA.constitution, 8);
            Assert.Less(characterA.constitution, 19);

        }

        [Test()]
        public void ShouldGenerateUniqueRandoms()
        {
            ICharacters characterA = new MockCharacter();
            ICharacters characterB = new MockCharacter();

            sut.CharacterGeneratorEngine(characterA);
            sut.CharacterGeneratorEngine(characterB);

            Assert.AreNotEqual(characterA.strength, characterA.dexterity);
            Assert.AreNotEqual(characterA.strength, characterB.strength);

        }

		[Test()]
		public void ShouldReturnGeneratedCharacter()
		{
			ICharacters mockCharacter = new MockCharacter();

			sut.CharacterGeneratorEngine(mockCharacter);

			Assert.Greater(mockCharacter.strength, 8, "Failure");
			Assert.Greater(mockCharacter.hitpoints, 0, "Failure");
		}


		[Test()]
		public void ShouldReturnChangedThac0()
		{
			ICharacters mockCharacter = new MockCharacter();
           
            int beforeGeneration = mockCharacter.toHitAC0;
            int afterGeneration=0;
            while (afterGeneration == 0)
            {
				sut.CharacterGeneratorEngine(mockCharacter);
				if (mockCharacter.strength > 14)
				{
					afterGeneration = mockCharacter.toHitAC0;
				}
			}
			
			Assert.AreNotEqual(beforeGeneration, afterGeneration);
		}
		[Test()]
		public void ShouldReturnChangedBaseArmorClass()
		{
			ICharacters mockCharacter = new MockCharacter();
            //mockCharacter.baseArmorClass
            sut.CharacterGeneratorEngine(mockCharacter);

		
		}

		[Test()]
		public void ShouldBeHitpointsRange1To13()
		{
			ICharacters mockCharacter = new MockCharacter();
            mockCharacter.level = 1;

            sut.CharacterGeneratorEngine(mockCharacter);

            Assert.Greater(mockCharacter.hitpoints,0);
            Assert.AreEqual(mockCharacter.hitpoints,mockCharacter.maxHitpoints);
		
		}


		[TestFixtureTearDown]
        public void TestTearDown()
        {
            sut = null;
        }

        public class MockBonus : IBonus
        {
            private const int strengthBonusMinimum = 13;
            private const int dexterityBonusMinimum = 11;
            private const int constitutionBonusMinimum = 15;
            private ICharacters characterBonusCheck;

            public MockBonus(ICharacters character)
            {
                characterBonusCheck = character;
            }

            public void BonusAdjustment()
            {
                CheckStrengthBonus();
                CheckDexterityBonus();
                CheckConstitutionBonus();
            }

            public void CheckStrengthBonus()
            {
                int adjustment = characterBonusCheck.strength - strengthBonusMinimum;
                if (adjustment > 0)
                {
                    characterBonusCheck.toHitAC0Bonus = adjustment;
                    characterBonusCheck.damageBonus = adjustment;
                }
                else
                {
                    characterBonusCheck.toHitAC0Bonus = 0;
                    characterBonusCheck.damageBonus = 0;
                }

            }

            public void CheckDexterityBonus()
            {
                int adjustment = characterBonusCheck.dexterity - dexterityBonusMinimum;
                if (adjustment > 0)
                {
                    characterBonusCheck.initiativeBonus += adjustment;
                    characterBonusCheck.baseArmorClass -= adjustment;
                }
                else
                {
                    characterBonusCheck.initiativeBonus = 0;
                }
            }

            public void CheckConstitutionBonus()
            {
                int adjustment = characterBonusCheck.constitution - constitutionBonusMinimum;

                if (adjustment > 0)
                {
                    characterBonusCheck.hitPointBonus += adjustment;
                    characterBonusCheck.maxHitpoints += characterBonusCheck.hitPointBonus;
                }
                else
                {
                    characterBonusCheck.hitPointBonus = 0;
                }

            }

        }
    }
}


