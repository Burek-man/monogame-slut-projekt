using System;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SharpDX.XInput;

namespace spaceshhoter;

public class Game1 : Game
{
    private Texture2D backround;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Player player;
    private Texture2D dacia;
    private Texture2D alberg;
    private Camera camera;
    private GameStates _gameState = GameStates.Playing;
    private SpriteFont meny;
    private Texture2D backroundtexturemeny;
   
    private Texture2D speedometerDial;
    private Texture2D needle;
    private SpriteFont font;

    private Vector2 dialPosition;
    private Vector2 needleOrigin;

    private Vector2 objectPosition;
    private Vector2 objectVelocity;



    public enum GameStates{
        Menu,
        Playing,
        Paused,
        GameOver,
    }


    float maxSpeed = 300f;
    Song theme; 

    public Game1()
    {
        GraphicsDeviceManager graphics;
        graphics = new GraphicsDeviceManager(this);
        //graphics.ToggleFullScreen();

        Content.RootDirectory = "Content";
        IsMouseVisible = false;


    }

    protected override void Initialize()
    {
        camera = new Camera(GraphicsDevice.Viewport);
        base.Initialize();
        dialPosition = new Vector2(100, 100); // Position of speedometer
        objectPosition = new Vector2(400, 240);
    }


    protected override void LoadContent()
    {



        _spriteBatch = new SpriteBatch(GraphicsDevice);

        speedometerDial = Content.Load<Texture2D>("speedometerDial");

        needle = Content.Load<Texture2D>("needle");

        font = Content.Load<SpriteFont>("Font"); // Add a SpriteFont

        needleOrigin = new Vector2(needle.Width / 2f, needle.Height);


        backround = Content.Load<Texture2D>("background-road");

        alberg = Content.Load<Texture2D>("Alberg");

        dacia = Content.Load<Texture2D>("Dacia");

        backroundtexturemeny = Content.Load<Texture2D>("geneva-dacia-dusterjpeg");

        meny = Content.Load<SpriteFont>("File1");

        player = new Player(dacia, new Vector2(380, 250), 150);


        theme = Content.Load<Song>("youtube_tjvz3ZHx5Ls_audio");
        MediaPlayer.Play(theme);

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        player.Update(gameTime);
        camera.UpdateCamera(GraphicsDevice.Viewport,player.Hitbox.Location.ToVector2());

}


    protected override void Draw(GameTime gameTime)
    {
        



        GraphicsDevice.Clear(new Color(0x666666));

        if (_gameState == GameStates.Menu)
        {
            _spriteBatch.Begin();
            Rectangle bgRect = new(0, 0, 800, 480);
            _spriteBatch.Draw(backroundtexturemeny, bgRect, Color.White);
            _spriteBatch.DrawString(meny, "Dacia Duster The Game", new Vector2(225, 100), Color.Azure);
            _spriteBatch.End();
        }
        
        else if (_gameState == GameStates.Playing || _gameState == GameStates.GameOver)

        {

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.Transform);
            for (int i = -100; i < 100; i++)
            {
                Rectangle rect = new(0, 600 * i, 800, 600);
                _spriteBatch.Draw(backround, rect, Color.White);
            }

            player.Draw(_spriteBatch);



            _spriteBatch.End();


            _spriteBatch.Begin();
            var kstate = Keyboard.GetState();

            objectVelocity = Vector2.Zero;

            if (kstate.IsKeyDown(Keys.W)) objectVelocity.Y -= 1;
            if (kstate.IsKeyDown(Keys.S)) objectVelocity.Y += 1;
            if (kstate.IsKeyDown(Keys.A)) objectVelocity.X -= 1;
            if (kstate.IsKeyDown(Keys.D)) objectVelocity.X += 1;

            if (objectVelocity != Vector2.Zero)
                objectVelocity.Normalize();

            float speed = 150f;
            objectVelocity *= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            objectPosition += objectVelocity;

            base.Update(gameTime);

            float currentSpeed = objectVelocity.Length() / (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            float needleAngle = MathHelper.ToRadians(-135f) +
                (MathHelper.ToRadians(270f) * (currentSpeed / maxSpeed));
            needleAngle = MathHelper.Clamp(needleAngle, MathHelper.ToRadians(-135f), MathHelper.ToRadians(135f));




            // Draw rotating needle
            _spriteBatch.Draw(needle, dialPosition + new Vector2(speedometerDial.Width / 2f, speedometerDial.Height / 2f),
                            null, Color.White, needleAngle, needleOrigin, 10f, SpriteEffects.None, 0f);


            // Draw speedometer dial
            _spriteBatch.Draw(speedometerDial, new Rectangle(dialPosition.ToPoint(), new Point(200, 200)), Color.White);


            _spriteBatch.End();





        }    





        base.Draw(gameTime);

    }



}