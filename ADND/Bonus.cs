using System;
namespace ADND
{
    public class Bonus : IBonus
    {
        private const int strengthBonusMinimum = 13;
        private const int dexterityBonusMinimum = 11;
        private const int constitutionBonusMinimum = 15;
        private ICharacters characterBonusCheck;

        public Bonus(ICharacters character)
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
            int adjustment = characterBonusCheck.strength-strengthBonusMinimum;
            if (adjustment>0)
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
			if (adjustment>0)
			{
                characterBonusCheck.initiativeBonus += adjustment;
                characterBonusCheck.baseArmorClass -= adjustment;
			}
            else
            {
                characterBonusCheck.initiativeBonus = 0;
            }
        }

        public void CheckConstitutionBonus ()
        {
            int adjustment = characterBonusCheck.constitution - constitutionBonusMinimum;

			if (adjustment>0)
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
