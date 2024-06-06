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
        Texture2D barrierTexture, coinTexture, fireboySpritesheet;
        List<Rectangle> barriers;
        List<Rectangle> coins;
        List<Texture2D> boyTexture;
        int playerIndex;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 460;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.Window.Title = "BoxBoy & BoxGirl";

            boyTexture = new List<Texture2D>();
            playerIndex = 0;

            base.Initialize();
           
            

            coins = new List<Rectangle>();
            coinsAdd();

            barriers = new List<Rectangle>();
            barriersAdd();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            barrierTexture = Content.Load<Texture2D>("gold");
            coinTexture = Content.Load<Texture2D>("GoldCoin");
            fireboySpritesheet = Content.Load<Texture2D>("boySpriteTOP");

            Texture2D cropTexture;
            Rectangle sourceRect;

            int width = fireboySpritesheet.Width / 12;
            int height = fireboySpritesheet.Height / 2;

            for (int y = 0; y < 2; y++) // Loops through each row
                for (int x = 0; x < 12; x++) // Loops through each card in a row
                {
                    sourceRect = new Rectangle(x * width, y * height, width, height);
                    cropTexture = new Texture2D(GraphicsDevice, width, height);
                    Color[] data = new Color[width * height];
                    fireboySpritesheet.GetData(0, sourceRect, data, 0, data.Length);
                    cropTexture.SetData(data);

                    //if (boyTexture.Count < 24)
                    boyTexture.Add(cropTexture);
                }


            player = new Player(boyTexture, new Vector2(0, 0));

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            player.Update(gameTime,barriers,coins);



            // TODO: Add your update logic here

            base.Update(gameTime);
        }
        public void coinsAdd()
        {
            coins.Add(new Rectangle(450,410,20,20));//1st
            coins.Add(new Rectangle(200,410, 20, 20));//2nd
            coins.Add(new Rectangle(780, 400, 20, 20));//3rd
            coins.Add(new Rectangle(780, 330, 20, 20));//4th
            coins.Add(new Rectangle(570, 330, 20, 20));//5th
            coins.Add(new Rectangle(320, 330, 20, 20));//6th
            coins.Add(new Rectangle(10, 210, 20, 20));//6th



        }

        public void barriersAdd()
        {
            //Parkour Part
            barriers.Add(new Rectangle(500, 200, 20, 10));
            barriers.Add(new Rectangle(550, 180, 10, 10));
            barriers.Add(new Rectangle(600, 150, 10, 10));
            barriers.Add(new Rectangle(730, 110, 10, 10));
            barriers.Add(new Rectangle(790, 50, 10, 10));
            barriers.Add(new Rectangle(500,45,200,10));





            //4th Floor
            barriers.Add(new Rectangle(0, 180, 500, 10));

            //3rd Floor
            barriers.Add(new Rectangle(0, 250, 50, 10));
            barriers.Add(new Rectangle(72, 250, 622, 10));
            barriers.Add(new Rectangle(770, 250, 150, 10));
            barriers.Add(new Rectangle(500,45, 10, 215));



            //Block in between 2nd and 3rd floor
            barriers.Add(new Rectangle(50, 310, 22, 10));
            barriers.Add(new Rectangle(770, 310, 30, 10));


            //2nd Floor
            barriers.Add(new Rectangle(0, 370,700,10));

            //Box on Floor
            barriers.Add(new Rectangle(780, 430, 20, 20));

            //Outline
            barriers.Add(new Rectangle(0, 450, 800, 10));//Bottom
            barriers.Add(new Rectangle(0, 0, 0, 460));//LeftSide
            barriers.Add(new Rectangle(800, 0,0, 460));//RightSide
            barriers.Add(new Rectangle(0, 0, 800,0));//Top


        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            player.Draw(_spriteBatch);

            foreach (Rectangle barrier in barriers)
                _spriteBatch.Draw(barrierTexture, barrier, Color.White);

            foreach (Rectangle coin in coins)
                _spriteBatch.Draw(coinTexture, coin, Color.White);


            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}