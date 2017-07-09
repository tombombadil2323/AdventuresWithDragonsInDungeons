using System;
using System.Collections.Generic;

namespace ADND
{
    public class MapTileEncounter : IEncounterType
    {
        public string encounterStartDescription { get; set; }
        public string encounterEndingDescription { get; set; }
        public int goldWon { get; set; }
        public int xpGained { get; set; }
		public string encounterType { get; set; }
        private IList<ICharacters>  playerCharacterList;
		private IEncounter encounter;
        private IList<ICharacters> encounterCharacterList;
		private RandomSingleton randomGenerator = RandomSingleton.Instance();
		private IMonsterGenerator monsterGenerator;
		private ICharacters monster;

        public MapTileEncounter(IList<ICharacters> partyList)
        {
            playerCharacterList = partyList;
        }

		public void GenerateEncounter()
		{
	        //select random encounter possible for maptile e.g. combat
		}

		public void ExecuteEncounter(IMapTile mapTile)
        {
            //currently hard coded to combat - todo: link to GenerateEncounter()
            encounter = new CombatEncounter();
            encounterCharacterList = new List<ICharacters>();

            foreach(ICharacters playerCharacters in playerCharacterList)
            {
				encounterCharacterList.Add(playerCharacters);
            }

            monsterGenerator = new MonsterGenerator();
     		int maxMonsters = randomGenerator.Next(1,((int)Math.Abs(mapTile.positionZ))+3);

			for (int i = 0; i < maxMonsters; i++)
	            {
					monster = monsterGenerator.GenerateMonsterType();
					encounterCharacterList.Add(monster);
	            }
	            encounter.EncounterFlow(encounterCharacterList);
	        }
    }
}
