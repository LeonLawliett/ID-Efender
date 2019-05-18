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
    class AbductionShip
    {
        //Texture
        private Texture2D m_txr;

        //Collision/Movement
        public Rectangle collisionrect;
        public Rectangle abductionrect;
        private Rectangle m_screensize;
        private Vector2 m_velocity;
        private float m_speed;
        public enum ShipState
        {
            Descending,
            Landed,
            Ascending,
            Left
        }
        public ShipState currstate;

        //Constructor
        public AbductionShip(Texture2D txr, Random RNG, Rectangle screensize, float speed)
        {
            currstate = ShipState.Descending;

            m_txr = txr;

            m_screensize = screensize;

            collisionrect = new Rectangle(RNG.Next(0, m_screensize.Width - txr.Width), 0, m_txr.Width, m_txr.Height);
            abductionrect = new Rectangle(collisionrect.X, collisionrect.Y + collisionrect.Height / 2, collisionrect.Width / 4, collisionrect.Height / 2);

            m_velocity = Vector2.Zero;
            m_speed = speed;
        }

        //Update
        public void UpdateMe()
        {
            switch(currstate)
            {
                case ShipState.Descending:
                    m_velocity.Y = m_speed;
                    if (collisionrect.Y + m_txr.Height == m_screensize.Height)
                    {
                        currstate = ShipState.Landed;
                    }
                    break;
                case ShipState.Landed:
                    m_velocity.Y = 0;
                    break;
                case ShipState.Ascending:
                    m_velocity.Y = -m_speed;
                    if (collisionrect.Y + collisionrect.Height < 0)
                    {
                        currstate = ShipState.Left;
                    }
                    break;
            }

            collisionrect.Y += (int)m_velocity.Y;

            abductionrect.Y = collisionrect.Y + collisionrect.Height / 2;
            abductionrect.X = collisionrect.X;
        }

        //Draw
        public void DrawMe(SpriteBatch sb)
        {
            sb.Draw(m_txr, collisionrect, Color.White);
        }
    }
}
