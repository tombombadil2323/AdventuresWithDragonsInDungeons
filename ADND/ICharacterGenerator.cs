using System;
namespace ADND
{
    public interface ICharacterGenerator
    {
        ICharacters CharacterGeneratorEngine(ICharacters character);
    }
}
