using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BOB2
{
    class SmåMotståndare : Motståndare
    {
        int cooldown = 50;
        Texture2D bollT;
        public SmåMotståndare(Texture2D _texture, Vector2 _vector, Vector2 _hastighet, float _scale, Boll baseBoll, int _liv, Texture2D boll) : base(_texture, _vector, _hastighet, _scale, baseBoll,_liv)
        {
            bollT = boll;
        }

        public override void Update(Gubbe bill, List<Hinder> hinder)
        {
            cooldown--;
            Vector2 playerpos = bill.getVector(); //Kollar var huvudkaraktären är
            if (senastePromenad < 0)
            {
                if (vector.X - playerpos.X < 2 && vector.X - playerpos.X > -2) //Kollar om den står i x-led med spelaren
                {
                    senastePromenad = 0;
                    if(cooldown < 1)
                    {
                        if (vector.X - playerpos.X != 0)
                        {
                            bollar.Add(new Boll(bollT, vector, new Vector2(0, 1) * -(vector.X - playerpos.X) / (Math.Abs(vector.X - playerpos.X)), 0.5f));   //Kollar vilken riktning bollen ska ha
                        
                        cooldown = 0;
                        }
                    }
                    else
                    {
                        
                    }
                   
                  
                }
            } else if(senastePromenad > 0)
            {
                if (vector.Y - playerpos.Y < 2 && vector.Y - playerpos.Y > -2) //Kollar om motståndaren står på samma y-led som spelaren
                {
                    
                    if (vector.X - playerpos.X != 0)
                    {
                        senastePromenad = 0;
                        if (cooldown < 1)
                        {
                            bollar.Add(new Boll(bollT,vector, new Vector2(1, 0) * -(vector.X - playerpos.X) / (Math.Abs(vector.X - playerpos.X)),0.5f));  //Kollar vilken riktning bollen ska ha
                            cooldown = 0;
                          
                        }
                        else
                        {
                            
                        }
                       

                    }
                }
            }

            
            base.Update(bill, hinder); //Använder basklassens rörelsealgoritm
        }
    }
}
