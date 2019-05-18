using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;

namespace ID_Efender
{
    class InvasionMeter
    {
        private Rectangle m_rect;
        private Texture2D m_txr;
        private Texture2D m_txr2;
        private int m_maxMeter;
        private float m_currMeter;
        private const float m_modifier = 1.9f;
        private float timer;
        private const float timerMax = 1f;
        private float timerIncrement = 0.1f;

        //Constructor
        public InvasionMeter(Texture2D txr, Texture2D txr2, int xpos, int ypos)
        {
            m_txr = txr;
            m_txr2 = txr2;
            m_maxMeter = m_txr.Width;
            m_currMeter = 0;
            timer = 0;

            m_rect = new Rectangle(xpos, ypos, (int)m_currMeter, m_txr.Height);
        }

        public void UpdateMe(int score)
        {
            m_currMeter = score * m_modifier;
            m_rect.Width = (int)m_currMeter % m_maxMeter;

            if (score >= 50)
            {
                timer = timer + timerIncrement;
                if (timer >= 1f || timer <= 0f)
                {
                    timerIncrement = -timerIncrement;
                }
            }
        }

        public void DrawMe(SpriteBatch sb)
        {
            sb.Draw(m_txr, m_rect, Color.White);
            sb.Draw(m_txr2, m_rect, Color.White * timer);
        }
    }
}
