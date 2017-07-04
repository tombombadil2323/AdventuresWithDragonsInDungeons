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
        Random r = new Random();
        private IEncounterType selectedEncounterType;
        public bool HadEncounter { get; set; }

        public ICharacters player;

        public MapTile(ICharacters character)
        {
            positionX = 0;
            positionY = 0;
            positionZ = -2;
            IsCurrent = false;
            IsLast = false;
            player = character;

            //TODO: initialise encounterType List
        }
        public void GenerateEncounterType()
        {
            //select encounter type, e.g. maptile
            selectedEncounterType = new MapTileEncounter(player);
            selectedEncounterType.GenerateEncounter();
        }

		public void ExecuteEncounterType()
		{
			//select encounter type, e.g. maptile

			selectedEncounterType.ExecuteEncounter(this);
            HadEncounter = true;
		}
    }
}
