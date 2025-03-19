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
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Player player;
    private Texture2D spaceShip;
    private Texture2D enemyspaceship;
    private Texture2D backgrundbild;
    private Texture2D Ufo;
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
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);


        spaceShip = Content.Load<Texture2D>("spaceship");

        enemyspaceship = Content.Load<Texture2D>("enemyspaceship");

        Ufo = Content.Load<Texture2D>("ufoEnemy");

        backgrundbild = Content.Load<Texture2D>("background-road.png");

        player = new Player(spaceShip,new Vector2(380,350),50);

        enemies.Add(new Enemy(enemyspaceship));
        
        theme = Content.Load<Song>("themesong1");
        MediaPlayer.Play(theme);

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        

        player.Update();
        foreach(Enemy Enemy in enemies){
            Enemy.Update();
        }

        enemybulletCollision();

        SpawnEnemy();
        base.Update(gameTime);
    }  

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);

        _spriteBatch.Begin();
        Rectangle bgRect = new(0, 0, 800, 600);
        _spriteBatch.Draw(backgrundbild, bgRect, Microsoft.Xna.Framework.Color.White);
        player.Draw(_spriteBatch);
        foreach(Enemy Enemy in enemies)
        Enemy.Draw(_spriteBatch);

        _spriteBatch.End();



        base.Draw(gameTime);
    }
    private void SpawnEnemy(){
        Random rand = new Random();
        int value = rand.Next(1,101);
        int spawnChancePercent = 2;
        if(value<=spawnChancePercent) {
            enemies.Add(new Enemy(enemyspaceship));
            enemies.Add(new Enemy(Ufo));
        }
    }

private void enemybulletCollision(){
for(int i = 0; i <enemies.Count; i++){
    for(int j = 0; j <player.Bullets.Count; j++){
        if(enemies[i].Hitbox.Intersects(player.Bullets[j].Hitbox)){
            enemies.RemoveAt(i);
            player.Bullets.RemoveAt(j);
            i--;  
        }
    }

}
}

}