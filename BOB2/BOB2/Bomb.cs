using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOB2
{
    class Bomb : PhysicalObject
    {
        private int fuse; //Hur långt det tar innan bomben sprängs
        private Texture2D[] textures;
        private int varannan;
        public Bomb(Texture2D[] _textures, Vector2 _vector, int _fuse): base(_textures[0],_vector, new Vector2(0,0),1)
        {
            if(_textures.Length < 3) //Klassen stödjer bomber med endast två textures
            {
                textures = new Texture2D[3];
                textures[1] = textures[0];
                textures[2] = textures[1];

            } else
            {
                textures = _textures;
            }
            fuse = _fuse;
            
        }

        

        public override void Update()
        {
            
            fuse--; //Räknar ned tiden till sprängning
            if(fuse < 5 && fuse > 3) //Kollar om bomben snart sprängs, i så fall ska den inte blinka längre
            {
                texture = textures[2];
                scale = 0.1f;
            } else if(fuse <= 3)
            {
                texture = textures[2];
                scale = 0.2f;
            }else
            {
                scale = 1;
                varannan++;
                if (varannan > 5 && varannan < 10) //Alternerar mellan två olika textures för att skapa en blinkande bomb
                {
                    texture = textures[0];


                }
                else if (varannan <= 5)
                {

                    texture = textures[1];
                }
                else
                {

                    varannan = 0;
                }

            }




        }
        /// <summary>
        /// Uppdaterar  och kollar om bomben ska sprängas
        /// </summary>
        /// <returns>Om bomben ska sprängas</returns>
        public bool Blast()
        {
            Update();
            if(fuse < 1)
            {
                return true;
            }
            return false;
        }
    }
}
