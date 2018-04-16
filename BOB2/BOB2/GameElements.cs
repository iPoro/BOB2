using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOB2
{
    static class GameElements
    {
        static Texture2D menuTexture;
        
        static Gubbe bill;
        static List<SmåMotståndare> motståndare;
        static List<Bomb> bomber;
        static SpriteFont font;
        static List<MenuItem> menu;
        static List<Boll> bollar;
        static GameObject backgrund;
        static List<Hinder> hinder;
        static Random slump;
        static Texture2D[] stentextures;
        static List<Powerup> gott;
        static Texture2D hjärta;
        static Texture2D hjärta2;
        static Boss förstaBossen;
        static List<Poäng> highscore;
        static Texture2D bakgrund;
        static Texture2D motorsåg;
        static Nyckel nyckel;
        public enum State { Run, HighScore, Quit, Menu, Loading, Skriv, Information};
        public static State state;

        public static void Initialize()
        {
            
            menu = new List<MenuItem>();
            motståndare = new List<SmåMotståndare>();
            bomber = new List<Bomb>();
            bollar = new List<Boll>();
            hinder = new List<Hinder>();
            slump = new Random();
            stentextures = new Texture2D[3];
            gott = new List<Powerup>();
            highscore = new List<Poäng>();
            information = new List<Rektangel>();
            state = State.Menu;
        }

        public static void LoadContent(ContentManager content, GameWindow window)
        {
            //Laddar alla textures som kommer användas
            highscore.Add(new Poäng("A", 10000));
            bakgrund = content.Load<Texture2D>("hög");
            motorsåg = content.Load<Texture2D>("viktigt");
            Texture2D[,] billbilder = new Texture2D[4, 2]; //Array som har alla riktningarna för spelaren
            billbilder[0, 0] = content.Load<Texture2D>("isaacfront"); billbilder[0, 1] = content.Load<Texture2D>("isaacfront_1"); billbilder[1, 0] = content.Load<Texture2D>("isaacfront"); billbilder[1, 1] = content.Load<Texture2D>("isaacfront_1"); billbilder[2, 0] = content.Load<Texture2D>("isaacright"); billbilder[2, 1] = content.Load<Texture2D>("isaacright_1"); billbilder[3, 0] = content.Load<Texture2D>("isaacleft"); billbilder[3, 1] = content.Load<Texture2D>("isaacleft_1");
            bill = new Gubbe(billbilder, new Vector2(150, 50), new Vector2(2, 2), 0.5f); //Skapar gubben man styr
            backgrund = new GameObject(content.Load<Texture2D>("BobBg"), new Vector2(window.ClientBounds.Width, window.ClientBounds.Height));
            menuTexture = content.Load<Texture2D>("bakgrund");
            font = content.Load<SpriteFont>("Comic sans");
            t2 = content.Load<Texture2D>("snurr");
            //Lägger till menyföremålen som man kan klicka på
            menu.Add(new MenuItem(new Vector2(75, window.ClientBounds.Height / 2 - 200), content.Load<Texture2D>("menu1"), "Starta", content.Load<SpriteFont>("MenuSpritw"), State.Run)); 
            menu.Add(new MenuItem(new Vector2(75, window.ClientBounds.Height / 2 - 100), content.Load<Texture2D>("menu1"), "Highscore", content.Load<SpriteFont>("MenuSpritw"), State.HighScore));
            menu.Add(new MenuItem(new Vector2(75, window.ClientBounds.Height / 2 + 100 ), content.Load<Texture2D>("menu1"), "Avsluta", content.Load<SpriteFont>("MenuSpritw"), State.Quit));
            menu.Add(new MenuItem(new Vector2(75, window.ClientBounds.Height / 2), content.Load<Texture2D>("menu1"), "Info", content.Load<SpriteFont>("MenuSpritw"), State.Information));
            stentextures[0] = content.Load<Texture2D>("sten1"); stentextures[1] = content.Load<Texture2D>("sten2"); stentextures[2] = content.Load<Texture2D>("kruka");
            hjärta = content.Load<Texture2D>("hjärta1");
            hjärta2 = content.Load<Texture2D>("hjärta2");
            nyckel = new Nyckel(content.Load<Texture2D>("key"), new Vector2(350, 200), new Vector2(0, 0), 1);
            laddahighscore();
            information.Add(new Rektangel(new Vector2(75, 75), content.Load<Texture2D>("nyckel"), "W", font,0.06f));
            information.Add(new Rektangel(new Vector2(25, 120), content.Load<Texture2D>("nyckel"), "A", font, 0.06f));
            information.Add(new Rektangel(new Vector2(75, 120), content.Load<Texture2D>("nyckel"), "S", font, 0.06f));
            information.Add(new Rektangel(new Vector2(125, 120), content.Load<Texture2D>("nyckel"), "D", font, 0.06f));
            information.Add(new Rektangel(new Vector2(25, 200), content.Load<Texture2D>("nyckel"), "Shi", font, 0.06f));
            information.Add(new Rektangel(new Vector2(75, 200), content.Load<Texture2D>("nyckel"), "ft", font, 0.06f));
            information.Add(new Rektangel(new Vector2(25, 280), content.Load<Texture2D>("nyckel"), "Sp", font, 0.06f));
            information.Add(new Rektangel(new Vector2(75, 280), content.Load<Texture2D>("nyckel"), "ace", font, 0.06f));
            information.Add(new Rektangel(new Vector2(75, 360), content.Load<Texture2D>("nyckel"), "Esc", font, 0.06f));

        }

        public static State MenuUpdate()
        {
            //Går igenom alla menyföremål 
            for(int i = 0; i < menu.Count; i++)
            {
                //Kollar om musen är över menyföremålet
                if(menu[i].hover(Mouse.GetState()))
                {
                    //Sätter menyföremålet till en annnan färg
                    menu[i].setActive(true);
                } else
                {
                    menu[i].setActive(false);
                }
                //Kollar om knappen är klickad
                if(menu[i].Clicked(Mouse.GetState()))
                {
                    if(menu[i].getState() == State.HighScore)
                    {
                        laddahighscore();
                    }
                    return menu[i].getState();
                }
            }
            return State.Menu;
        }

        public static void MenuDraw(SpriteBatch sb)
        {

            sb.Draw(menuTexture, new Vector2(0,0), null, Color.White, 0, new Vector2(0, 0), 0.8f, SpriteEffects.None, 0); //Ritar backgrundsbilden för menyn
            for (int i = 0; i < menu.Count; i++)
            {
                //Ritar ut alla menyobjekt
                menu[i].drawObject(sb);
            }
          
        }

        private static void laddahighscore()
        {
            try
            {
                //Laddar highscore
                StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + "/scores.txt");
                highscore.Clear();
                String input;
                while ((input = sr.ReadLine()) != null)
                {
                    //Läser highscore från fil och delar på tecknet &
                    highscore.Add(new Poäng(input.Split('&')[0], int.Parse(input.Split('&')[1])));
                }
                sr.Close();
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private static int cooldown;
        private static int senastepoäng;
        public static State Update(ContentManager content,GameWindow window, GameTime time)
        {
            if(bill.getLiv() <= 0) //Återställer allt om man har dött
            {
                
                
                return State.Skriv; //Byter State för att skriva in highscore
            }
            cooldown++;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                return State.Skriv;

            }
           


            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                tryckSpace(content); 
            }
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.W) && ks.IsKeyDown(Keys.A))
            {
                bill.setSpeed(new Vector2(-1, -1));
                bill.setOrientation(0);
            }
            
            else if (ks.IsKeyDown(Keys.Tab)) //Skapar ett nytt rum
            {
                nyttRum(content, time);
            } else if(ks.IsKeyDown(Keys.T)) //Gubben tar skada
            {
                hinder.Clear();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift)) //Lägger ut en bomb på platsen där 
            {
                bomber.Add(new Bomb(new Texture2D[] { content.Load<Texture2D>("bomb"), content.Load<Texture2D>("bomb_1"), content.Load<Texture2D>("explo") }, new Vector2(bill.getVector().X, bill.getVector().Y),150));
            }
            bill.Update(hinder);
            
            if(bill.VidDörr(time) && motståndare.Count == 0 && förstaBossen == null) //Kollar om man är vid en dörr och skapar ett nytt rum i så fall
            {
                bill.setNyckel(true);
                nyttRum(content,time);

                return State.Run;
            }
            if (nyckel != null && bill.checkCollision(nyckel)) //Kollar om man har plockat upp nyckeln
            {

                bill.setNyckel(true);
                nyckel.Update(bill);
            }
            // TODO: Add your update logic here
            foreach (Boll b in bollar.ToList()) //Går igenom alla bollar
            {
                b.Update(); //Uppdaterar bollarna
                if (b.checkCollision(förstaBossen)) //Kollar om bollen har kolliderat med bossen
                {
                    bollar.Remove(b); //Tar bort bollen
                    förstaBossen.TaSkada(1); //Skadar bossen
                    if (!förstaBossen.getLever()) //Kollar om en nyckel för att öppna dörrarna ska skapas
                    {
                        nyckel = new Nyckel(content.Load<Texture2D>("key"), new Vector2(window.ClientBounds.Width / 2, window.ClientBounds.Height / 2), new Vector2(0, 0), 1);
                        förstaBossen = null;
                    }
                }
                foreach (SmåMotståndare m in motståndare.ToList())
                {
                    
                    if(m.checkCollision(b)) //Kollar om bollen har kolliderat med motståndarna
                    {
                        motståndare.Remove(m); //Tar bort motståndaren
                        bollar.Remove(b);//Tar bort bollen
                    }

                   

                }
               

            }

            for (int i = 0; i < motståndare.Count; i++) //Går igenom alla motståndare och uppdaterar dem
            {
                motståndare[i].Update(bill,hinder);
               

            }

            foreach(Powerup power in gott.ToList()) //Går igenom alla powerups 
            {
                
                if(bill.checkCollision(power)) //Kollar om man gått över en powerup
                {
                    power.PickUp(bill); 
                    gott.Remove(power);
                }
            }
            motståndare.Sort(); //Sorterar motståndarna
            for (int i = 0; i < bomber.Count; i++) //Går igenom alla bomber
            {
                if (bomber[i].Blast()) //Kollar om bomben ska sprängas
                {
                    foreach(SmåMotståndare m in motståndare.ToList()) //Går igenom alla motståndare
                    {
                        if (bomber[i].checkCollision(motståndare[i])) //Kollar om motståndaren sprängs av bomben
                        {
                            motståndare.Remove(m);//Dödar motståndaren
                            
                        }
                    }
                    bomber.RemoveAt(i); //Tar bort bomben
                }
            }
            
            if (förstaBossen != null) {
                if (förstaBossen.getLever())
                {
                    förstaBossen.Update(bill, hinder);
                }
                else
                {
                    förstaBossen = null;
                }
                }
            return State.Run;
        }
        #region "Knappar&Annat"


        private static void nyttRum(ContentManager Content, GameTime gameTime) //Skapar ett nytt rum när man går igenom dörren
        {
            if(!bill.getNyckel())
            {
                return;
            }
            //Återställer allting
            nyckel = null;
            bill.setNyckel(false);
            motståndare.Clear();
            bollar.Clear();
            gott.Clear();
            bill.nyttRum();
            hinder.Clear();
            förstaBossen = null;
            bill.addLiv(1); //Lägger till ett liv som belöning när ett nytt rum skapas
            gott.Add(new Powerup(Content.Load<Texture2D>("18_a_dollar"), new Vector2(slump.Next(100, 500), slump.Next(95, 300)), new Vector2(0, 0), 1, 1)); //
            for(int i = 0; i < slump.Next(0,2); i++) //Skapar ett slumpat antal hinder
            {
                Vector2 pos = new Vector2(slump.Next(100, 500), slump.Next(95, 300)); 
                hinder.Add(new Hinder(stentextures[slump.Next(0,3)], pos, new Vector2(0, 0), slump.Next(1,10) * 0.1f)); //Skapar ett hinder med slumpad storlek och slumpad texture

            }
            if(slump.Next(0,10) == 1) //Slumpar om bossen ska visas
            {
                förstaBossen = new Boss(new Texture2D[] { Content.Load<Texture2D>("boss1"), Content.Load<Texture2D>("boss2") }, new Vector2(500, 250), new Vector2(1, 1), 1.0f, null, Content.Load<Texture2D>("boll"),10);
                
            }else
            {
                förstaBossen = null;
                for (int i = 0; i < slump.Next(1, 10) * 0.5 * (bill.getRum()+1); i++)
                {
                    motståndare.Add(new SmåMotståndare(Content.Load<Texture2D>("Fiende"), new Vector2(slump.Next(50, 540), slump.Next(50, 300)), new Vector2(2, 2), 0.4f, new Boll(Content.Load<Texture2D>("boll"), new Vector2(-10, -10), new Vector2(0, 0), 0.5f),1,Content.Load<Texture2D>("boll")));
                }

            }
            nyckel = new Nyckel(Content.Load<Texture2D>("key"), new Vector2(350, 200), new Vector2(0, 0), 1);


        }
       

        public static void tryckSpace(ContentManager content)
        {
            if (cooldown > 50)
            {
                bollar.Add(new Boll(content.Load<Texture2D>("boll"), bill.getVector(), bill.getSenaste() * 2, 0.5f));
                cooldown = 0;
            }
        }
        #endregion

        
        public static void Draw(SpriteBatch sb)
        {
            
           //Ritar ut allt
            sb.Draw(backgrund.getTexture(),new Rectangle(0,0,(int)backgrund.getVector().X,(int)backgrund.getVector().Y),Color.White);
            
            foreach (Powerup h in gott)
            {
                h.drawObject(sb);
            }
            for (int i = 0; i < motståndare.Count; i++)
            {
                motståndare[i].drawObject(sb);

            }
            for(int i = 0; i < hinder.Count;i++)
            {
                hinder[i].drawObject(sb);
            }

            for (int i = 0; i < bollar.Count; i++)
            {
                bollar[i].drawObject(sb);
            }
            for (int i = 0; i < bomber.Count; i++)
            {
                bomber[i].drawObject(sb);
            }
            bill.drawObject(sb);
            for(int i  = 0; i < bill.getLiv();i++)
            {
                sb.Draw(hjärta, new Vector2(30 + i * 35, 20), Color.White);
            }
            for(int  i = 0; i < bill.getMaxLiv() - bill.getLiv(); i++)
            {
                sb.Draw(hjärta2, new Vector2(30 + (bill.getLiv()) * 35 + i * 35, 20), Color.White);
            }
            if (förstaBossen != null) { förstaBossen.drawObject(sb); }
            sb.DrawString(font, bill.getVector().ToString(), new Vector2(200, 200), Color.Red);
            if(nyckel != null) { nyckel.drawObject(sb); }

        }
        private static float mu = 1f; 
        public static State HighScoreUpdate()
        {
            //Gör så att att alla poäng scrollas igenom
          
            if(mu < highscore.Count - 8)
            {
                //Visar fler poäng
                mu *= 1.005f;
            }
            highscore.Sort(); //Sorterar listan så högst poäng är först
            if(Keyboard.GetState().IsKeyDown(Keys.Escape)) //Om man klickar Escape återvänder man till huvudmenyn
            {
                return State.Menu;
            }
            return State.HighScore;
        }

        public static void HighScoreDraw(SpriteBatch sb)
        {
            sb.Draw(bakgrund, new Vector2(0,0), null, Color.White, 0, new Vector2(0, 0), 0.45f, SpriteEffects.None, 0); //Ritar bakgrunden
            for (int i = 0;i < highscore.Count; i++) //Går igenom alla poäng
            {
                if(150 + 50 * i - mu*50 > 100 && 100+50*i-mu*50 < 350) //Kollar om poängen hamnar i fönstret och är synligt
                {
                    sb.DrawString(font, highscore[i].getNamn() + " - " + highscore[i].getPoäng(), new Vector2(150, 100 + 50 * i - mu * 50), Color.DarkSlateGray); //Skriver ut namnet och poängen
                }
              
            }
        }
        public static float rotation;
        static Texture2D t2;
        //LoadingUpdate och LoadingDraw var ett koncept på en laddningsskärm men används inte
        public static State LoadingUpdate(ContentManager content,GameTime time)
        {
            
            rotation += (float)Math.PI /24;

            Console.WriteLine(time.ElapsedGameTime.Seconds);
            if(bill.getLoading() + 5 < time.TotalGameTime.Seconds)
            {
                nyttRum(content,time);

                return State.Run;
            }
            return State.Loading;

        }
        //Används inte
        public static void LoadingDraw(SpriteBatch sb)
        {
            sb.Draw(t2, new Vector2(385, 255), null, Color.White, (float)(rotation), new Vector2(t2.Width / 2, t2.Height / 2), 0.4f, SpriteEffects.None, 1);
           
            sb.DrawString(font, "Laddar - " + bill.getRum() + " : " + bill.getLoading(), new Vector2(250, 150), Color.White);
        }


        static StringBuilder builder = new StringBuilder();
        
        static Keys senaste;
        public static State SkrivUpdate()
        {
            
            KeyboardState k = Keyboard.GetState(); //Hämtar tangentbordet
            if(k.IsKeyDown(Keys.Enter)) //Kollar om enter-knappen är nedtryckt
            {
                highscore.Add(new Poäng(builder.ToString(), bill.getRum())); //Lägger till namn och poäng i ett nytt föremål
                bollar.Clear();
                bomber.Clear();
                motståndare.Clear();
                hinder.Clear();
                bill.Clear();
                bill.setNyckel(false);
                förstaBossen = null;
                builder = new StringBuilder(); //Återställer StringBuilder så att den kan återanvändass
                StreamWriter sw = new StreamWriter(Directory.GetCurrentDirectory()+ "/scores.txt");
                foreach(Poäng p in highscore)
                {
                    sw.WriteLine(p.ToString());
                }
                sw.Close();
                laddahighscore();
                return State.HighScore; //Visar highscores
            }
            Keys[] knappar = k.GetPressedKeys(); //Hämtar alla nedtryckta knappar
            if(knappar.Length < 1 || (senaste != Keys.None && !k.IsKeyUp(senaste))) //Om inga knappar eller man inte släppt upp den förra knappen behövs inget mer göras
            {
                if(k.IsKeyUp(senaste))
                {
                    senaste = Keys.None;
                }
                return State.Skriv;
            }

            if (knappar[0].ToString().Length == 1) //Kollar om längden på knappen är 1. Endast A-Z och 0-9 ska gå att trycka. Om esc trycks ned skulle ESC skrivas ut
            {
                
                senaste = knappar[0];
                builder.Append(knappar[0].ToString());
                
            } else if(knappar[0] == Keys.Space)//Eftersom mellanslag ger texten SPACE måste den ändras till ett mellanrum
            {
                senaste = knappar[0];
                builder.Append(" ");
               

            }else if(knappar[0] == Keys.Back)//Eftersom mellanslag ger texten REMOVE(längre än 1 bokstav långt) måste den kollas direkt
            {
                senaste = Keys.Back;
                builder.Remove(builder.Length - 1, 1); //Tar bort den senaste bokstaven
               
            }
            return State.Skriv;
        }

        public static void SkrivDraw(SpriteBatch sb)
        {
            sb.DrawString(font, builder.ToString(), new Vector2(200, 200), Color.White); //Skriver ut namnet man skrivit in

            sb.Draw(motorsåg, new Vector2(50,50), null, Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0); //Ritar ut en bild så det ser lite mer spännande ut
        }
        static List<Rektangel> information;
        public static State InformationUpdate()
        {
            KeyboardState ks = Keyboard.GetState();
            if(ks.IsKeyDown(Keys.Escape))
            {
                return State.Menu; //Återvänder till huvudmenyn
            }
            return State.Information;
        }

        public static void InformationDraw(SpriteBatch sb)
        {
            sb.Draw(menuTexture, new Vector2(0, 0), null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0); //Ritar backgrundsbilden för menyn
            foreach (Rektangel rek in information) //Ritar ut alla rektanglar
            {
                rek.drawObject(sb);
            }
            //Text som visar hur man spelar. Varje text har tillhörande knappar som visar hur man spelar
            sb.DrawString(font, "Flytta Billy Gates", new Vector2(200, 120), Color.Wheat);
            sb.DrawString(font, "Placera en bomb", new Vector2(200, 200), Color.Wheat);
            sb.DrawString(font, "Skjut", new Vector2(200, 280), Color.Wheat);
            sb.DrawString(font, "Tillbaka till menyn", new Vector2(200, 360), Color.Wheat);
            
        }



        
    }
}
