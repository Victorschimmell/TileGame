using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TileGame.Utility;
using Tilegame.Utility.Enums;

namespace TileGame.Entity
{
    public class Player : Entity
    {
        private float moveDelay = 0.1f;
        private float timeSinceLastFrame = 0f;
        private float keyPressDuration = 0f; // Track how long the key has been pressed
        private int currentFrame = 0;

        private Directions currentDirection = Directions.Down;

        private Texture2D[,] animationFrames;

        private Vector2 targetPosition;
        private bool isMoving = false;

        private Keys lastKeyPressed;

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
            if (IsVisible())
            {
                Texture2D currentTexture = animationFrames[(int)currentDirection, currentFrame];
                spriteBatch.Draw(currentTexture, new Rectangle((int)position.X, (int)position.Y, GameUtil.PlayerSize, GameUtil.PlayerSize), Color.White);
            }
        }

        public void Move(KeyboardState input, float deltaTime)
        {
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

                var keyActions = new Dictionary<Keys, System.Action>
                {
                    { Keys.Up, () => ChangeDirectionAndMove(Directions.Up) },
                    { Keys.Down, () => ChangeDirectionAndMove(Directions.Down) },
                    { Keys.Left, () => ChangeDirectionAndMove(Directions.Left) },
                    { Keys.Right, () => ChangeDirectionAndMove(Directions.Right) }
                };

                foreach (var keyAction in keyActions)
                {
                    if (input.IsKeyDown(keyAction.Key))
                    {
                        if (lastKeyPressed == keyAction.Key)
                        {
                            keyPressDuration += deltaTime;
                        }
                        else
                        {
                            lastKeyPressed = keyAction.Key;
                            keyPressDuration = 0f;
                        }

                        if (keyPressDuration >= moveDelay)
                        {
                            keyAction.Value();
                            moved = true;
                            break;
                        }
                        else
                        {
                            ChangeDirection(keyAction.Key);
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

        private void ChangeDirection(Keys key)
        {
            switch (key)
            {
                case Keys.Up:
                    currentDirection = Directions.Up;
                    break;
                case Keys.Down:
                    currentDirection = Directions.Down;
                    break;
                case Keys.Left:
                    currentDirection = Directions.Left;
                    break;
                case Keys.Right:
                    currentDirection = Directions.Right;
                    break;
            }
        }

        private void ChangeDirectionAndMove(Directions direction)
        {
            currentDirection = direction;
            switch (direction)
            {
                case Directions.Up:
                    if (position.Y - GameUtil.TileSize >= 0)
                    {
                        targetPosition.Y -= GameUtil.TileSize;
                        isMoving = true;
                    }
                    break;
                case Directions.Down:
                    if (position.Y + GameUtil.PlayerSize < GameUtil.WindowSize.Y / 2)
                    {
                        targetPosition.Y += GameUtil.TileSize;
                        isMoving = true;
                    }
                    break;
                case Directions.Left:
                    if (position.X - GameUtil.TileSize >= 0)
                    {
                        targetPosition.X -= GameUtil.TileSize;
                        isMoving = true;
                    }
                    break;
                case Directions.Right:
                    if (position.X + GameUtil.PlayerSize < GameUtil.WindowSize.X)
                    {
                        targetPosition.X += GameUtil.TileSize;
                        isMoving = true;
                    }
                    break;
                default:
                    isMoving = false;
                    break;
            }
        }

        public Directions GetDirection()
        {
            return currentDirection;
        }
    }
}
