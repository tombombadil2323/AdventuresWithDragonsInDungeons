using System;
namespace ADND
{
	public class LeatherArmor : IArmor
	{
		public string name { get; }
		public int armorClass { get; }
		public int initiativeFactor { get; }

		public LeatherArmor()
		{
			name = "Leather Armor";
			armorClass = 7;
			initiativeFactor = 3;
		}
	}
}