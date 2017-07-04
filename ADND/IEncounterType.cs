using System;
namespace ADND
{
    public interface IEncounterType
    {
        string encounterType { get; set; }
        string encounterStartDescription { get; set; }
        string encounterEndingDescription { get; set; }
        int goldWon { get; set; }
        int xpGained { get; set; }

        void GenerateEncounter();

        void ExecuteEncounter(IMapTile mapTile);

    }
}
