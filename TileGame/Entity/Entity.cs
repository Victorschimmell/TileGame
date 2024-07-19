using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TileGame.Utility;

namespace TileGame.Entity;

public class Entity
{
    protected Texture2D texture { get; set; }
    protected Vector2 position { get; set; }
    private bool isVisible { get; set; }
    private bool isDeleted { get; set; }

    public Entity(Texture2D texture, Vector2 pos)
    {
        isVisible = true;
        isDeleted = false;
        this.texture = texture;
        position = pos;
    }

    public void DeleteEntity()
    {
        isDeleted = true;
    }

    public void SetVisibility(bool visible)
    {
        isVisible = visible;
    }

    public bool IsVisible()
    {
        return isVisible;
    }

    public bool IsDeleted()
    {
        return isDeleted;
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        if (isVisible)
        {
            //spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, GameUtil.TileSize, GameUtil.TileSize), Color.White);
        }
    }

    public void Update()
    {
    }

    public void Unload()
    {
        texture.Dispose();
    }

    public Vector2 Getposition()
    {
        return position;
    }

    public void UpdatePosition(Vector2 pos)
    {
        position = pos;
    }
}