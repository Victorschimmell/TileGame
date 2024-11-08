using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TileGame.Utility;

public static class GameUtil
{
    // General values
    public static readonly Vector2 WindowSize = new(600, 832);

    // Entities
    public static readonly int TileSize = 32;
    public static readonly int PlayerSize = 64;
    public static readonly float CharacterMoveSpeed = 175f;
    public static readonly float CharacterMoveDelay = 0.1f;
    public static readonly float AnimationDelay = 0.1f;

}