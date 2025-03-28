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
            int size = rand.Next(120);
            speed = rand.NextFloat(5, 100);
            position.X = rand.NextFloat(100, 650);
            position.Y = -50;
            hitbox = new ((int) position.X, (int) position.Y, size, size);
        }

        public void Update(){
            position.Y += speed*1f/50f;

            hitbox.Location = position.ToPoint();
        }

        public void Draw(SpriteBatch spriteBatch){
            spriteBatch.Draw(texture, hitbox, Microsoft.Xna.Framework.Color.White);
        }
    }
}