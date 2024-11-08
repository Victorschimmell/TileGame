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
        private float timeSinceLastFrame = 0f;
        private float keyPressDuration = 0f;
        private int currentFrame = 0;
        private Directions currentDirection = Directions.Down;
        private Texture2D[,] animationFrames;
        private Vector2 targetPosition;
        private bool isMoving = false;
        private Keys lastKeyPressed;
        private readonly Dictionary<Keys, Directions> keyDirectionMap;
        private const float RunSpeedMultiplier = 2f; 
        private bool isRunning = false;

        public Player(Texture2D texture, Vector2 pos) : base(texture, pos)
        {
            InitializeAnimationFrames(texture);
            targetPosition = pos;
            keyDirectionMap = new Dictionary<Keys, Directions>
            {
                { Keys.Up, Directions.Up },
                { Keys.Down, Directions.Down },
                { Keys.Left, Directions.Left },
                { Keys.Right, Directions.Right }
            };
        }

        private void InitializeAnimationFrames(Texture2D texture)
        {
            animationFrames = new Texture2D[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
 
                    animationFrames[i, j] = texture;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible())
            {
                Texture2D currentTexture = animationFrames[(int)currentDirection, currentFrame];
                Rectangle sourceRectangle = new Rectangle(currentFrame * GameUtil.PlayerSize, (int)currentDirection * GameUtil.PlayerSize, GameUtil.PlayerSize, GameUtil.PlayerSize);
                spriteBatch.Draw(currentTexture, new Rectangle((int)position.X, (int)position.Y, GameUtil.PlayerSize, GameUtil.PlayerSize), sourceRectangle, Color.White);
            }
        }

        public void Move(KeyboardState input, float deltaTime)
        {
            timeSinceLastFrame += deltaTime;

            isRunning = input.IsKeyDown(Keys.Z);

            if (isMoving)
            {
                MoveToTargetPosition(deltaTime);
                AnimateMovement();
            }
            else
            {
                HandleInput(input, deltaTime);
            }
        }

        private void MoveToTargetPosition(float deltaTime)
        {
            float currentSpeed = GameUtil.CharacterMoveSpeed * (isRunning ? RunSpeedMultiplier : 1f);
            Vector2 direction = Vector2.Normalize(targetPosition - position);
            position += direction * currentSpeed * deltaTime;

            if (Vector2.Distance(position, targetPosition) < currentSpeed * deltaTime)
            {
                position = targetPosition;
                isMoving = false;
            }
        }

        private void HandleInput(KeyboardState input, float deltaTime)
        {
            bool moved = false;
            foreach (var keyDirection in keyDirectionMap)
            {
                if (input.IsKeyDown(keyDirection.Key))
                {
                    if (lastKeyPressed == keyDirection.Key)
                        keyPressDuration += deltaTime;
                    else
                        ResetKeyPress(keyDirection.Key);

                    if (keyPressDuration >= GameUtil.CharacterMoveDelay)
                    {
                        SetDirectionAndMove(keyDirection.Value);
                        moved = true;
                        break;
                    }
                    else
                    {
                        currentDirection = keyDirection.Value;
                    }
                }
            }

            if (!moved)
            {
                currentFrame = 0;
            }
        }


        private void ResetKeyPress(Keys key)
        {
            lastKeyPressed = key;
            keyPressDuration = 0f;
        }

        private void AnimateMovement()
        {
            float animationDelay = isRunning ? GameUtil.AnimationDelay / RunSpeedMultiplier : GameUtil.AnimationDelay;

            if (timeSinceLastFrame >= animationDelay)
            {
                currentFrame = (currentFrame + 1) % 4;
                timeSinceLastFrame = 0f;
            }
        }

        private void SetDirectionAndMove(Directions direction)
        {
            currentDirection = direction;
            Vector2 nextPosition = position;

            switch (direction)
            {
                case Directions.Up:
                    nextPosition.Y -= GameUtil.TileSize;
                    break;
                case Directions.Down:
                    nextPosition.Y += GameUtil.TileSize;
                    break;
                case Directions.Left:
                    nextPosition.X -= GameUtil.TileSize;
                    break;
                case Directions.Right:
                    nextPosition.X += GameUtil.TileSize;
                    break;
            }

            if (IsPositionWithinBounds(nextPosition))
            {
                targetPosition = nextPosition;
                isMoving = true;
            }
        }

        private bool IsPositionWithinBounds(Vector2 position)
        {
            return position.X >= 0 && position.X + GameUtil.PlayerSize <= GameUtil.WindowSize.X + GameUtil.PlayerSize/2 &&
                   position.Y >= 0 && position.Y + GameUtil.PlayerSize <= GameUtil.WindowSize.Y/2;
        }

        public Directions GetDirection()
        {
            return currentDirection;
        }
    }
}
