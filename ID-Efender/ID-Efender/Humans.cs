using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ID_Efender
{
    class Humans
    {
        //Textures
        private Texture2D m_human0;
        private Texture2D m_human1;
        private int txrpick;

        //Movement
        public Rectangle collisionrect;
        private float m_speed;
        private Vector2 m_velocity;
        private Rectangle m_screensize;

        //Constructor
        public Humans(Texture2D txr1, Texture2D txr2, Random RNG, Rectangle screensize)
        {
            //Textures
            m_human0 = txr1;
            m_human1 = txr2;
            txrpick = RNG.Next(0, 2);

            //Movement
            m_screensize = screensize;
            collisionrect = new Rectangle(RNG.Next(0, screensize.Width - m_human1.Width), screensize.Height - m_human1.Height, m_human1.Width, m_human1.Height);
            m_speed = RNG.Next(1, 6);
            m_velocity = new Vector2(m_speed, 0);
        }

        //Update
        public void UpdateMe()
        {
            if (collisionrect.X + collisionrect.Width >= m_screensize.Width)
            {
                m_velocity.X = -m_speed;
            }
            else if (collisionrect.X <= 0)
            {
                m_velocity.X = m_speed;
            }

            collisionrect.X += (int)m_velocity.X;
        }

        public void DrawMe(SpriteBatch sb)
        {
            if (txrpick == 0)
            {
                sb.Draw(m_human0, collisionrect, Color.White);
            }
            else
            {
                sb.Draw(m_human1, collisionrect, Color.White);
            }
        }
    }
}
