using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlayerNamespace;

public class Player
{
    public Vector2 Position { get; set; }
    public Texture2D Texture { get; set; }
    public bool IsVisible { get; set; }

    public Player(Texture2D texture)
    {
        Texture = texture;
        IsVisible = true;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Position, Color.White);
    }
}