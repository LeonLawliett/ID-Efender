using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID_Efender
{
    class MenuText
    {
        public Vector2 pos;
        private string m_string;
        private SpriteFont m_font;
        private Color m_color;

        //Constructor
        public MenuText(string txt, SpriteFont font, float xpos, float ypos, Color color)
        {
            pos = new Vector2(xpos, ypos);
            m_string = txt;
            m_font = font;
            m_color = color;
        }

        //Draw
        public void DrawMe(SpriteBatch sb)
        {
            sb.DrawString(m_font, m_string, pos, m_color);
        }
    }
}
