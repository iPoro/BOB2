using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOB2
{
    abstract class MovingObject : GameObject
    {
        protected Vector2 hastighet;
        protected float scale;




        public MovingObject(Texture2D _texture, Vector2 _vector, Vector2 _hastighet, float _scale) : base(_texture, _vector)
        {
            scale = _scale;
            hastighet = _hastighet;
        }
        

       

        public abstract void Update();
    }
}
