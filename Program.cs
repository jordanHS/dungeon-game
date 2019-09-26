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
      foreach(Room i in GameBoard)
      {
        Console.WriteLine(i.Coordinates);
        if(i.Item != null)
          Console.WriteLine(i.Item.ItemName);
        foreach(Room j in i.Doors)
        {
          Console.WriteLine("+" + j.Coordinates);
        }
      }
    }
    public static void InitializeGame(Room[] board)
    {
      for(int i = 0; i < board.Length; i++)
      {
        board[i] = new Room(null,((i % 4) + 1) + "_" + ((i / 4) + 1));
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
            board[i].Item = new Item("money");
            break;
          case 9:
            board[i].Doors.Add(board[5]);
            board[i].Doors.Add(board[13]);
            break;
          case 10:
            board[i].Doors.Add(board[6]);
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
            board[i].Item = new Item("food");
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
