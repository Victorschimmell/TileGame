using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TileGame.Entity;
using TileGame.Utility;

namespace TileGame;

public class TileGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Player _player;
    private Texture2D _backgroundTexture;
    private SpriteFont _font;


    private Texture2D tempTexture; // To be removes


    public TileGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _graphics.PreferredBackBufferWidth = (int)GameUtil.WindowSize.X;
        _graphics.PreferredBackBufferHeight = (int)GameUtil.WindowSize.Y;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Load map
        tempTexture = Content.Load<Texture2D>("tile_1");


        // Load player
        Texture2D playerTexture = Content.Load<Texture2D>("HGSS_155");
        _player = new Player(playerTexture, new(0, 0));
        _font = Content.Load<SpriteFont>("DefaultFont");


        // Initial setup of two screens, base look
        _backgroundTexture = new Texture2D(GraphicsDevice, 1, 1);
        _backgroundTexture.SetData(new[] { Color.White });
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // Temporary player movement
        var kstate = Keyboard.GetState();
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        _player.Move(kstate, deltaTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, (int)GameUtil.WindowSize.X, (int)GameUtil.WindowSize.Y / 2), Color.DarkGray);
        _spriteBatch.Draw(_backgroundTexture, new Rectangle(0, (int)GameUtil.WindowSize.Y / 2, (int)GameUtil.WindowSize.X, (int)GameUtil.WindowSize.Y / 2), Color.Gray);

        // Playing around with tile generation
        Vector2 pos = new(0, 0);
        for (int i = 0; i < GameUtil.WindowSize.X / GameUtil.TileSize; i++)
        {
            pos.Y = 0;
            for (int j = 0; j < GameUtil.WindowSize.Y / 2 / GameUtil.TileSize; j++)
            {
                _spriteBatch.Draw(tempTexture, new Rectangle((int)pos.X, (int)pos.Y, GameUtil.TileSize, GameUtil.TileSize), Color.White);
                pos.Y += GameUtil.TileSize;
            }
            pos.X += GameUtil.TileSize;
        }

        _player.Draw(_spriteBatch);

        // Draw players current position
        string playerPositionText = $"Player Position: {_player.GetPosition()}";
        Vector2 textPosition = new Vector2(10, (int)GameUtil.WindowSize.Y / 2 + 25);
        _spriteBatch.DrawString(_font, playerPositionText, textPosition, Color.White);

        string directionPositionText = $"Current direction: {_player.GetDirection()}";
        Vector2 textPosition2 = new Vector2(10, (int)GameUtil.WindowSize.Y / 2 + 50);
        _spriteBatch.DrawString(_font, directionPositionText, textPosition2, Color.White);

        string resolutionPositionText = $"Resolution: {GameUtil.WindowSize}";
        Vector2 textPosition3 = new Vector2(10, (int)GameUtil.WindowSize.Y / 2 + 75);
        _spriteBatch.DrawString(_font, resolutionPositionText, textPosition3, Color.White);


        _spriteBatch.End();

        base.Draw(gameTime);
    }

    protected override void UnloadContent()
    {
        // Unload any non-ContentManager content here
        _player.Unload();
    }
}
