using System.Collections.Generic;
namespace DungeonGame.Models
{
  public class Player
  {
    List<Item> Inventory = new List<Item>();
    public string PlayerName { get; set; }
    public string PlayerPosition { get; set; }
    public Player(string name)
    {
      PlayerName = name;
      PlayerPosition = "1_1";
    }
  }
}
