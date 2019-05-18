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
    class Explosion
    {
        //Enums
        public enum ExplosionState
        {
            Active,
            Inactive
        }

        //Animation
        private Texture2D m_spriteSheet;
        private Vector2 m_pos;
        private Rectangle m_animcell;
        private float m_frameTimer;
        private float m_fps;
        private const int ANIMCELLWIDTH = 46;
        public ExplosionState currstate;


        
        //Constructor
        public Explosion(Texture2D spriteSheet, Rectangle abductionShip, float fps)
        {
            m_spriteSheet = spriteSheet;
            m_pos = new Vector2(abductionShip.X, abductionShip.Y);
            m_fps = fps;
            m_animcell = new Rectangle(0, 0, ANIMCELLWIDTH, spriteSheet.Height);
            m_frameTimer = 1;
            currstate = ExplosionState.Active;
        }

        //Draw
        public void DrawMe(SpriteBatch sb, GameTime gt)
        {
            if (m_frameTimer <= 0)
            {
                m_animcell.X = (m_animcell.X + m_animcell.Width);
                if (m_animcell.X >= m_spriteSheet.Width)
                {
                    currstate = ExplosionState.Inactive;
                }
                m_frameTimer = 1;
            }
            else
            {
                m_frameTimer -= (float)gt.ElapsedGameTime.TotalSeconds * m_fps;
            }

            sb.Draw(m_spriteSheet, m_pos, m_animcell, Color.White);
        }
    }
}
