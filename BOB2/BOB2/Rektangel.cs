using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BOB2
{
    class Rektangel : GameObject
    {
        protected Color färg = Color.White;
        protected Rectangle rect;
        protected String text;
        protected SpriteFont font;
        private float scale;
        public Rektangel(Vector2 _pos, Texture2D _texture, String _text, SpriteFont _font, float _scale) : base(_texture, _pos)
        {
            font = _font;
            rect = new Rectangle((int)_pos.X, (int)_pos.Y, (int)(_texture.Width * _scale), (int)(_texture.Height*_scale));
            text = _text;
            scale = _scale;
        }

        public override void drawObject(SpriteBatch sb)
        {
            sb.Draw(texture, vector, null, färg, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0); 

            sb.DrawString(font, text, new Vector2(vector.X + (rect.Width / 13), vector.Y + (3*rect.Height / 13)), Color.Black); //Skriver ut text på texture som ritats tidigare
        }
    }
}
