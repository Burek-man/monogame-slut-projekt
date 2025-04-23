using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Monogame._2;

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


    private List<Enemy> enemies = new List<Enemy>();
    Song theme; 

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        camera = new Camera(GraphicsDevice.Viewport);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        backround = Content.Load<Texture2D>("background-road");

        alberg = Content.Load<Texture2D>("Alberg");

        dacia = Content.Load<Texture2D>("Dacia");

        player = new Player(dacia,new Vector2(380,250),150);

        enemies.Add(new Enemy(alberg));
        
        theme = Content.Load<Song>("youtube_tjvz3ZHx5Ls_audio");
        MediaPlayer.Play(theme);

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        
             player.Update();
        camera.UpdateCamera(GraphicsDevice.Viewport,player.Hitbox.Location.ToVector2());



        enemybulletCollision();

        SpawnEnemy();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime){

        
        GraphicsDevice.Clear(new Color(0x666666));
        
        _spriteBatch.Begin(SpriteSortMode.Deferred,null,null,null,null,null,camera.Transform);
        for(int i = -100; i < 100; i++) {
            Rectangle bgRect = new(0, 600*i, 800, 600);
            _spriteBatch.Draw(backround, bgRect, Color.White);
        }
        
        player.Draw(_spriteBatch);

       

        _spriteBatch.End();
        base.Draw(gameTime);

    }


    private void SpawnEnemy(){
        Random rand = new Random();
        int value = rand.Next(1,1001);
        int spawnChancePercent = 5;
        if(value<=spawnChancePercent) {
            enemies.Add(new Enemy(alberg));
        }
    }

private void enemybulletCollision(){
for(int i = 0; i <enemies.Count; i++){
    for(int j = 0; j <player.Bullets.Count; j++){
        if(enemies[i].Hitbox.Intersects(player.Bullets[j].Hitbox)){
            enemies.RemoveAt(i);
            player.Bullets.RemoveAt(j);
            break;  
        }
    }

}
}

}