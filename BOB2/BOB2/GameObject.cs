using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOB2
{
    class GameObject
    {
        protected Texture2D texture;
        protected Vector2 vector;

        public GameObject(Texture2D _texture, Vector2 _vector)
        {
            texture = _texture;
            vector = _vector;
        }

        public GameObject(Texture2D[] _textures, Vector2 _vector)
        {
            texture = _textures[0];
            vector = _vector;
        }

        public Texture2D getTexture()
        {
            return texture;
        }

        public virtual void drawObject(SpriteBatch sb)
        {
           
            sb.Draw(texture, vector, Color.White);
            
        }

        public virtual Vector2 getVector()
        {
            return vector;
        }

        public virtual float getWidth()
        {
            return texture.Width;
        }

        public virtual float getHeight()
        {
            return texture.Height;
        }
    }
}
