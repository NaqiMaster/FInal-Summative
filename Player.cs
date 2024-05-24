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
        private float _acceleration = 1.05f;


        Vector2 velocity;
        Vector2 position;

        bool hasJumped;

        public Player(Texture2D newTexture,Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
            hasJumped = true;
        }


        public Rectangle CollisonRectangle 
        {
            get { return _location; }
            set { _location = value; }
        }

        public bool CollisionCollide(Rectangle item)
        {
            return _location.Intersects(item);
        }

        public void Update(GameTime gameTime, List<Rectangle> barriers)
        {
            //position += velocity;
            velocity.X = 0;
            if (Keyboard.GetState().IsKeyDown(Keys.Right)) 
                velocity.X += 3f;

            if (Keyboard.GetState().IsKeyDown(Keys.Left)) 
                velocity.X += -3f;

            
            position.X += velocity.X;

            foreach (Rectangle barrier in barriers)
            {
                if (this.CollisionCollide(barrier))
                {
                    position.X -= velocity.X;
                }
            }
            _location.Location = position.ToPoint();


            if (Keyboard.GetState().IsKeyDown(Keys.Up) && hasJumped == false)
            {
                position.Y -= 10f;
                velocity.Y = -5f;
                hasJumped = true;
            }

            if (hasJumped == true)
            {
                float i = 0;
                velocity.Y += 0.20f + i;
            }

            if (position.Y + texture.Height >= 450)
            {
                hasJumped = false;
            }

            if(hasJumped == false)
            {
                velocity.Y = 0f;
            }


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,position, Color.White);
        }
    }
}
