using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ID_Efender
{
    class Logo
    {
        //Animation
        private Texture2D m_spriteSheet;
        private Vector2 m_pos;
        private Rectangle m_animcell;
        private float m_frameTimer;
        private float m_fps;
        private const int ANIMCELLWIDTH = 572;

        //Constructor
        public Logo(Texture2D spriteSheet, float xpos, float ypos, float fps)
        {
            m_spriteSheet = spriteSheet;
            m_pos = new Vector2(xpos, ypos);
            m_fps = fps;
            m_animcell = new Rectangle(0, 0, ANIMCELLWIDTH, spriteSheet.Height);
            m_frameTimer = 1;
        }

        //Draw
        public void DrawMe(SpriteBatch sb, GameTime gt)
        {
            //Moving animrect
            if (m_frameTimer <= 0)
            {
                m_animcell.X = (m_animcell.X + m_animcell.Width);
                if (m_animcell.X >= m_spriteSheet.Width)
                {
                    m_animcell.X = 0;
                }
                m_frameTimer = 1;
            }
            else
            {
                m_frameTimer -= (float)gt.ElapsedGameTime.TotalSeconds * m_fps;
            }

            //Draw
            sb.Draw(m_spriteSheet, m_pos, m_animcell, Color.White);
        }
    }
}
