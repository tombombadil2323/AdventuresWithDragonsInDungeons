using System;
namespace ADND.test
{
public class MockCharacter : CharacterTemplate, ICharacters
	{
		public int level { get; set; }
		public int toHitAC0 { get; set; }

		public IWeapon weapon { get; set; }
		public IArmor armor { get; set; }

		public int hitPointFactor { get; }
		public string characterClassName { get; }
		public string characterTypeName { get; }

		public MockCharacter()
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
