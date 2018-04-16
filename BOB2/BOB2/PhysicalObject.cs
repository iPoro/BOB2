using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BOB2
{
    abstract class PhysicalObject : MovingObject
    {
        protected bool visible = true;
        public PhysicalObject(Texture2D _texture, Vector2 _vector, Vector2 _hastighet, float _scale) : base(_texture, _vector, _hastighet, _scale)
        {
        }
        /// <summary>
        /// Kollar om två objekt kolliderar med varandra
        /// </summary>
        /// <param name="p">Objekt av typen physicalobject</param>
        /// <returns>Om objekten har kolliderat</returns>
        public virtual Boolean checkCollision(PhysicalObject p)
        {
            try
            {
                if (p == null)
                {
                    return false;
                }
                Rectangle r1 = new Rectangle(Convert.ToInt32(getVector().X), Convert.ToInt32(getVector().Y), Convert.ToInt32(getWidth()), Convert.ToInt32(getHeight()));
                Rectangle r2 = new Rectangle(Convert.ToInt32(p.getVector().X), Convert.ToInt32(p.getVector().Y), Convert.ToInt32(p.getWidth()), Convert.ToInt32(p.getHeight()));
                return r1.Intersects(r2);
            }catch(Exception e)
            {
                return false;
            }
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Höjden på 
        /// </summary>
        /// <returns>Verkliga höjden på föremålet</returns>
        public override float getHeight()
        {
            return base.getHeight() * scale;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Verkliga bredden på föremålet</returns>
        public override float getWidth()
        {
            return base.getWidth() * scale;
        }
        /// <summary>
        /// Ritar föremålet så länge den är synlig
        /// </summary>
        /// <param name="sb"></param>
        public override void drawObject(SpriteBatch sb)
        {
            if(!visible)
            {
                return;
            }
            sb.Draw(texture, vector, null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);



        }


    }
}
