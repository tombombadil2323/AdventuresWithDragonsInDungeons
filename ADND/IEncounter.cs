using System.Collections.Generic;


namespace ADND
{
    public interface IEncounter
    {
        void EncounterFlow(IList<ICharacters> characterList);
    }

}
