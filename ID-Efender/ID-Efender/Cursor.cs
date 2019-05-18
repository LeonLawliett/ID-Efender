using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID_Efender
{
    class Cursor
    {
        //Enums
        public enum CursorState
        {
            Play,
            Exit
        }

        private Texture2D m_txr;
        private Vector2 m_playPos;
        private Vector2 m_exitPos;
        private Vector2 m_pos;
        public CursorState currState;

        //Constructor
        public Cursor(Texture2D txr, Vector2 playPos, Vector2 exitPos)
        {
            m_txr = txr;
            m_playPos = new Vector2(playPos.X - m_txr.Width - 10, playPos.Y);
            m_exitPos = new Vector2(exitPos.X - m_txr.Width - 10, exitPos.Y);
            currState = CursorState.Play;
            m_pos = m_playPos;
        }

        public void UpdateMe(GamePadState pad1, GamePadState oldpad)
        {
            if (pad1.DPad.Up == ButtonState.Pressed && oldpad.DPad.Up == ButtonState.Released)
            {
                if (currState == CursorState.Play)
                {
                    m_pos = m_exitPos;
                    currState = CursorState.Exit;
                }
                else
                {
                    m_pos = m_playPos;
                    currState = CursorState.Play;
                }
            }
            else if (pad1.DPad.Down == ButtonState.Pressed && oldpad.DPad.Down == ButtonState.Released)
            {
                if (currState == CursorState.Play)
                {
                    m_pos = m_exitPos;
                    currState = CursorState.Exit;
                }
                else
                {
                    m_pos = m_playPos;
                    currState = CursorState.Play;
                }
            }
        }

        public void DrawMe(SpriteBatch sb)
        {
            sb.Draw(m_txr, m_pos, Color.White);
        }
    }
}
