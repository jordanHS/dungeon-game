using System;
using System.Collections.Generic;
using DungeonGame.Models;

namespace DungeonGame
{
  public class Program
  {
    public static void Main()
    {
      Room[] GameBoard = new Room[16];
      InitializeGame(GameBoard);
      Console.WriteLine("Welcome to the Dungeon Game!");
      Console.WriteLine("Enter your name:");
      string name = Console.ReadLine();
      Console.WriteLine("Choose a difficulty:");
      Console.WriteLine("1: Easy (25 Health)");
      Console.WriteLine("2: Medium (15 Health)");
      Console.WriteLine("3: Hard (10 Health)");
      int difficulty = int.Parse(Console.ReadLine());
      Player player = CreatePlayer(name, difficulty);
      player.PlayerPosition = GameBoard[0];
      DrawBoard(player);
      int input;
      while(player.GameActive)
      {
        Console.WriteLine("You have {0} health remaining. Each move uses 1 health.", player.PlayerLife);
        Console.WriteLine("What would you like to do?");
        Console.WriteLine("1: Move");
        Console.WriteLine("2: Use an item");
        Console.WriteLine("3: View the map");
        input = int.Parse(Console.ReadLine());
        switch(input)
        {
          case 1:
            DrawBoard(player);
            Move(player, GameBoard);
            if(player.GameActive == true)
              DrawBoard(player);
            break;
          case 2:
            UseItem(player);
            break;
          case 3:
            DrawBoard(player);
            break;
          default:
            Console.WriteLine("Invalid input.");
            break;
        }
        if(player.GameActive == true && player.PlayerLife == 0)
        {
          player.GameActive = false;
          Console.WriteLine("####################################################");
          Console.WriteLine("\\    /  ------   |     |      ----\\   |  ------  ----\\ ");
          Console.WriteLine(" \\  /   |     |  |     |      |    |  |  |       |    | ");
          Console.WriteLine("  \\/    |     |  |     |      |    |  |  +----   |    | ");
          Console.WriteLine("   |    |     |  |     |      |    |  |  |       |    | ");
          Console.WriteLine("   |    ------   -------      -----/  |  ------  -----/ ");
          Console.WriteLine("####################################################");
        }
      }
    }
    public static void Move(Player player, Room[] board)
    {
      int playerPos = player.PlayerPosition.PositionId;
      Console.WriteLine("Which direction do you want to move?");
      Console.WriteLine("1: Up");
      Console.WriteLine("2: Down");
      Console.WriteLine("3: Left");
      Console.WriteLine("4: Right");
      int input = int.Parse(Console.ReadLine());
      switch(input)
      {
        case 1:
          if(playerPos > 3)
          {
            if(player.PlayerPosition.Doors.Contains(board[player.PlayerPosition.PositionId - 4]))
            {
              player.PlayerPosition = board[player.PlayerPosition.PositionId - 4];
              if(player.PlayerPosition.Item != null)
              {
                Console.WriteLine("*************************************************************");
                Console.WriteLine("**** You found a {0}! It has been placed in your inventory ****", player.PlayerPosition.Item.ItemName);
                Console.WriteLine("*************************************************************");
                player.GrabItem();
              }
              player.PlayerLife--;
            }
          }
          break;
        case 2:
          if(playerPos < 12)
          {
            if(player.PlayerPosition.Doors.Contains(board[player.PlayerPosition.PositionId + 4]))
            {
              player.PlayerPosition = board[player.PlayerPosition.PositionId + 4];
              if(player.PlayerPosition.Item != null)
              {
                Console.WriteLine("*************************************************************");
                Console.WriteLine("**** You found a {0}! It has been placed in your inventory ****", player.PlayerPosition.Item.ItemName);
                Console.WriteLine("*************************************************************");
                player.GrabItem();
              }
              player.PlayerLife--;
            }
          }
          break;
        case 3:
        if(playerPos > 0)
          {
            if(player.PlayerPosition.Doors.Contains(board[player.PlayerPosition.PositionId - 1]))
            {
              player.PlayerPosition = board[player.PlayerPosition.PositionId - 1];
              if(player.PlayerPosition.Item != null)
              {
                Console.WriteLine("*************************************************************");
                Console.WriteLine("**** You found a {0}! It has been placed in your inventory ****", player.PlayerPosition.Item.ItemName);
                Console.WriteLine("*************************************************************");
                player.GrabItem();
              }
              player.PlayerLife--;
            }
          }
          break;
        case 4:
          if(playerPos < 15)
          {
            if(player.PlayerPosition.Doors.Contains(board[player.PlayerPosition.PositionId + 1]))
            {
              player.PlayerPosition = board[player.PlayerPosition.PositionId + 1];
              if(player.PlayerPosition.Item != null)
              {
                Console.WriteLine("*************************************************************");
                Console.WriteLine("**** You found a {0}! It has been placed in your inventory ****", player.PlayerPosition.Item.ItemName);
                Console.WriteLine("*************************************************************");
                player.GrabItem();
              }
              player.PlayerLife--;
            }
          }
          else
          {
            if(player.DoorUnlocked == true)
            {
              player.GameActive = false;
              Console.WriteLine("####################################################");
              Console.WriteLine("####################################################");
              Console.WriteLine("####################################################");
              Console.WriteLine("################# Congratulations ##################");
              Console.WriteLine("################ You Escaped Alive #################");
              Console.WriteLine("####################################################");
              Console.WriteLine("####################################################");
            }
          }
          break;
        default:
          Console.WriteLine("Invalid input.");
          break;
      }

    }
    public static void UseItem(Player player)
    {
      if(player.Inventory.Count == 0)
      {
        Console.WriteLine("You don't have any items!!");
      }
      else
      {
        Console.WriteLine("Which item would you like to use?");
        for(int i = 0; i < player.Inventory.Count; i++)
        {
          Console.WriteLine("{0}: {1}", i+1, player.Inventory[i].ItemName);
        }
        int input = int.Parse(Console.ReadLine());
        if(input >= 1 && input < player.Inventory.Count + 1)
        {
          Item choice = player.Inventory[input-1];
          switch(choice.ItemName)
          {
            case "fork":
              Console.WriteLine("**** You stab a wall with the fork, nothing happens ****");
              break;
            case "bowl of food":
              bool foodEaten = false;
              int foodIndex = 0;
              foreach(Item i in player.Inventory)
              {
                if(i.ItemName == "fork")
                {
                  Console.WriteLine("**** You eat the food and gain 5 health ****");
                  player.PlayerLife += 5;
                  foodEaten = true;
                  foodIndex = player.Inventory.FindIndex(x => x.ItemName == "bowl of food");
                }
              }
              if(foodEaten)
              {
                player.Inventory.RemoveAt(foodIndex);
              }
              break;
            case "key":
              if(player.PlayerPosition.PositionId == 15)
              {
                Console.WriteLine("**** You unlock the door ****");
                player.DoorUnlocked = true;
              }
              else
              {
                Console.WriteLine("**** You attempt to unlock the air, nothing happens ****");
              }
              break;
            case "shiny coin":
              Console.WriteLine("**** You flip the coin, it lands heads. It always lands heads... ****");
              break;
          }
        }
        else
        {
          Console.WriteLine("Invalid selection.");
        }
      }
    }
    public static void DrawBoard(Player player)
    {

      string row1 = "|                       |                       |";
      string row2 = "|                                   |           |";
      string row3 = "|           |           |                       |";
      string row4 = "|                       |                        Exit";
      string[] rows = new string[4] {row1, row2, row3, row4};
      int playerX = int.Parse(player.PlayerPosition.Coordinates.Substring(0,1));
      int playerY = int.Parse(player.PlayerPosition.Coordinates.Substring(2));
      char[] rowArray = rows[playerY-1].ToCharArray();
      rowArray[((playerX-1) * 12) + 6] = 'X';
      rows[playerY-1] = new string (rowArray);
      Console.WriteLine("-----------------------------------");
      Console.WriteLine("X is your position.");
      Console.WriteLine("________________________________________________");
      Console.WriteLine("Start       |           |           |           |");
      Console.WriteLine(rows[0]);
      Console.WriteLine("|___________|____   ____|____   ____|____   ____|");
      Console.WriteLine("|           |           |           |           |");
      Console.WriteLine(rows[1]);
      Console.WriteLine("|___________|____   ____|____   ____|___________|");
      Console.WriteLine("|           |           |           |           |");
      Console.WriteLine(rows[2]);
      Console.WriteLine("|____   ____|____   ____|____   ____|____   ____|");
      Console.WriteLine("|           |           |           |           |");
      Console.WriteLine(rows[3]);
      Console.WriteLine("|___________|___________|___________|___________|");
    }
    public static Player CreatePlayer(string name, int difficulty)
    {
      int health = 0;
      switch(difficulty)
      {
        case 1:
          health = 25;
          break;
        case 2:
          health = 20;
          break;
        case 3:
          health = 15;
          break;
        default:
          break;
      }
      Player p1 = new Player(name, health);
      return p1;
    }
    public static void InitializeGame(Room[] board)
    {
      for(int i = 0; i < board.Length; i++)
      {
        board[i] = new Room(null,((i % 4) + 1) + "_" + ((i / 4) + 1), i);
      }
      for(int i = 0; i < board.Length; i++){
        switch(i)
        {
          case 0:
            board[i].Doors.Add(board[1]);
            break;
          case 1:
            board[i].Doors.Add(board[0]);
            board[i].Doors.Add(board[5]);
            break;
          case 2:
            board[i].Doors.Add(board[3]);
            board[i].Doors.Add(board[6]);
            break;
          case 3:
            board[i].Item = new Item("key");
            board[i].Doors.Add(board[2]);
            board[i].Doors.Add(board[7]);
            break;
          case 4:
            board[i].Doors.Add(board[5]);
            break;
          case 5:
            board[i].Doors.Add(board[1]);
            board[i].Doors.Add(board[4]);
            board[i].Doors.Add(board[6]);
            board[i].Doors.Add(board[9]);
            board[i].Item = new Item("fork");
            break;
          case 6:
            board[i].Doors.Add(board[2]);
            board[i].Doors.Add(board[5]);
            board[i].Doors.Add(board[10]);
            break;
          case 7:
            board[i].Doors.Add(board[3]);
            break;
          case 8:
            board[i].Doors.Add(board[12]);
            board[i].Item = new Item("shiny coin");
            break;
          case 9:
            board[i].Doors.Add(board[5]);
            board[i].Doors.Add(board[13]);
            break;
          case 10:
            board[i].Doors.Add(board[6]);
            board[i].Doors.Add(board[11]);
            board[i].Doors.Add(board[14]);
            break;
          case 11:
            board[i].Doors.Add(board[10]);
            board[i].Doors.Add(board[15]);
            break;
          case 12:
            board[i].Doors.Add(board[8]);
            board[i].Doors.Add(board[13]);
            break;
          case 13:
            board[i].Doors.Add(board[12]);
            board[i].Doors.Add(board[9]);
            board[i].Item = new Item("bowl of food");
            break;
          case 14:
            board[i].Doors.Add(board[10]);
            board[i].Doors.Add(board[15]);
            break;
          case 15:
            board[i].Doors.Add(board[11]);
            board[i].Doors.Add(board[14]);
            break;
          default:
            break;
        }
      }
    }
  }
}
