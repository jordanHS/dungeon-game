using System.Collections.Generic;
namespace DungeonGame.Models
{
  public class Room
  {
    public Item Item { get; set; }
    public string Coordinates { get; set; }
    public List<Room> Doors { get; set; } = new List<Room>();
    public Room(Item itemconstructor, string position)
    {
      Item = itemconstructor;
      Coordinates = position;
    }
  }
}
