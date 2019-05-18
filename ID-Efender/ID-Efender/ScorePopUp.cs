using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID_Efender
{
    class ScorePopUp
    {
        public enum ScoreState
        {
            Active,
            Inactive
        }
        public ScoreState currState;

        private SpriteFont m_font;
        private string m_string;
        private Vector2 m_pos;
        private Color m_color;
        private int age;
        private const int MAXAGE = 30;
        private float m_speed;
        private Vector2 m_velocity;

        //Constructor
        public ScorePopUp(string txt, SpriteFont font, float xpos, float ypos, Color color, float speed)
        {
            m_pos = new Vector2(xpos + 26, ypos);
            m_string = txt;
            m_font = font;
            m_color = color;
            age = 0;
            m_speed = speed;
            m_velocity = new Vector2(0, m_speed);

            currState = ScoreState.Active;      
        }

        public void UpdateMe()
        {
            age++;
            m_pos -= m_velocity;

            if (age >= MAXAGE)
            {
                currState = ScoreState.Inactive;
            }
        }

        //Draw
        public void DrawMe(SpriteBatch sb)
        {
            sb.DrawString(m_font, m_string, m_pos, m_color);
        }
    }
}
