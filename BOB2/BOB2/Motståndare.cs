using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BOB2
{
    class Motståndare : PhysicalObject, IComparable
    {
        protected int senastePromenad = 0;
        protected bool lever = true;
        protected Boll b;/// <summary>
        protected int liv;
        /// 
        /// </summary>
        /// <param name="_texture"></param>
        /// <param name="_vector"></param>
        /// <param name="_hastighet"></param>
        /// <param name="_scale"></param>
        /// <param name="baseBoll">Används som basobjekt för bollar</param>
        public Motståndare(Texture2D _texture, Vector2 _vector, Vector2 _hastighet, float _scale, Boll baseBoll, int _liv) : base(_texture, _vector, _hastighet, _scale)
        {
            liv = _liv;
            b = baseBoll;
        }

        public override void drawObject(SpriteBatch sb)
        {
            if (lever)
            {
               
                sb.Draw(texture, vector, null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                foreach(Boll b in bollar)
                {
                    b.drawObject(sb);
                }
            }
        }

      
        private float längd;

        public float getLängd()
        {
            return längd;
        }
        protected List<Boll> bollar = new List<Boll>();
        public virtual void Update(Gubbe bill,List<Hinder> hinder)
        {
            Vector2 playerpos = bill.getVector();
            Vector2 innan = vector;
            längd = (float)Math.Sqrt(Math.Pow(playerpos.X - vector.X, 2) + Math.Pow(playerpos.Y-  vector.Y, 2));
            Random r = new Random();
            if(playerpos.X == vector.Y)
            {
            }
            if (senastePromenad < 0)
            {
                senastePromenad++;
                if (vector.X - playerpos.X < 0)
                {
                    vector.X += hastighet.X;
                }
                else if (vector.X - playerpos.X > 0)
                {
                    vector.X += hastighet.X * -1;
                }
                
            }
            else if (senastePromenad > 0)
            {
                senastePromenad--;
                if (vector.Y - playerpos.Y < 0)
                {
                    vector.Y += hastighet.Y;
                }
                else if (vector.Y - playerpos.Y > 0)
                {
                    vector.Y += hastighet.Y * -1;
                }
                
            }
            else
            {


                if (r.Next(0, 2) * DateTime.Now.Ticks == 0)
                {
                    senastePromenad = -10;
                    if (vector.X - playerpos.X < 0)
                    {
                        vector.X += hastighet.X;
                    }
                    else
                    {
                        vector.X += hastighet.X * -1;
                    }

                }
                else
                {
                    senastePromenad = 10;
                    if (vector.Y - playerpos.Y < 0)
                    {
                        vector.Y += hastighet.Y;
                    }
                    else
                    {
                        vector.Y += hastighet.Y * -1;
                    }
                }
            }
            for(int i= 0; i < hinder.Count;i++)
            {
                if(hinder[i].checkCollision(this))
                {
                    vector = innan;
                }
            }
            foreach (Boll b in bollar.ToList())
            {
                if (!b.getLever())
                {
                    bollar.Remove(b);
                    
                }
                b.Update();
                if (b.checkCollision(bill))
                {
                    bill.TaSkada();
                    bollar.Remove(b);
                    
                }
            }
           
           if(liv < 1)
            {
                lever = false;
            }


        }

        public override float getHeight()
        {
            return base.getHeight() * scale;
        }

        public override float getWidth()
        {
            return base.getWidth() * scale;
        }

        public void setLever(bool a)
        {
            lever = a;
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        public bool getLever()
        {
            
            return lever;
        }

        public int CompareTo(object obj)
        {
            Motståndare m = obj as Motståndare;
           // Console.WriteLine(getLängd().CompareTo(m.getLängd()));
            return getLängd().CompareTo(m.getLängd());
         
        }

        public void TaSkada(int mängd)
        {
            liv -= mängd;
        }
    }
}
