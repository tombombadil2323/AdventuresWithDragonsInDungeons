using System;
namespace ADND
{
    public class Bonus : IBonus
    {
        private const int strengthBonusMinimum = 13;
        private const int dexterityBonusMinimum = 11;
        private const int constitutionBonusMinimum = 15;
        private ICharacters characterCheckedForBonus;

        public Bonus(ICharacters character)
        {
            characterCheckedForBonus = character;
        }

        public void BonusAdjustment()
        {
            CheckStrengthBonus();
            CheckDexterityBonus();
            CheckConstitutionBonus();
        }

        public void CheckStrengthBonus()
        {
            int adjustment = characterCheckedForBonus.strength-strengthBonusMinimum;
            if (adjustment>0)
            {
                characterCheckedForBonus.toHitAC0Bonus = adjustment;
                characterCheckedForBonus.damageBonus = adjustment; 
            }
            else
            {
                characterCheckedForBonus.toHitAC0Bonus = 0;
                characterCheckedForBonus.damageBonus = 0;
            }

        }

        public void CheckDexterityBonus()
        {
            int adjustment = characterCheckedForBonus.dexterity - dexterityBonusMinimum;
			if (adjustment>0)
			{
                characterCheckedForBonus.initiativeBonus += adjustment;
                characterCheckedForBonus.baseArmorClass -= adjustment;
			}
            else
            {
                characterCheckedForBonus.initiativeBonus = 0;
            }
        }

        public void CheckConstitutionBonus ()
        {
            int adjustment = characterCheckedForBonus.constitution - constitutionBonusMinimum;

			if (adjustment>0)
			{
				characterCheckedForBonus.hitPointBonus += adjustment;
                characterCheckedForBonus.maxHitpoints += characterCheckedForBonus.hitPointBonus;
			}
            else
            {
                characterCheckedForBonus.hitPointBonus = 0;
            }

        }

    }
}
