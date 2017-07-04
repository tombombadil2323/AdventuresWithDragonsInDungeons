//test
using System;
namespace ADND
{
    public class BiteWeapon :IWeapon
	{
		public int initiativeFactor { get; }
		public int damage { get; }
		public string name { get; }
		public bool isMagic { get; }

		public BiteWeapon()
		{
			initiativeFactor = 1;
			damage = 5;
			name = "Bite";
			isMagic = false;
		}
    }
}
