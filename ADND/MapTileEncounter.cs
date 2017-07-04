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
        private ICharacters player;

        public MapTileEncounter(ICharacters character)
        {
            player = character;
        }

		public void GenerateEncounter()
		{
	        //select random encounter possible for maptile e.g. combat
		}

		public void ExecuteEncounter(IMapTile mapTile)
        {
            //currently hard coded to combat - todo: link to GenerateEncounter()
            IEncounter encounter = new CombatEncounter();
            IList<ICharacters> characterList = new List<ICharacters>();
            IMonsterGenerator monsterGenerator = new MonsterGenerator();
            RandomSingleton randomGenerator = RandomSingleton.Instance();
            ICharacters monster;

            characterList.Add(player);

            //only works for dungeons with negative levels down
     		int maxMonsters = randomGenerator.Next(1,((int)Math.Abs(mapTile.positionZ))+3);

		for (int i = 0; i < maxMonsters; i++)
            {
				monster = monsterGenerator.GenerateMonsterType();
				characterList.Add(monster);
            }
            encounter.EncounterFlow(characterList);
        }
    }
}
