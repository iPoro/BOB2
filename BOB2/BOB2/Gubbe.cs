using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BOB2
{
    class Gubbe : PhysicalObject
    {
        
        private Texture2D[,] textures; //Innehåller alla olika bilder
        private int orientation = 2; //Vilket håll personen kollar åt
        private float redbull = 5; //Hastighet
        private Vector2 senasteRiktning = new Vector2(0, 1);
        private int plats = 0;
        private int liv = 4;
       
        private int maxliv = 15;
        public Gubbe(Texture2D[,] _textures, Vector2 _vector, Vector2 _hastighet, float _scale) : base(_textures[0,0],_vector, _hastighet, _scale)
        {
            textures = _textures;
           
        }
        
       

      
        /// <summary>
        /// Bestämmer hållet personen tittar åt
        /// </summary>
        /// <param name="håll">Int med värdet 0-3</param>
        public void setOrientation(int håll)
        {
            
            if(håll < 0 || håll >= textures.Length)
            {
                
                texture = textures[0,0];

                return;
            }
            
           
            orientation = håll;
        }
        
        public void Update(List<Hinder> hinder)
        {
            
            if(senasteskada > 0) //Gör så att man bara kan ta skada en gång varje 30 ticks
            {
                senasteskada--;
            }
            Vector2 båda = vector + hastighet; //Lagrar de nya koordinaterna i en temporär variabel
            if(båda.X < 90 || båda.X > 640 || båda.Y < 30 || båda.Y > 340)
            {
                //Om de nya koordinaterna sätter personen utanför skärmen händer inget
            } else
            {
                vector += hastighet; //Uppdaterar positionen
                for(int i = 0; i < hinder.Count; i++)
                {
                    if(hinder[i].checkCollision(this)) //Kollar om den här klassen kolliderar med något hinder
                    {
                        vector -= hastighet; //Ångra rörelsen
                    }
                }
            }
           
            if (hastighet != new Vector2(0,0)){ //Kollar om det finns någon hastighet
                plats++;
                if (plats > 5 && plats < 10) //Varierar mellan två bilder för att skapa en animation
                {
                    texture = textures[orientation, 1]; //Orientation är vilket håll personen kollar
                    
                    
                } else if(plats <= 5)
                {
                    
                    texture = textures[orientation, 0];
                } else
                {
                   
                    plats = 0;
                }
                
            } else
            {
                texture = textures[orientation, 0];
            }
            //Kollar vilka knappar som är nedtrycka
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                tryckA();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                tryckW();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                tryckS();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                tryckD();
            }
            else
            {
                setSpeed(new Vector2(0, 0));
            }
            

        }
        private List<Bomb> bomber = new List<Bomb>();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Vector2 getSenaste()
        {
            return senasteRiktning;
        }
        /// <summary>
        /// Bestämmer hastigheten
        /// </summary>
        /// <param name="speed"></param>
        public void setSpeed(Vector2 speed)
        {
            hastighet = speed * redbull;
            if(speed != new Vector2(0,0))
            {
                senasteRiktning = speed;
            }
            
        }
        private int senasteskada = 0;
        public void TaSkada()
        {
            if (liv > 0 && senasteskada == 0) //Kollar om man är död och hur länge sedan man tog skada
            {
                liv--;
                senasteskada = 30; //Återställer så man endast kan ta skada en gång varje 0.5 sekunder
            }
        }
        /// <summary>
        /// Ger antal liv där liv alltid är större än eller lika med 0
        /// </summary>
        /// <returns>Antal liv</returns>
        public int getLiv()
        {
            return liv;
        }
        private int loadingstart;
        //Kollar om man är vid dörren
        public Boolean VidDörr(GameTime gt)
        {
            if (95 > vector.X && 30 < vector.X && vector.Y > 145 && vector.Y < 210)
            {
                rum++; 
                loadingstart = gt.TotalGameTime.Seconds;
                nyckel = false;
                return true;
            } else if (390 > vector.X && 350 < vector.X && vector.Y > 320 && vector.Y < 360) {
                rum++;
                loadingstart = gt.TotalGameTime.Seconds;
                nyckel = false;
                return true;
            }else if(660 > vector.X && 620 < vector.X && vector.Y > 145 && vector.Y < 210) {
                rum++;
                loadingstart = gt.TotalGameTime.Seconds;
                nyckel = false;
                return true; 
            } 
            return false;
            
        }
        private Boolean nyckel = false;
        public void setNyckel(Boolean har)
        {
            nyckel = har;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>True om nyckeln är upplockad och dörrar går att öppna</returns>
        public Boolean getNyckel()
        {
            return nyckel;
        }

        private int rum;

        public int getLoading()
        {
            return loadingstart;
        }/// <summary>
        /// 
        /// </summary>
        /// <returns>Nuvarande nivå</returns>
        public int getRum()
        {
            return rum;
        }
        
        public void nyttRum()
        {
            vector = new Vector2(150, 70); //Återställer positionen till vänstra hörnet
        }

        public void addRedbull(float r)
        {
            redbull += r; //Gör gubben snabbare
        }

        public void addLiv(int mängd) //Lägger till liv
        {
            if(liv+ mängd >= maxliv) //Kollar om man har maximala antalet liv
            {
                liv = maxliv;
                return;
            }
            liv += mängd;
        }
        public int getMaxLiv()
        {
            return maxliv;
        }

        public void Clear() //återställer alla variabler
        {
            nyttRum();
            liv = 3;
            rum = 0;
            redbull = 5;
            rum = 0;
        }
        //Ändrar vilket håll man kollar åt
        private void tryckW()
        {
            setSpeed(new Vector2(0, -1));
            setOrientation(0);
        }

        private void tryckA()
        {
            setSpeed(new Vector2(-1, 0));
            setOrientation(3);
        }

        private  void tryckS()
        {
            setSpeed(new Vector2(0, 1));
            setOrientation(1);
        }

        private  void tryckD()
        {
            setSpeed(new Vector2(1, 0));
            setOrientation(2);
        }



    }
}
