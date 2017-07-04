using System;
namespace ADND
{
    public class Dagger : IWeapon
    {

		public int initiativeFactor { get; }
		public int damage { get; }
		public string name { get; }
		public bool isMagic { get; }

		public Dagger()
		{
			initiativeFactor = 1;
			damage = 5;
			name = "Dagger";
			isMagic = false;
		}

    }
}
