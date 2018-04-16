using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BOB2
{
    class Hinder : PhysicalObject
    {
        public Hinder(Texture2D _texture, Vector2 _vector, Vector2 _hastighet, float _scale) : base(_texture, _vector, _hastighet, _scale)
        {
            
        }
    }
}
