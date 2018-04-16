using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BOB2
{
    class Powerup : PhysicalObject
    {
        private float redbull;
        public Powerup(Texture2D _texture, Vector2 _vector, Vector2 _hastighet, float _scale, float redbull) : base(_texture, _vector, _hastighet, _scale)
        {
            this.redbull = redbull;
        }
        //Lägger till så spelaren blir snabbare
        public void PickUp(Gubbe b)
        {
            b.addRedbull(redbull);
        }

       
    }
}
