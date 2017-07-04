using System;
using System.Collections.Generic;
using System.Reflection;

namespace ADND
{
    public interface IEncounter
    {
        void EncounterFlow(IList<ICharacters> characterList);
    }

}
