using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Steamworks;

namespace GyroTest;

public class Game1 : Game {
    
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    public static bool isSteamRunning = false;
    
    private Color default_colour = Color.Cyan;
    
    public Game1() {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize() {
        // TODO: Add your initialization logic here

        try {
            bool steam_launched = SteamAPI.Init();
            if (!steam_launched) {
                Trace.WriteLine("Steam init failed!");
            } else {
                isSteamRunning = true;
                Exiting += GameExiting;
                default_colour = Color.Red;
            }
        } catch (Exception e) {
            Trace.WriteLine(e);
            throw;
        }

        base.Initialize();
    }

    protected override void LoadContent() {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime) {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if (isSteamRunning) {
            SteamAPI.RunCallbacks();
            
        }

        base.Update(gameTime);
    }

    private void GameExiting(object sender, EventArgs e) {
        SteamAPI.Shutdown();
    }

    protected override void Draw(GameTime gameTime) {
        GraphicsDevice.Clear(default_colour);

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
