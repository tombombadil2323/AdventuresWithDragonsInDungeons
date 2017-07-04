using System;
namespace ADND
{
    public class Sword : IWeapon
    {
		public int initiativeFactor { get; }
		public int damage { get; }
		public string name { get; }
		public bool isMagic { get; }

        public Sword()
        {
            initiativeFactor = 4;
            damage = 11;
            name = "Sword";
            isMagic = false;
        }
    }
}
