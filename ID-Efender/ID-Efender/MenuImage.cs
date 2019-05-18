using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ID_Efender
{
    class MenuImage
    {
        private Vector2 m_pos;
        private string m_string;
        private Texture2D m_txr;

        //Constructor
        public MenuImage(Texture2D txr, float xpos, float ypos)
        {
            m_pos = new Vector2(xpos, ypos);
            m_txr = txr;
        }

        //Draw
        public void DrawMe(SpriteBatch sb)
        {
            sb.Draw(m_txr, m_pos, Color.White);
        }
    }
}
