using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TileGame.Utility;

namespace TileGame.Entity;

public class Player
{
    private Vector2 position;
    public Texture2D Texture { get; set; }
    private bool IsVisible { get; set; }
    private float moveDelay = 0.1f;
    private float timeSinceLastMove = 0f;

    public Player(Texture2D texture, Vector2 startPos)
    {
        Texture = texture;
        IsVisible = true;
        position = startPos;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (IsVisible)
        {
            spriteBatch.Draw(Texture, position, Color.White);
        }
    }

    public void Update()
    {

    }

    public void Move(KeyboardState input, float deltaTime)
    {
        timeSinceLastMove += deltaTime;

        if (timeSinceLastMove >= moveDelay)
        {
            var keyActions = new Dictionary<Keys, System.Action>
                {
                    { Keys.Up, () => moveUp() },
                    { Keys.Down, () => moveDown() },
                    { Keys.Left, () => moveLeft() },
                    { Keys.Right, () => moveRight() }
                };

            foreach (var keyAction in keyActions)
            {
                if (input.IsKeyDown(keyAction.Key))
                {
                    keyAction.Value();
                    timeSinceLastMove = 0f;
                    break;
                }
            }
        }
    }

    private void moveUp()
    {
        if (position.Y - GameUtil.TileSize >= 0)
        { // Temporary collision
            position.Y -= GameUtil.TileSize;
        }
    }

    private void moveDown()
    {
        if (position.Y + GameUtil.TileSize * 2 < GameUtil.WindowSize.Y / 2)
        { // Temporary collision
            position.Y += GameUtil.TileSize;
        }
    }

    private void moveLeft()
    {
        if (position.X - GameUtil.TileSize >= 0)
        { // Temporary collision
            position.X -= GameUtil.TileSize;
        }
    }

    private void moveRight()
    {
        if (position.X + GameUtil.TileSize * 2 < GameUtil.WindowSize.X)
        { // Temporary collision
            position.X += GameUtil.TileSize;
        }
    }

    public Vector2 Getposition()
    {
        return position;
    }
}