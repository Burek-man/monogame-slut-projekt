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
            position.Y -= speed*1f/50f;

            hitbox.Location = position.ToPoint();
        }

        public void Draw(SpriteBatch spriteBatch){

        Color myColor;
        Color startColor = new Color(250, 250, 250);
        Color finalColor = new Color(231, 214, 90);
        myColor = Color.Lerp(startColor, finalColor, 0.5f);

        spriteBatch.Draw(texture, Vector2.Zero, myColor);

    }
  }
}

