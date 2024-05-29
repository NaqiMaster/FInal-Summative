using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FInal_Summative
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Player player;
        Texture2D barrierTexture;
        List<Rectangle> barriers;
        KeyboardState keyboardState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            barriers = new List<Rectangle>();
            barriersAdd();


        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            player = new Player(Content.Load<Texture2D>("Brick1"), new Vector2(50, 50));
            barrierTexture = Content.Load<Texture2D>("gold");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            this.Window.Title = "Number of barriers: " + barriers.Count;
            player.Update(gameTime,barriers);




            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        public void barriersAdd()
        {
            //3rd Floor
            barriers.Add(new Rectangle(0, 250, 50, 10));
            barriers.Add(new Rectangle(72, 250, 622, 10));
            barriers.Add(new Rectangle(750, 250, 150, 10));
            barriers.Add(new Rectangle(500, 170, 10, 80));



            //Block in between 2nd and 3rd floor
            barriers.Add(new Rectangle(50, 310, 22, 10));
            barriers.Add(new Rectangle(770, 310, 30, 10));


            //2nd Floor
            barriers.Add(new Rectangle(0, 370,700,10));

            //1st Floor
            barriers.Add(new Rectangle(0, 450, 800, 10));

            //Box on Floor
            barriers.Add(new Rectangle(780, 430, 20, 20));
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            player.Draw(_spriteBatch);

            foreach (Rectangle barrier in barriers)
                _spriteBatch.Draw(barrierTexture, barrier, Color.White);

            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}