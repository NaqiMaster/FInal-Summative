using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FInal_Summative
{
    internal class Player
    {
        
        Texture2D _textureDoor;
        private List<Texture2D> _boyTextures;

        private Rectangle _location;

        Vector2 _velocity;
        Vector2 _position;

        SpriteEffects _spriteEffect;

        private int playerIndex;

        bool hasJumped;



        public Player(List<Texture2D> boyTexture, Vector2 newPosition)//, Texture2D textureDoor
        {
            _boyTextures = boyTexture;
        //    _textureDoor = textureDoor;
            _position = newPosition;
            hasJumped = true;
            _location = new Rectangle(newPosition.ToPoint(), new Point(15, 50));
            _position = new Vector2(550, 200);
            _spriteEffect = SpriteEffects.None;
        }

        public void Update(GameTime gameTime, List<Rectangle> barriers, List <Rectangle> coins)
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




        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_boyTextures[playerIndex], _location, null, Color.Black, 0f, Vector2.Zero, _spriteEffect, 1f);
          //  spriteBatch.Draw(_textureDoor, new Vector2(50, 200),Color.White);
        }
}
}
