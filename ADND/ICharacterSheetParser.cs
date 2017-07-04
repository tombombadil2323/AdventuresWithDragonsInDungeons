using System;
using System.Collections.Generic;

namespace ADND
{
    public interface ICharacterSheetParser
    {
        IList<string> ParseSheet(ICharacters character);

    }
}
