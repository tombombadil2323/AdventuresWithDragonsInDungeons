﻿using System;
using System.Collections.Generic;

namespace ADND
{
    public class MapMovement
    {
		private IMapNavigator gameMapNavigator;
		private ICharacters player;

		private IMessageChannel message;
        private IList<string> movementOptions = new List<string> { "North", "East", "South", "West", "Up", "Down" };
		private IMapMovement moveDirection;
        private int movementOptionID=-1;


		public MapMovement()
        {
            message = new MessageConsole();
        }
		public void AsksNextStep(IMapNavigator gameMap, ICharacters character)
		{
			player = character;
			gameMapNavigator = gameMap;

            DisplayOptions();
			
            message.MessagePush("Where do you want to go from here?");
            int.TryParse(message.MessagePull(), out movementOptionID);

            ProcessOptionAnswer();
		}

        private void ProcessOptionAnswer()
        {
			switch (movementOptionID)
			{
				case 0:
					moveDirection = new GoNorth(gameMapNavigator, player);
					moveDirection.Move();

					break;

				case 1:
					moveDirection = new GoEast(gameMapNavigator, player);
					moveDirection.Move();
					break;
				
				case 2:
					moveDirection = new GoSouth(gameMapNavigator, player);
					moveDirection.Move();
					break;

				case 3:
					moveDirection = new GoWest(gameMapNavigator, player);
					moveDirection.Move();
					break;

				case 4:
					moveDirection = new GoUp(gameMapNavigator, player);
					moveDirection.Move();
					break;

				case 5:
					moveDirection = new GoDown(gameMapNavigator, player);
					moveDirection.Move();
					break;

				case 6:
					moveDirection = new GoRecuperate(gameMapNavigator, player);
					moveDirection.Move();
					break;

			}
        }

		private void DisplayOptions()
		{
			foreach (string moveOption in movementOptions)
			{
				message.MessagePush(string.Format("{0}{1}", movementOptions.IndexOf(moveOption), moveOption));
			}
		}
    }
}
