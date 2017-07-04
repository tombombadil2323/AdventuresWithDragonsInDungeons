using System;
using System.Collections.Generic;

namespace ADND
{
    public class CharacterSheetParser :ICharacterSheetParser
    {
        private IList<string> sheet = new List<string>();


        public IList<string> ParseSheet(ICharacters character)
        {
            sheet.Add("Name: " + character.name);
            sheet.Add("Character Class: " + character.characterClassName);
            sheet.Add("Character Level: " + character.level);
            sheet.Add("---------------------");
            sheet.Add("Strength: " + character.strength);
            sheet.Add("Dexterity: " + character.dexterity);
            sheet.Add("Constitution: " + character.constitution);
            sheet.Add("Intelligence: " + character.intelligence);
            sheet.Add("Wisdom: " + character.wisdom);
            sheet.Add("Charisma: " + character.charisma);
            sheet.Add("---------------------");
            sheet.Add("Max. Hitpoints: " + character.maxHitpoints);
            sheet.Add("Current Hitpoints: " + character.hitpoints);
            sheet.Add("---------------------");
            sheet.Add("Armor: "+character.armor.name);
            sheet.Add(("Armor Class of Armor: " +character.armor.armorClass));
            sheet.Add("Armor Class: " + character.armorClass);
            sheet.Add("Armor Initiative Malus: " + character.armor.initiativeFactor);
            sheet.Add("Base Armor Class: "+character.baseArmorClass);
            sheet.Add("---------------------");
            sheet.Add("Weapon: "+ character.weapon.name);
            sheet.Add("Weapon Damage: "+ character.weapon.damage);
            sheet.Add("Weapon Initiative Malus: " + character.weapon.initiativeFactor);

            return sheet;
        }
    
    }

}
