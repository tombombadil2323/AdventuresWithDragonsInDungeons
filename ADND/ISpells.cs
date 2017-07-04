using System;
namespace ADND
{
    public interface ISpells
    {
        int duration { get; }
        int initiative { get; }
        int damage { get; }
        string name { get; }
        int descriptionOfEffect { get; }

        int CastSpell(ICharacters character);
    }
}
