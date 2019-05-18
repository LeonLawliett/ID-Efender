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
    class Bullet
    {
        //Texture
        private Texture2D m_txr;

        //Movement
        public Rectangle collisionRect;
        private Vector2 m_velocity;
        private float m_speed;
        private const int BULLETTIMER = 30;
        private int bulletActive;

        //Enums
        public enum BulletState
        {
            Active,
            Inactive
        }
        private enum Direction
        {
            Left,
            Right
        }
        public BulletState currState;
        private Direction m_direction;

        //Constructor
        public Bullet(Texture2D txr, Rectangle shipRect, PlayerShip.AnimState direction, float speed)
        {
            //Texture
            m_txr = txr;

            //Movement
            if (direction == PlayerShip.AnimState.Right)
            {
                collisionRect = new Rectangle(shipRect.X + shipRect.Width, shipRect.Y + shipRect.Height / 2, m_txr.Width, m_txr.Height);
                m_speed = speed;
                m_direction = Direction.Right;
            }
            else
            {
                collisionRect = new Rectangle(shipRect.X, shipRect.Y + shipRect.Height / 2, m_txr.Width, m_txr.Height);
                m_speed = -speed;
                m_direction = Direction.Left;
            }

            m_velocity = new Vector2(m_speed, 0);
            bulletActive = 0;

            currState = BulletState.Active;
        }

        //Update
        public void UpdateMe()
        {
            bulletActive++;
            collisionRect.X += (int)m_velocity.X;

            if (bulletActive == BULLETTIMER)
            {
                currState = BulletState.Inactive;
            }
        }

        //Draw
        public void DrawMe(SpriteBatch sb)
        {
            switch(m_direction)
            {
                case Direction.Right:
                    sb.Draw(m_txr, collisionRect, Color.White);
                    break;
                case Direction.Left:
                    sb.Draw(m_txr, collisionRect, null, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                    break;
            }
        }
    }
}
