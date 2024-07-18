using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PlayerNamespace;

public class Player
{
    private Vector2 Position;
    private float speed { get; set; }
    private int tileSize { get; set; }
    public Texture2D Texture { get; set; }
    private bool IsVisible { get; set; }
    // Tile size should be the the width of the window divided by 20
    private float moveDelay = 0.1f; // Adjust the delay as needed
    private float timeSinceLastMove = 0f;
    private Vector2 windowSize;

    public Player(Texture2D texture, Vector2 wSize)
    {
        Texture = texture;
        IsVisible = true;
        tileSize = (int)wSize.X / 15;
        Position = new Vector2(0, 0); //Default position
        speed = 5f;
        windowSize = wSize;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (IsVisible)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
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
                    timeSinceLastMove = 0f; // Reset the move delay timer
                    break; // Move only once per input, remove this line if you want to handle multiple keys
                }
            }
        }
    }

    private void moveUp()
    {
        if (Position.Y - tileSize >= 0)
        { // Temporary collision
            Position.Y -= tileSize;
        }
    }

    private void moveDown()
    {
        if (Position.Y + tileSize < windowSize.Y / 2)
        { // Temporary collision
            Position.Y += tileSize;
        }
    }

    private void moveLeft()
    {
        if (Position.X - tileSize >= 0)
        { // Temporary collision
            Position.X -= tileSize;
        }
    }

    private void moveRight()
    {
        if (Position.X + tileSize < windowSize.X)
        { // Temporary collision
            Position.X += tileSize;
        }
    }

    public Vector2 GetPosition()
    {
        return Position;
    }
}