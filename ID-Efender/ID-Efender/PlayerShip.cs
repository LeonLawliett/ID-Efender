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
    class PlayerShip
    {
        //Position/Movement stuff
        public Rectangle collisionrect;
        private Vector2 m_velocity;
        private float m_speed;

        //Animation Stuff
        //Direction enum for p and texture
        public enum AnimState
        {
            Left,
            Right
        }
        Texture2D m_spriteSheet;
        public AnimState currState;
        private Rectangle m_animcell;
        private float m_frameTimer;
        private float m_fps;
        private static int WIDTH = 75;

        //Constructor
        public PlayerShip(Texture2D spritesheet, float fps, int xpos, int ypos, float speed)
        {
            //Animation
            currState = AnimState.Right;
            m_spriteSheet = spritesheet;
            m_animcell = new Rectangle(0, 0, WIDTH, m_spriteSheet.Height);
            m_frameTimer = 1;
            m_fps = fps;

            //Movement
            collisionrect = new Rectangle(xpos, ypos, WIDTH, m_spriteSheet.Height);
            m_speed = speed;
            m_velocity = Vector2.Zero;
        }

        //Update
        public void UpdateMe(GamePadState pad1)
        {
            //Movement
            m_velocity.X = 0;
            m_velocity.Y = 0;


            if (pad1.DPad.Up == ButtonState.Pressed)
            {
                m_velocity.Y = -m_speed;
            }
            else if (pad1.DPad.Down == ButtonState.Pressed)
            {
                m_velocity.Y = m_speed;
            }

            if (pad1.DPad.Right == ButtonState.Pressed)
            {
                m_velocity.X = m_speed;
                currState = AnimState.Right;
            }
            else if (pad1.DPad.Left == ButtonState.Pressed)
            {
                m_velocity.X = -m_speed;
                currState = AnimState.Left;
            }

            collisionrect.X += (int)m_velocity.X;
            collisionrect.Y += (int)m_velocity.Y;
        }
        
        //Draw
        public void DrawMe(SpriteBatch sb, GameTime gt)
        {
            //Animation
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

            //Directional state machine
            switch (currState)
            {
                case AnimState.Right:
                    sb.Draw(m_spriteSheet, collisionrect, m_animcell, Color.White);
                    break;
                case AnimState.Left:
                    sb.Draw(m_spriteSheet, new Vector2(collisionrect.X, collisionrect.Y), m_animcell, Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
                    break;
            }
        }
    }
}
