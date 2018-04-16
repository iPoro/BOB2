using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOB2
{
    class Boll : PhysicalObject
    {

        private float redbull = 4; // Hastigheten på bollen

        public Boll(Texture2D _texture, Vector2 _vector, Vector2 _hastighet, float _scale) : base(_texture, _vector, _hastighet, _scale)
        {
            //Sätter var bollen ska starta. Endast 4 riktningar är giltiga: höger,vänster,upp,ned
            if (hastighet == new Vector2(1, 0))//Riktning höger 
            {
                vector += new Vector2(getWidth() / 2, getHeight() / 2);
            }
            else if (hastighet == new Vector2(0, 1)) //Riktning ned
            {
                vector += new Vector2(getWidth() / 2, getHeight());
            }
            else if (hastighet == new Vector2(0, -1)) //Riktning upp
            {
                vector += new Vector2(getWidth() / 2, 0);
            }
            else if (hastighet == new Vector2(-1, 0)) //Riktning vänster
            {
                vector += new Vector2(0, getHeight() / 2);
            }
            if (hastighet == new Vector2()) //Ingen hastighet
            {
                hastighet = new Vector2(0, 0);
            }

        }


        /// <summary>
        /// Bestämmer om bollen lever och i så fall om den ska ritas
        /// </summary>
        /// <param name="lever"></param>
        public void setLever(bool lever)
        {
            visible = lever;
        }

        /// <summary>
        /// Om bollen ritas ut och om den lever
        /// </summary>
        /// <returns></returns>
        public bool getLever()
        {
            return visible;
        }

        private int offset = 10; //Hur långt ifrån väggen den ska förstöras
        /// <summary>
        /// Uppdaterar bollen och dess position
        /// </summary>
        public override void Update()
        {
            Vector2 båda = vector + hastighet * redbull; //Flyttar bollen i en temporär variabel

            if (båda.X < 90 - offset || båda.X > 640 + offset || båda.Y < 30 - offset || båda.Y > 340 + offset * 3) //Kollar om bollen hamnar utanför vid nästa uppdatering
            {
                visible = false; //Bollen är utanför rummet och visas inte längre
            }
            else
            {
                vector += hastighet * redbull; //Flyttar bollen
                visible = true;
            }
        }




    }
}
