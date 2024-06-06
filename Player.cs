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
        
        Texture2D texture;
        private List<Texture2D> _boyTextures;

        private Rectangle _location;

        Vector2 _velocity;
        Vector2 _position;

        bool hasJumped;

        public Player(List<Texture2D> boyTexture, Vector2 newPosition)
        {
            _boyTextures = boyTexture;
            _position = newPosition;
            hasJumped = true;
            _location = new Rectangle(newPosition.ToPoint(), new Point(14, 42));
            _position = new Vector2(0, 387);
        }

        public void Update(GameTime gameTime, List<Rectangle> barriers, List <Rectangle> coins)
        {
           
            // Horizontal Movement
            _velocity.X = 0;
            if (Keyboard.GetState().IsKeyDown(Keys.Right)) 
                _velocity.X += 3f;

            if (Keyboard.GetState().IsKeyDown(Keys.Left)) 
                _velocity.X += -3f;

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
                //position.Y -= 10f;
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
            spriteBatch.Draw(_boyTextures[3], _location, Color.White);
        }
}
}
