using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;

namespace spaceshhoter
{
    public class Player
    {
        private Texture2D texture;
        private Vector2 position;
        private Rectangle hitbox;
        private KeyboardState newKstate;
        private float speed = 0;
        private KeyboardState oldkState;
        private float hp;

        private List<bullet> bullets = new List<bullet>();

        public List<bullet> Bullets{
            get{return bullets;}
        }
        public float Hp{
            get{return hp;}
        }

        public Rectangle Hitbox{
            get{return hitbox;}
        }

        public Player(Texture2D texture, Vector2 position, int pixelSize){
            this.texture = texture;
            this.position = position;
            hitbox = new Rectangle((int)position.X,(int)position.Y,
            pixelSize,(int)(pixelSize*1.5f));

        }
    public void Update(){
        newKstate = Keyboard.GetState();
       Move();
       Shoot();
        oldkState = newKstate;

       foreach(bullet b in bullets){
        b.Update();
       }
    }


    private void Shoot(){
        
        if((newKstate.IsKeyDown(Keys.Space) && oldkState.IsKeyUp(Keys.Space)) || newKstate.IsKeyDown(Keys.E)){
            bullet bullet = new bullet(texture,new(position.X + hitbox.Width/2, position.Y+30)); 
            bullets.Add(bullet);
        }
    }

    private void Move(){
    
        bool accel = false;
        if(newKstate.IsKeyDown(Keys.A)){
            position.X -= 5;
        }
        else if(newKstate.IsKeyDown(Keys.D)){
            position.X +=5;
        }
        if(newKstate.IsKeyDown(Keys.W)){
            speed += 0.3f;
            accel = true;
        }
        else if(newKstate.IsKeyDown(Keys.S)){
            speed -= 0.3f;
            accel = true;
        }

        position.Y -= speed;
        SlowDown(accel);
        
        if (speed > 20)
            speed = 20;

        if (speed < -20)
            speed = -20;

        hitbox.Location = position.ToPoint();

    }

    private void SlowDown(bool accel){
        if(accel){
            return;
        }

        if (speed > 0) 
            speed -= 0.1f;
        else if (speed < 0)
            speed += 0.1f;
    }



    public void Draw(SpriteBatch spriteBatch){
        spriteBatch.Draw(texture,hitbox,Color.White);
        foreach(bullet b in bullets){
            b.Draw(spriteBatch);
        }
    }
    }
}