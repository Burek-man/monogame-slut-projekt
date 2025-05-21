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


 

        public Rectangle Hitbox{
            get{return hitbox;}
        }

        public Player(Texture2D texture, Vector2 position, int pixelSize){
            this.texture = texture;
            this.position = position;
            hitbox = new Rectangle((int)position.X,(int)position.Y,
            pixelSize,(int)(pixelSize*1.5f));

        }
    public void Update(GameTime gameTime){
            newKstate = Keyboard.GetState();
            Move();
            oldkState = newKstate;
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
        }
    }
}
