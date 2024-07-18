using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlayerNamespace;

namespace TileGame;

public class TileGame : Game
{
    private GraphicsDeviceManager _graphics;
    private Vector2 windowSize;
    private SpriteBatch _spriteBatch;
    private Player _player;
    private Texture2D _backgroundTexture;


    public TileGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        windowSize.X = 750;
        windowSize.Y = 1000;
        _graphics.PreferredBackBufferWidth = (int)windowSize.X;
        _graphics.PreferredBackBufferHeight = (int)windowSize.Y;
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
        Texture2D playerTexture = Content.Load<Texture2D>("playerTexture");
        _player = new Player(playerTexture, windowSize);


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

        int screenWidth = GraphicsDevice.Viewport.Width;
        int screenHeight = GraphicsDevice.Viewport.Height;
        int halfHeight = screenHeight / 2;

        _spriteBatch.Begin();

        _spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, screenWidth, halfHeight), Color.DarkGray);
        _spriteBatch.Draw(_backgroundTexture, new Rectangle(0, halfHeight, screenWidth, halfHeight), Color.Gray);

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
