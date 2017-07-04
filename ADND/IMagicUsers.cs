using System;
using System.Collections.Generic;

namespace ADND
{
    public interface IMagicUsers
    {
        IList<ISpells> spellBook { get; set; }
        bool wasInterruptedWhileCasting { get; set; }
    }
}