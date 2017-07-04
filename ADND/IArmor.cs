using System;
namespace ADND
{
    public interface IArmor
    {
        string name { get; }
        int armorClass { get; }
        int initiativeFactor { get; }
    }
}
