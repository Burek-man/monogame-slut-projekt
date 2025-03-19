using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;


namespace spaceshhoter
{
    public class bullet
    {
        private Texture2D texture;
        private Vector2 position;
        private Rectangle hitbox;

        public Rectangle Hitbox{
            get{return hitbox;}
        }

        public bullet(Texture2D texture, Vector2 spawnposition){
            this.texture = texture;
            position = spawnposition;
            hitbox = new Rectangle((int)position.X,(int)position.Y,10,10);
        }

        public void Update(){
            float speed =50;
            position.Y -= speed*1f/60f;

            hitbox.Location = position.ToPoint();
        }

        public void Draw(SpriteBatch spriteBatch){
        spriteBatch.Draw(texture,hitbox,Color.SeaGreen);
 
    }
  }
}

