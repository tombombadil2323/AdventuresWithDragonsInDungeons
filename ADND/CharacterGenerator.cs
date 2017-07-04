using System;
namespace ADND
{
    public class CharacterGenerator : ICharacterGenerator
    {
        private IMessageChannel messageChannelConsole = new MessageConsole();
        private Random randomDice = new Random();
        private IBonus bonus;
        private ICharacters characterToBeGenerated;

        public ICharacters CharacterGeneratorEngine(ICharacters character)
        {
            characterToBeGenerated = character;

            GenerateStats();
            GenerateBonus();
            GenerateHitPoints();

            return characterToBeGenerated;

        }
      
        private void GenerateStats()
        {
			characterToBeGenerated.strength = randomDice.Next(9, 19);
			characterToBeGenerated.dexterity = randomDice.Next(9, 19);
			characterToBeGenerated.constitution = randomDice.Next(9, 19);
			characterToBeGenerated.intelligence = randomDice.Next(9, 19);
			characterToBeGenerated.wisdom = randomDice.Next(9, 19);
			characterToBeGenerated.charisma = randomDice.Next(9, 19);
        }

        private void GenerateBonus()
        {
			bonus = new Bonus(characterToBeGenerated);
			bonus.BonusAdjustment();
            characterToBeGenerated.toHitAC0 -= characterToBeGenerated.toHitAC0Bonus;
            int armorToArmorClassTranslation = 10 - characterToBeGenerated.armor.armorClass;
            characterToBeGenerated.baseArmorClass -= armorToArmorClassTranslation;
			characterToBeGenerated.armorClass = characterToBeGenerated.baseArmorClass;

		}

        private void GenerateHitPoints()
        {
		int numberOfHitPointDice = characterToBeGenerated.level;
            for (int i = 0; i<numberOfHitPointDice; i++)
            {
                characterToBeGenerated.maxHitpoints += randomDice.Next(1, characterToBeGenerated.hitPointFactor);
                characterToBeGenerated.maxHitpoints += characterToBeGenerated.hitPointBonus;
            }

         characterToBeGenerated.hitpoints = characterToBeGenerated.maxHitpoints;
        }
    }
}
