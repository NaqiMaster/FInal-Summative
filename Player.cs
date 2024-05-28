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

        private Rectangle _location;


        Vector2 velocity;
        Vector2 position;

        bool hasJumped;

        public Player(Texture2D newTexture,Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
            hasJumped = true;
            _location = new Rectangle(newPosition.ToPoint(), new Point(newTexture.Width, newTexture.Height));
            position = new Vector2(0, 401);
        }

        public void Update(GameTime gameTime, List<Rectangle> barriers)
        {
           
            // Horizontal Movement
            velocity.X = 0;
            if (Keyboard.GetState().IsKeyDown(Keys.Right)) 
                velocity.X += 3f;

            if (Keyboard.GetState().IsKeyDown(Keys.Left)) 
                velocity.X += -3f;

            position.X += velocity.X;
            _location.Location = position.ToPoint();

            foreach (Rectangle barrier in barriers)
                if (_location.Intersects(barrier))
                {
                    position.X -= velocity.X;
                    _location.Location = position.ToPoint();
                    break;
                }


            // Vertical Movement

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && hasJumped == false)
            {
                //position.Y -= 10f;
                velocity.Y = -5f;
                hasJumped = true;
            }           
            velocity.Y += 0.20f;
            

           

            

            position.Y += velocity.Y;
            _location.Location = position.ToPoint();

            foreach (Rectangle barrier in barriers)
                if (_location.Intersects(barrier))
                {
                    // hit bottom
                    if (velocity.Y < 0)
                    {
                        position.Y -= velocity.Y;
                        velocity.Y = 0;
                        _location.Location = position.ToPoint();

                    }
                    // land on
                    else
                    {
                        hasJumped = false;
                        position.Y -= velocity.Y;
                        velocity.Y = 0;
                        _location.Location = position.ToPoint();

                    }
                }




        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,position, Color.White);
        }
}
}
