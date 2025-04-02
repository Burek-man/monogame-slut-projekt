using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX;

namespace Monogame._2
{
    public class Enemy
    {
        private Texture2D texture;
        private Microsoft.Xna.Framework.Vector2 position;
        private Microsoft.Xna.Framework.Rectangle hitbox;
        private float speed;

        public Microsoft.Xna.Framework.Rectangle Hitbox{
            get{return hitbox;}
        }

        public Enemy(Texture2D texture){
            this.texture = texture;
            Random rand = new Random();
            speed = rand.NextFloat(20, 50);
            position.X = rand.NextFloat(100, 600);
            position.Y = -50;
            hitbox = new ((int) position.X, (int) position.Y, 100, 100);
        }

        public void Update(){
            position.Y += speed*1f/20f;

            hitbox.Location = position.ToPoint();
        }

        public void Draw(SpriteBatch spriteBatch){
            spriteBatch.Draw(texture, hitbox, Microsoft.Xna.Framework.Color.White);
        }
    }
}