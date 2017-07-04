using System;
namespace ADND

{
    public class Fighter : CharacterTemplate, ICharacters
    {
		public string characterClassName { get; }
        public int hitPointFactor { get; }
        public IWeapon weapon { get; set; }
        public IArmor armor { get; set; }
        public int level { get; set; }
        public int toHitAC0 {get; set;}
		public string characterTypeName { get; }


    public Fighter()
        {
            characterTypeName = "Player";
			characterClassName = "Fighter";

            hitPointFactor = 11;

            weapon = new Sword();
            armor = new ChainMail();
            level = 5;

            toHitAC0 = 0;
            toHitAC0 -= level - 1;
        }
    }
}
