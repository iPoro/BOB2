using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BOB2
{
    class Steve : Motståndare
    {
        private Texture2D[] textures;
        private int count;
        private int intensitet = 7;
        private Random r = new Random();
        private Texture2D boll;
        public Steve(Texture2D[] _texture, Vector2 _vector, Vector2 _hastighet, float _scale, Boll baseBoll, Texture2D _boll, int _liv) : base(_texture[0], _vector, _hastighet, _scale, baseBoll, _liv)
        {
            textures = _texture;
            boll = _boll;
        }

        public override void drawObject(SpriteBatch sb)
        {
           
            if (lever)
            {
                //Byter mellan olika textures
                if (count > intensitet && count <= 2*intensitet)
                {
                    sb.Draw(textures[0], new Vector2(vector.X +15, vector.Y), null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                    count++;
                }else if(count <= intensitet && count <= 2*intensitet)
                {
                    count++;
                    sb.Draw(textures[1], vector, null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                 
                }
                else
                {
                    count++;
                    sb.Draw(textures[0], vector, null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                    count = 0;
                }
                foreach (Boll b in bollar)
                {
                    b.drawObject(sb);
                }
            }
           
        }

        public override void Update(Gubbe bill, List<Hinder> hinder)
        {
            if(r.Next(1,50) == 1)
            {
                if(r.Next(0,2) == 0) //Slumpar vilket håll en stor boll ska skjutas åt
                {
                    if(r.Next(0,2) == 0)
                    {
                        bollar.Add(new Boll(boll, new Vector2(vector.X - getWidth() * 2, +vector.Y - getHeight() * 2), new Vector2(-1, 0), 2.0f));
                    }
                    else
                    {
                        bollar.Add(new Boll(boll, new Vector2(vector.X - getWidth() * 2, + vector.Y - getHeight() * 2), new Vector2(0, -1), 2.0f));
                    }
                }else
                {
                    if(r.Next(0,2) == 0)
                    {
                        bollar.Add(new Boll(boll, new Vector2(vector.X - getWidth() * 2, +vector.Y - getHeight() * 2), new Vector2(1, 0), 2.0f));
                    }
                    else
                    {
                        bollar.Add(new Boll(boll, new Vector2(vector.X - getWidth() * 2, +vector.Y - getHeight() * 2), new Vector2(0, 1), 2.0f));
                    }
                }
               
            }
            
             
            base.Update(bill, hinder); //Använder basklassens rörelsealgoritm
        }
    }
}
