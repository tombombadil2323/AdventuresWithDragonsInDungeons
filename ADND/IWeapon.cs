using System;
namespace ADND
{
    public interface IWeapon
    {
        int initiativeFactor { get;}
        int damage { get; }
        string name { get; }
        bool isMagic { get; }
    }
}
