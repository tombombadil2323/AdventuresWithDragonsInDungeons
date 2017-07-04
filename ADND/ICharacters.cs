using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace ADND
{
    public interface ICharacters
    {
	//inherited from CharacterTemplate
	 string name { get; set; }
	 int strength { get; set; }
	 int dexterity { get; set; }
	 int constitution { get; set; }
	 int intelligence { get; set; }
	 int charisma { get; set; }
	 int wisdom { get; set; }
	 int maxHitpoints { get; set; }
	 int hitpoints { get; set; }
	 int baseArmorClass { get; set; }
	 int armorClass { get; set; }
	 int toHitAC0Bonus { get; set; }
	 int damageBonus { get; set; }
	 int initiativeBonus { get; set; }
	 int hitPointBonus { get; set; }
	 int gold { get; set; }
	 int xp { get; set; }
	 IAction currentAction { get; set; }
	 bool isDead { get; set; }


	//implemented by Player and Monster Classes
	string characterTypeName { get; }
	string characterClassName { get; }
	int hitPointFactor { get; }
	IWeapon weapon { get; set; }
	IArmor armor  { get; set; }
	int level { get; set; }
	int toHitAC0 { get; set; }
    }
}
