using System;
namespace ADND
{
    public class HideArmor : IArmor
    {
		public string name { get; }
		public int armorClass { get; }
		public int initiativeFactor { get; }

        public HideArmor()
        {
            name = "Hide";
            armorClass = 2;
            initiativeFactor = 1;
        }
    }
}
