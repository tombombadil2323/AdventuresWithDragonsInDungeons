using System;
namespace ADND
{
    public class MonsterGenerator : IMonsterGenerator
    {
        private ICharacters monster;
        private string monsterType;
        private Random randomDice = new Random();

        public ICharacters GenerateMonsterType()
        {
            int sizeOfMonsterList = sizeof(MonsterCharacters);
            int randomMonsterID = randomDice.Next(0, sizeOfMonsterList);

            monsterType = Enum.GetName(typeof(MonsterCharacters), randomMonsterID);

            CheckForGoblin();
            CheckForOrc();
            CheckForGiant();
            CheckForDragon();
            CheckForSpider();
            return monster;
        }

        private void CheckForGoblin()
        {
            if (monsterType == "Goblin")
            {
                monster = new Goblin();
            }
        }

        private void CheckForOrc()
        {
            if (monsterType == "Orc")
            {
                monster = new Orc();
            }
        }
        private void CheckForGiant()
        {
            if (monsterType == "Giant")
            {
                monster = new Giant();
            }
        }
        private void CheckForDragon()
        {
            if (monsterType == "Dragon")
            {
                monster = new Dragon();
            }
        }
        private void CheckForSpider()
        {
            if (monsterType == "Spider")
            {
                monster = new Spider();
            }
        }
    }
}
