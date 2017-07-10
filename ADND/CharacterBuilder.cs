using System;
using System.Collections.Generic;

namespace ADND
{
    //funktioniert noch nicht - falsche auswahl von player types.. 
    //auch Thaco berechnung überprüfen

    public class CharacterBuilder
    {
        ICharacters characterChosen;
		IMessageChannel message = new MessageConsole();

        public ICharacters CharacterBuilderExecution()
        {
            CharacterClassChosing();
            GenerateStats();
            DisplayCharacterSheet();
            return characterChosen;
        }

        private void CharacterClassChosing()
        {
            message.MessagePush("Please chose a Character Class: (F)ighter or (W)izard");

			foreach (int value in Enum.GetValues(typeof(PlayerCharacters)))
			{
				string optionName = Enum.GetName(typeof(PlayerCharacters), value);
				message.MessagePush(string.Format("[{0}] {1}", value, optionName));
			}

            int.TryParse(message.MessagePull(), out int classChosenEnumID);
            if(classChosenEnumID == 0)
            {
				characterChosen = new Fighter();
            }
			if (classChosenEnumID == 1)
			{
                characterChosen = new Wizard();
			}
        }

        public void DisplayCharacterSheet()
        {
            ICharacterSheetParser sheetParser = new CharacterSheetParser();

            IList<string> sheetList = sheetParser.ParseSheet(characterChosen);

            IWriteSheet writeConsole = new WriteSheetConsole();

            foreach (string sheet in sheetList)
            {
                message.MessagePush(sheet);
            }
        }

        //for players
		public void GenerateStats()
		{
			ICharacterGenerator generator = new CharacterGenerator();
			generator.CharacterGeneratorEngine(characterChosen);

            DisplayCharacterSheet();

			message.MessagePush("Do you want to keep these stats?");

			foreach (int value in Enum.GetValues(typeof(OptionYesNo)))
			{
                string optionName = Enum.GetName(typeof(OptionYesNo), value);
				message.MessagePush(string.Format("[{0}] {1}", value, optionName));
			}

            int.TryParse(message.MessagePull(), out int keepStatOption);

            if(keepStatOption == 0)
            {
                AskForName();
            }
			if (keepStatOption == 1)
			{
                GenerateStats();
            }
		}

        //for monsters
		public void GenerateStats(ICharacters monster)
		{
			ICharacterGenerator generator = new CharacterGenerator();
			generator.CharacterGeneratorEngine(monster);
			
		}

        private void AskForName()
        {
			message.MessagePush("What name should your Character have?");
            characterChosen.name = message.MessagePull();
        }
    }
}
