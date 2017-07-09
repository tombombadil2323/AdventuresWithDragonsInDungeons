using System;
using System.Collections.Generic;

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

        public bool HadEncounter { get; set; }

        public FirstTile()
        {
			positionX = 0;
			positionY = 0;
			positionZ = 0;
            description = string.Format("You were wandering through the deep dark wood looking for mushrooms. " +
                                        "Suddendly the earth beneath your feet gives way and you fall into darkness!" +
                                        "You scramble to your feet, checking that you have not hurt yourself. "+
	                        			"Then your eyes adjust to the darkness and you see that you are in a small " +
	                        			"hallway of some sort of a dungeon and the hole in the roof is too high to reach." +
	                        			"You will need to look for another exit. Strangely the hallway is dimly lit by the " +
		                        		"fluourescent moss that seems to be very common down here. The hallway leads to a cavern " +
                                         "with an entrance that seems like a fanged maw. The top is jagged and there are rising, cones " +
                                        "of stone below. The cavern is 40m wide, 70m long, and over 20m high in the central area. " +
                                        "It has obviously been used much in the past. The walls and ceiling are blackened by soot, " +
                                        "and there are bits of broken furniture and discarded gear scattered around.");
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