namespace ADND
{
    internal class FirstTile : IMapTile
    {
		public int positionX { get; set; }
		public int positionY { get; set; }
		public int positionZ { get; set; }
		public string description { get; set; }
		public bool IsCurrent { get; set; }
		public bool IsLast { get; set; }
        private ICharacters player;
        public bool HadEncounter { get; set; }

        public FirstTile(ICharacters character)
        {
            player = character;

			positionX = 0;
			positionY = 0;
			positionZ = -2;
			description = "You were wandering " +
				"through the deep dark wood looking for mushrooms. " +
				"Suddendly the earth beneath your feet gives way and you fall into darkness!" +
				"You scramble to your feet, checking that you have not hurt yourself. " +
				"Then your eyes adjust to the darkness and you see that you are in a small " +
				"hallway of some sort of a dungeon and the hole in the roof is too high to reach." +
				"You will need to look for another exit. Strangely the hallway is dimly lit by the " +
				"fluourescent moss that seems to be very common down here.";
			IsCurrent = true;

            ExecuteEncounterType();
        }

        public void GenerateEncounterType()
        {
        }

		public void ExecuteEncounterType()
		{
			IMessageChannel message = new MessageConsole();

			message.MessagePush(description);
		}
    }
}