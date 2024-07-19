using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TileGame.Utility;
using Tilegame.Utility.Enums;
using System;

namespace TileGame.Entity
{
    public class Player : Entity
    {
        private float moveDelay = 0.1f;
        private float timeSinceLastMove = 0f;
        private float timeSinceLastFrame = 0f;
        private int currentFrame = 0;

        private Directions currentDirection = Directions.Down;

        private Texture2D[,] animationFrames;

        private Vector2 targetPosition;
        private bool isMoving = false;

        public Player(Texture2D texture, Vector2 pos) : base(texture, pos)
        {
            InitializeAnimationFrames(texture);
            targetPosition = pos;
        }

        private void InitializeAnimationFrames(Texture2D texture)
        {
            int frameWidth = texture.Width / 4;
            int frameHeight = texture.Height / 4;

            animationFrames = new Texture2D[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Rectangle sourceRectangle = new Rectangle(j * frameWidth, i * frameHeight, frameWidth, frameHeight);
                    Texture2D frame = new Texture2D(texture.GraphicsDevice, frameWidth, frameHeight);
                    Color[] data = new Color[frameWidth * frameHeight];
                    texture.GetData(0, sourceRectangle, data, 0, data.Length);
                    frame.SetData(data);
                    animationFrames[i, j] = frame;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Console.Write(Getposition());
            if (IsVisible())
            {
                Texture2D currentTexture = animationFrames[(int)currentDirection, currentFrame];
                spriteBatch.Draw(currentTexture, new Rectangle((int)position.X, (int)position.Y, GameUtil.TileSize, GameUtil.TileSize), Color.White);
            }
        }

        public void Move(KeyboardState input, float deltaTime)
        {
            timeSinceLastMove += deltaTime;
            timeSinceLastFrame += deltaTime;

            if (isMoving)
            {
                Vector2 direction = Vector2.Normalize(targetPosition - position);
                position += direction * GameUtil.CharacterMoveSpeed * deltaTime;

                if (Vector2.Distance(position, targetPosition) < GameUtil.CharacterMoveSpeed * deltaTime)
                {
                    position = targetPosition;
                    isMoving = false;
                }
            }
            else
            {
                bool moved = false;

                if (timeSinceLastMove >= moveDelay)
                {
                    var keyActions = new Dictionary<Keys, System.Action>
                    {
                        { Keys.Up, () => MoveUp() },
                        { Keys.Down, () => MoveDown() },
                        { Keys.Left, () => MoveLeft() },
                        { Keys.Right, () => MoveRight() }
                    };

                    foreach (var keyAction in keyActions)
                    {
                        if (input.IsKeyDown(keyAction.Key))
                        {
                            keyAction.Value();
                            timeSinceLastMove = 0f;
                            moved = true;
                            break;
                        }
                    }
                }

                if (moved)
                {
                    if (timeSinceLastFrame >= GameUtil.AnimationDelay)
                    {
                        currentFrame = (currentFrame + 1) % 4;
                        timeSinceLastFrame = 0f;
                    }
                }
                else
                {
                    currentFrame = 0;
                }
            }
        }

        private void MoveUp()
        {
            currentDirection = Directions.Up;
            Vector2 newPosition = new(position.X, position.Y - GameUtil.TileSize);
            if (newPosition.Y >= 0)
            {
                targetPosition = newPosition;
                isMoving = true;
            }
        }

        private void MoveDown()
        {
            currentDirection = Directions.Down;
            Vector2 newPosition = new(position.X, position.Y + GameUtil.TileSize);
            if (newPosition.Y < GameUtil.WindowSize.Y / 2)
            {
                targetPosition = newPosition;
                isMoving = true;
            }
        }

        private void MoveLeft()
        {
            currentDirection = Directions.Left;
            Vector2 newPosition = new(position.X - GameUtil.TileSize, position.Y);
            if (newPosition.X >= 0)
            {
                targetPosition = newPosition;
                isMoving = true;
            }
        }

        private void MoveRight()
        {
            currentDirection = Directions.Right;
            Vector2 newPosition = new(position.X + GameUtil.TileSize, position.Y);
            if (newPosition.X < GameUtil.WindowSize.X)
            {
                targetPosition = newPosition;
                isMoving = true;
            }
        }
    }
}
