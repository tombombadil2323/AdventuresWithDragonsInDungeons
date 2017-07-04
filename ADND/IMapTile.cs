using System;
namespace ADND
{
    public interface IMapTile
    {
		int positionX { get; set; }
		int positionY { get; set; }
		int positionZ { get; set; }
		string description { get; set; }
        bool IsCurrent { get; set; }
		bool IsLast { get; set; }
        bool HadEncounter { get; set; }

        void GenerateEncounterType();
        void ExecuteEncounterType();
       
    }
}
