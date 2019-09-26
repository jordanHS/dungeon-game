namespace DungeonGame.Models
{
  public class Item
  {
    public string ItemName { get; set; }
    public Item(string name)
    {
      ItemName = name;
    }
  }
}
