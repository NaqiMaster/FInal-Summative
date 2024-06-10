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
            barrierTexture = Content.Load<Texture2D>("Grass");
            coinTexture = Content.Load<Texture2D>("GoldCoin");
            fireboySpritesheet = Content.Load<Texture2D>("boySprite1");

            Texture2D cropTexture;

            //Default Stand Index 0
            Rectangle sourceRect2 = new Rectangle(152, 17, 187 - 152, 80 - 17);
            cropTexture = new Texture2D(GraphicsDevice, sourceRect2.Width, sourceRect2.Height);
            Color[] data2 = new Color[sourceRect2.Width * sourceRect2.Height];
            fireboySpritesheet.GetData(0, sourceRect2, data2, 0, data2.Length);
            cropTexture.SetData(data2);

            boyTexture.Add(cropTexture);

            //Jump Index 1
            Rectangle sourceRect = new Rectangle(120, 259,160-120, 315-259);
            cropTexture = new Texture2D(GraphicsDevice, sourceRect.Width, sourceRect.Height);
            Color[] data = new Color[sourceRect.Width * sourceRect.Height];
            fireboySpritesheet.GetData(0, sourceRect, data, 0, data.Length);
            cropTexture.SetData(data);

            boyTexture.Add(cropTexture);

            //Run Index 2
            Rectangle sourceRect1 = new Rectangle(366,99, 411-366, 155-99);
            cropTexture = new Texture2D(GraphicsDevice, sourceRect1.Width, sourceRect1.Height);
            Color[] data1 = new Color[sourceRect1.Width * sourceRect1.Height];
            fireboySpritesheet.GetData(0, sourceRect1, data1, 0, data1.Length);
            cropTexture.SetData(data1);

            boyTexture.Add(cropTexture);




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