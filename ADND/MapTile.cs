using System;
using System.Collections.Generic;

namespace ADND
{
    public class MapTile : IMapTile
    {
        public int positionX { get; set; }
        public int positionY { get; set; }
        public int positionZ { get; set; }
        public string description { get; set; }
        public bool IsCurrent { get; set; }
        public bool IsLast { get; set; }
        public enum State {visited, current}
		public IList<IEncounterType> encounterType { get; set; }
		public bool HadEncounter { get; set; }

        private IEncounterType selectedEncounterType;
        private IList<ICharacters>  playerCharacterList;

        public MapTile(IList<ICharacters> partyList)
        {
            playerCharacterList = partyList;
            positionX = 0;
            positionY = 0;
            positionZ = 0;
            IsCurrent = false;
            IsLast = false;

            //TODO: initialise encounterType List
        }
        public void GenerateEncounterType()
        {
            //select encounter type, e.g. maptile
            selectedEncounterType = new CombatMapTileEncounter(playerCharacterList);
            selectedEncounterType.GenerateEncounter();
        }

		public void ExecuteEncounterType()
		{
			//select encounter type, e.g. CombatEncounter

			selectedEncounterType.ExecuteEncounter(this);
            HadEncounter = true;
		}
    }
}
