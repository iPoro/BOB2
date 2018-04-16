using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOB2
{
    class MenuItem : Rektangel
    {

        
        private GameElements.State state;
 

        public MenuItem(Vector2 _pos, Texture2D _texture, String _text, SpriteFont _font, GameElements.State returnState) : base(_pos, _texture,_text,_font,1)
        {
           
            state = returnState;
        }

        
        /// <summary>
        /// Kollar om musen klickat på knappen
        /// </summary>
        /// <param name="ms">Nuvarande mousestate</param>
        /// <returns>Om musen har klickat på knappen</returns>
        public Boolean Clicked(MouseState ms)
        {
            if (getVector().X < ms.X && getVector().X + rect.Width > ms.X && getVector().Y < ms.Y && getVector().Y + rect.Height > ms.Y)
            {
                
                if (ms.LeftButton == ButtonState.Pressed)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Kollar om musen är över knappen
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public Boolean hover(MouseState ms)
        {
            if (getVector().X < ms.X && getVector().X + rect.Width > ms.X && getVector().Y < ms.Y && getVector().Y + rect.Height > ms.Y)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Ändrar färgen på knappen
        /// </summary>
        /// <param name="value">True = blå färg</param>
        public void setActive(Boolean value)
        {
            if(value)
            {
                färg = Color.DarkSlateBlue;
            }else
            {
                färg = Color.White;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Texten på knappen</returns>
        public String getText()
        {
            return text;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>GameElements.State</returns>
        public GameElements.State getState()
        {
            return state;
        }

    }
}
