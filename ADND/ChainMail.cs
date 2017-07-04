using System;
namespace ADND
{
    public class ChainMail : IArmor
    {
		public string name { get; }
		public int armorClass { get; }
		public int initiativeFactor { get; }

        public ChainMail()
        {
            name = "Chainmail";
            armorClass = 5;
            initiativeFactor = 5;
        }
    }
}
