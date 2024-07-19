using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tilegame.Utility.Enums;

namespace TileGame.Entity;

public class Tile : Entity
{
    private TileType tileType { get; set; }

    private Tile(Texture2D texture, Vector2 pos, TileType tiletype) : base(texture, pos)
    {
        this.texture = texture;
        position = pos;
        tileType = tiletype;
    }

    public static Tile Create(Texture2D texture, Vector2 pos, TileType tiletype)
    {
        return new Tile(texture, pos, tiletype)
        {
            tileType = tiletype
        };
    }
}