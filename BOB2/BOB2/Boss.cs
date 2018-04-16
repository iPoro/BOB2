using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BOB2
{
    class Boss : Motståndare
    {
        private int count;
        private Texture2D boll;
        private Texture2D[] textures;
        public Boss(Texture2D[] _texture, Vector2 _vector, Vector2 _hastighet, float _scale, Boll baseBoll, Texture2D _boll, int _liv) : base(_texture[0], _vector, _hastighet, _scale, baseBoll,_liv)
        {
            boll = _boll;
            textures = _texture;
        }

        public override void Update(Gubbe bill, List<Hinder> hinder)
        {
            
            if (count > 50)
            {


                Vector2 playerpos = bill.getVector();
                Console.WriteLine(vector + " " + getWidth() + " " + 200);
                bollar.Add(new Boll(boll, new Vector2(vector.X + getWidth() / 2, vector.Y + getHeight() / 2), new Vector2(1, 0),0.5f));
                bollar.Add(new Boll(boll, new Vector2(vector.X + getWidth() / 2, vector.Y + getHeight() / 2), new Vector2(-1, 0),0.5f));
                bollar.Add(new Boll(boll, new Vector2(vector.X + getWidth() / 2, vector.Y + getHeight() / 2), new Vector2(0, -1),0.5f));
                bollar.Add(new Boll(boll, new Vector2(vector.X + getWidth() / 2, vector.Y + getHeight()/2), new Vector2(0, 1), 0.5f));
                
               
                count = 0;
                Console.WriteLine(bollar.Count);
            }
            count++;
            base.Update(bill, hinder);
        }
        public override void drawObject(SpriteBatch sb)
        {
            if(!getLever())
            {
                return;
            }
            if (count > 62 || count < 10) 
            {
                sb.Draw(textures[1], vector, null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
            }else
            {
                sb.Draw(texture, vector, null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
            }
           
            foreach (Boll b in bollar)
            {
                
                b.drawObject(sb);
                Console.WriteLine("Ritade boll vid: " + b.getVector());
            }
        }
    }
}
