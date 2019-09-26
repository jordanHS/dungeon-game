using System.Collections.Generic;
namespace DungeonGame.Models
{
  public class Player
  {
    public List<Item> Inventory { get; set; } = new List<Item>();
    public string PlayerName { get; set; }
    public Room PlayerPosition { get; set; }
    public int PlayerLife { get; set; }
    public Player(string name, int life)
    {
      PlayerName = name;
      PlayerLife = life;
    }
    public void GrabItem()
    {
      Inventory.Add(PlayerPosition.Item);
      PlayerPosition.Item = null;
    }
  }
}
