using Tilegame.Utility.Enums;

namespace Tilegame.Entity;

public class Meta
{
    public string Name { get; }
    public TileType TileType { get; }

    public Meta(string name, TileType TileType)
    {
        Name = name;
        this.TileType = TileType;
    }
}