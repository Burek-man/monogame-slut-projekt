using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Monogame._2;

namespace spaceshhoter;

public class meny : Game
{ 
private Texture2D background; 
private SpriteBatch _Spritebatch;
private Texture2D buttonTexture;
private SpriteFont font;
private Rectangle startButtonRect;
private MouseState mouseState;
GameState currentGameState = GameState.TitleScreen;

protected override void LoadContent()
{
    _Spritebatch = new SpriteBatch(GraphicsDevice);

    background = Content.Load<Texture2D>("geneva-dacia-dusterjpeg.jpeg"); // Your image file name without extension
    buttonTexture = new Texture2D(GraphicsDevice, 1, 1);
    buttonTexture.SetData(new[] { Color.White });

    font = Content.Load<SpriteFont>("DefaultFont"); // Make sure to include a .spritefont
    startButtonRect = new Rectangle(300, 400, 200, 60);
}


protected override void Update(GameTime gameTime)
{
    mouseState = Mouse.GetState();

    if (currentGameState == GameState.TitleScreen)
    {
        if (mouseState.LeftButton == ButtonState.Pressed &&
            startButtonRect.Contains(mouseState.Position))
        {
            currentGameState = GameState.Playing;
        }
    }

    base.Update(gameTime);
}

protected override void Draw(GameTime gameTime)
{
    GraphicsDevice.Clear(Color.Black);
    _Spritebatch.Begin();

    if (currentGameState == GameState.TitleScreen)
    {
        // Background image
        _Spritebatch.Draw(background, GraphicsDevice.Viewport.Bounds, Color.White);

        // Start button
        _Spritebatch.Draw(buttonTexture, startButtonRect, Color.Black * 0.6f);
        _Spritebatch.DrawString(font, "Start Game", new Vector2(startButtonRect.X + 30, startButtonRect.Y + 20), Color.White);
    }
    else if (currentGameState == GameState.Playing)
    {
        _Spritebatch.DrawString(font, "Game Running...", new Vector2(300, 300), Color.White);
    }

    _Spritebatch.End();
    base.Draw(gameTime);
}

}