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
    class Particles
    {
        //Enums
        public enum ParticleState
        {
            Active,
            Inactive
        }

        //Texture
        private Texture2D m_txr;

        //Movement
        public ParticleState currstate;
        private Vector2 m_pos;
        private Vector2 m_velocity;
        private const float m_DRAG = 0.99f;
        private const int m_MAXAGE = 70;
        private int age;

        //Constructor
        public Particles(Texture2D txr, Rectangle abductionShip, Random RNG)
        {
            //Texture
            m_txr = txr;

            //Movement
            m_pos = new Vector2(abductionShip.X + abductionShip.Width / 2, abductionShip.Y + abductionShip.Height / 2);
            age = 0;
            currstate = ParticleState.Active;

            ///<summary> Picks a random direction and velocity for each particle</summary>
            int rightorleft = RNG.Next(0, 2);
            int upordown = RNG.Next(0, 2);
            float xVelocity = RNG.Next(1, 10);
            float yVelocity = RNG.Next(1, 10);

            if (rightorleft == 0)
            {
                m_velocity.X = xVelocity;
            }
            else
            {
                m_velocity.X = -xVelocity;
            }

            if (upordown == 0)
            {
                m_velocity.Y = yVelocity;
            }
            else
            {
                m_velocity.Y = -yVelocity;
            }
        }

        //Update
        public void UpdateMe()
        {
            age++;
            if (age >= m_MAXAGE)
            {
                currstate = ParticleState.Inactive;
            }

            ///<summary> Slowing the particles down </summary>
            m_velocity.X = m_velocity.X * m_DRAG;
            m_velocity.Y = m_velocity.Y * m_DRAG;

            m_pos += m_velocity;
        }

        public void DrawMe(SpriteBatch sb)
        {
            sb.Draw(m_txr, m_pos, Color.White);
        }
    }
}
