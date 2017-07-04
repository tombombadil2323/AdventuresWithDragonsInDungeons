using System;
namespace ADND
{
    public class Robe : IArmor
    {
        public string name { get; }
		public int armorClass { get; }
		public int initiativeFactor { get; }

		public Robe()
		{
			name = "Robe";
			armorClass = 10;
			initiativeFactor = 0;
		}
    }
}
