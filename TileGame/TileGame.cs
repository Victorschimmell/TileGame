using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlayerNamespace;

namespace TileGame;

public class TileGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Player _player;
    private Texture2D _whiteTexture;


    public TileGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _graphics.PreferredBackBufferWidth = 750;
        _graphics.PreferredBackBufferHeight = 1000;

    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Load player
        Texture2D playerTexture = Content.Load<Texture2D>("Tick_Mark_Dark-512");
        _player = new Player(playerTexture)
        {
            Position = new Vector2(100, 100) // Set initial position
        };

        // Initial setup of two screens
        _whiteTexture = new Texture2D(GraphicsDevice, 1, 1);
        _whiteTexture.SetData(new[] { Color.White });
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        int screenWidth = GraphicsDevice.Viewport.Width;
        int screenHeight = GraphicsDevice.Viewport.Height;
        int halfHeight = screenHeight / 2;


        // Drawing sprites, specifically the playersprite
        _spriteBatch.Begin();

        // Draw upper rectangle (white)
        _spriteBatch.Draw(_whiteTexture, new Rectangle(0, 0, screenWidth, halfHeight), Color.DarkGray);

        // Draw lower rectangle (black)
        _spriteBatch.Draw(_whiteTexture, new Rectangle(0, halfHeight, screenWidth, halfHeight), Color.Gray);


        _player.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    protected override void UnloadContent()
    {
        // Unload any non-ContentManager content here
        _player.Texture.Dispose();
    }
}
