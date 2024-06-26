using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FInal_Summative
{
    internal class Player
    {
        
        private List<Texture2D> _boyTextures;

        private Rectangle _location;

        private Vector2 _velocity;
        private Vector2 _position;
        private SpriteEffects _spriteEffect;

        private int playerIndex;

        bool hasJumped;



        public Player(List<Texture2D> boyTexture, Vector2 newPosition)
        {
            _boyTextures = boyTexture;
            _position = newPosition;
            hasJumped = true;
            _location = new Rectangle(newPosition.ToPoint(), new Point(15, 50));
            _position = new Vector2(0,400);
            _spriteEffect = SpriteEffects.None;
        }

        public void Update(GameTime gameTime, List<Rectangle> barriers, List <Rectangle> coins, List <Rectangle> door, List <Rectangle> lava, List <Rectangle> teleporters)
        {
            playerIndex = 0;

            // Horizontal Movement
            _velocity.X = 0;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                _velocity.X += 3f;
                playerIndex = 2;


            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                _velocity.X += -3f;
                playerIndex = 2;

            }


            if (_velocity.X < 0)// && _velocity.Y == 0)
            {
                _spriteEffect = SpriteEffects.FlipHorizontally;
            }
            if (_velocity.X > 0)// && _velocity.Y == 0)
            {
                _spriteEffect = SpriteEffects.None;
            }

            if (_velocity.Y < 0 && _velocity.X < 0)
            {
                playerIndex = 1;
            }
            if (_velocity.Y < 0 && _velocity.X > 0)
            {
                playerIndex = 1;
            }
          

            _position.X += _velocity.X;
            _location.Location = _position.ToPoint();

            foreach (Rectangle barrier in barriers)
                if (_location.Intersects(barrier))
                {
                    _position.X -= _velocity.X;
                    _location.Location = _position.ToPoint();
                    break;
                }

            for (int i = 0; i < coins.Count; i++)
            {                 
                if (_location.Intersects(coins[i]))
                {
                    coins.RemoveAt(i);
                    i--;
                }
            }




            // Vertical Movement

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && hasJumped == false)
            {
                _velocity.Y = -5f;
                hasJumped = true;
            }
            _velocity.Y += 0.20f;

                _position.Y += _velocity.Y;
            _location.Location = _position.ToPoint();

            foreach (Rectangle lavaRect in lava)
                if (_location.Intersects(lavaRect))
                {
                    if (_velocity.Y < 0)
                    {

                    }
                    else
                    {
                        SetLocation(0,400);
                        _location.Location = _position.ToPoint();

                    }

                }

            foreach (Rectangle barrier in barriers)
                if (_location.Intersects(barrier))
                {
                    // hit bottom
                    if (_velocity.Y < 0)
                    {
                        _position.Y -= _velocity.Y;
                        _velocity.Y = 0;
                        _location.Location = _position.ToPoint();

                    }
                    // land on
                    else
                    {
                        hasJumped = false;
                        _position.Y -= _velocity.Y;
                        _velocity.Y = 0;
                        _location.Location = _position.ToPoint();

                    }
                }
            

            Debug.WriteLine(_location.X + "");


        }

        public void SetLocation(int x, int y)
        {
            _position.X = x;
            _position.Y = y;
        }


        public Rectangle HitBox
        {
            get { return _location; }
            set { _location = value; }  
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_boyTextures[playerIndex], _location, null, Color.Black, 0f, Vector2.Zero, _spriteEffect, 1f);
        }
}
}
