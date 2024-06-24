using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq.Expressions;

namespace FInal_Summative
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Player player;
        Texture2D barrierTexture,lavaTexture, coinTexture, fireboySpritesheet, introBackground, level1Background, textureDoor;
        List<Rectangle> barriers;
        List<Rectangle> coins;
        List<Rectangle> lava;
        List<Texture2D> boyTexture;
        Rectangle window, playerr, btnInstructions, btnLevelChoose, btnLevel1,btnLevel2,btnLevel3, btnCredits, door;
        SpriteFont titleIntro, textChooseLevel, textLevel1, textCredits, textInstructions;
        MouseState mouseState,prevMouseState;

        enum Screen
        {
            Intro,
            Instructions,
            ChooseLevel,
            Level1,
            Level2,
            Level3,
            Credits,
            deathScreen
        }

        Screen screen;
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

            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 460;
            window = new Rectangle(0,0,_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            playerr = new Rectangle(270, 150, 200, 300);
            this.Window.Title = "BoxBoy & BoxGirl";
            screen = Screen.Intro;

            btnInstructions = new Rectangle(540, 380, 200, 50);
            btnCredits = new Rectangle(540, 300, 200, 50);
            btnLevelChoose = new Rectangle(40, 300, 200, 130);
            btnLevel1 = new Rectangle(50, 100, 200, 300);
            btnLevel2 = new Rectangle(300, 100, 200, 300);
            btnLevel3 = new Rectangle(550, 100, 200, 300);
            door = new Rectangle(50, 130, 50, 50);




            boyTexture = new List<Texture2D>();
            base.Initialize();

            lava = new List<Rectangle>();

            coins = new List<Rectangle>();
            coinsAdd();

            barriers = new List<Rectangle>();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            barrierTexture = Content.Load<Texture2D>("whiteBackground");
            coinTexture = Content.Load<Texture2D>("GoldCoin");
            fireboySpritesheet = Content.Load<Texture2D>("boySprite1");
            introBackground = Content.Load<Texture2D>("whiteBackground");
            level1Background = Content.Load<Texture2D>("whiteBackground");
            titleIntro = Content.Load<SpriteFont>("titleIntro");
            textChooseLevel = Content.Load<SpriteFont>("textChooseLevel");
            textureDoor = Content.Load<Texture2D>("door");
            textInstructions = Content.Load<SpriteFont>("textInstructions");
            lavaTexture = Content.Load<Texture2D>("lava");


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

            prevMouseState = mouseState;
            mouseState = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (screen == Screen.Intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    if (btnLevelChoose.Contains(mouseState.X, mouseState.Y))
                    {
                        screen = Screen.ChooseLevel;
                    }
                    else if (btnCredits.Contains(mouseState.X, mouseState.Y))
                    {
                        screen = Screen.Credits;
                    }
                    else if (btnInstructions.Contains(mouseState.X, mouseState.Y))
                    {
                        screen = Screen.Instructions;
                    }
            }   }
            else if (screen == Screen.ChooseLevel)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    if (btnLevel1.Contains(mouseState.X, mouseState.Y))
                    {
                        screen = Screen.Level1;
                        ClearBarriers();
                        barriersLevel1();
                        lavaLevel1();
                    }
                    else if (btnLevel2.Contains(mouseState.X, mouseState.Y))
                    {
                        screen = Screen.Level2;
                        ClearBarriers();
                        barriersLevel2();
                        player.SetLocation(0, 400);


                    }
                    if (btnLevel3.Contains(mouseState.X, mouseState.Y))
                    {
                        screen = Screen.Level3;
                    }
                }
            }

            else if (screen == Screen.Level1)
            {
                player.Update(gameTime, barriers, coins,door,lava);

                if (coins.Count == 0)
                {
                    if (door.Contains(player.HitBox))
                    {
                        screen = Screen.Level2;
                        player.SetLocation(0, 400);


                    }
                }

            }
            else if (screen == Screen.Level2)
            {
                player.Update(gameTime, barriers, coins, door,lava);

                if (coins.Count == 0)
                {
                    if (door.Contains(player.HitBox))
                    {
                        screen = Screen.Level3;
                    }
                }
            }
            else if (screen == Screen.Level3)
            {

            }
            else if (screen == Screen.Instructions)
            {

            }




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

        public void lavaLevel1()
        {
           lava.Add(new Rectangle(195, 450, 30, 10));
           lava.Add(new Rectangle(445, 450, 30, 10));
           lava.Add(new Rectangle(610, 370, 70, 10));
           lava.Add(new Rectangle(0, 370, 70, 10));
           lava.Add(new Rectangle(110, 370, 80, 10));

            lava.Add(new Rectangle(200, 180, 20, 10));
            lava.Add(new Rectangle(240, 180, 20, 10)); 
            lava.Add(new Rectangle(280, 180, 20, 10));
            lava.Add(new Rectangle(320, 180, 20, 10));






        }

        public void barriersLevel1()
        {
            barriers.Add(new Rectangle(500, 200, 20, 10));
            barriers.Add(new Rectangle(550, 180, 10, 10));
            barriers.Add(new Rectangle(600, 150, 10, 10));
            barriers.Add(new Rectangle(730, 110, 10, 10));
            barriers.Add(new Rectangle(790, 50, 10, 10));
            barriers.Add(new Rectangle(500, 50, 200, 10));
            //4th Floor
            barriers.Add(new Rectangle(0, 180, 500, 10));

            //3rd Floor
            barriers.Add(new Rectangle(0, 250, 50, 10));
            barriers.Add(new Rectangle(72, 250, 622, 10));
            barriers.Add(new Rectangle(770, 250, 150, 10));
            barriers.Add(new Rectangle(500, 50, 10, 210));
            //Block in between 2nd and 3rd floor
            barriers.Add(new Rectangle(50, 310, 22, 10));
            barriers.Add(new Rectangle(770, 310, 30, 10));
            //2nd Floor
            barriers.Add(new Rectangle(0, 370, 700, 10));
            //Box on Floor
            barriers.Add(new Rectangle(780, 430, 20, 20));
            //Outline
            barriers.Add(new Rectangle(0, 450, 800, 10));//Bottom
            barriers.Add(new Rectangle(0, 0, 0, 460));//LeftSide
            barriers.Add(new Rectangle(800, 0, 0, 460));//RightSide
            barriers.Add(new Rectangle(0, 0, 800, 0));//Top
        }

        public void barriersLevel2()
        {
            //4th Floor
            barriers.Add(new Rectangle(0, 180, 500, 10));

            //3rd Floor
            barriers.Add(new Rectangle(0, 250, 50, 10));
            barriers.Add(new Rectangle(72, 250, 622, 10));
            barriers.Add(new Rectangle(770, 250, 150, 10));
            barriers.Add(new Rectangle(500, 50, 10, 210));
            //Block in between 2nd and 3rd floor
            barriers.Add(new Rectangle(50, 310, 22, 10));
            barriers.Add(new Rectangle(770, 310, 30, 10));
            //2nd Floor
            barriers.Add(new Rectangle(0, 370, 700, 10));
            //Box on Floor
            barriers.Add(new Rectangle(780, 430, 20, 20));
            //Outline
            barriers.Add(new Rectangle(0, 450, 800, 10));//Bottom
            barriers.Add(new Rectangle(0, 0, 0, 460));//LeftSide
            barriers.Add(new Rectangle(800, 0, 0, 460));//RightSide
            barriers.Add(new Rectangle(0, 0, 800, 0));//Top
        }
        public void ClearBarriers()
        {
            barriers.Clear();
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(introBackground, window, Color.Black);

                _spriteBatch.Draw(boyTexture[0], playerr, Color.FloralWhite);
                _spriteBatch.DrawString(titleIntro, "S  h  a  d  o  w  B  o  y", new Vector2(100, 30), Color.White);

                //Btn Instructions, Btn Credits, Btn LevelChoose
                _spriteBatch.Draw(introBackground, btnInstructions, Color.White);
                _spriteBatch.Draw(introBackground, btnCredits, Color.White);
                _spriteBatch.Draw(introBackground, btnLevelChoose, Color.White);


                _spriteBatch.DrawString(textChooseLevel,"CREDITS",new Vector2(560,300),Color.Black);
                _spriteBatch.DrawString(textChooseLevel, "RULES", new Vector2(570, 380), Color.Black);
                _spriteBatch.DrawString(textChooseLevel, "CHOOSE", new Vector2(75, 320), Color.Black);
                _spriteBatch.DrawString(textChooseLevel, "LEVEL", new Vector2(80, 360), Color.Black);



            }
            else if (screen == Screen.Level1)
            {
                _spriteBatch.Draw(level1Background, window, Color.White);               
                player.Draw(_spriteBatch);

                foreach (Rectangle barrier in barriers)
                    _spriteBatch.Draw(barrierTexture, barrier, Color.Black);

                foreach (Rectangle lavaRect in lava)
                    _spriteBatch.Draw(lavaTexture, lavaRect, Color.White);

                foreach (Rectangle coin in coins)
                    _spriteBatch.Draw(coinTexture, coin, Color.Purple);
                if (coins.Count == 0)
                {
                    _spriteBatch.Draw(textureDoor, door, Color.White);
                }
            }
            else if (screen == Screen.Level2)
            {
                _spriteBatch.Draw(level1Background, window, Color.White);

                foreach (Rectangle barrier in barriers)
                    _spriteBatch.Draw(barrierTexture, barrier, Color.Black);

                player.Draw(_spriteBatch);
            }
            else if (screen == Screen.ChooseLevel)
            {
                _spriteBatch.Draw(level1Background, window, Color.Black);

                _spriteBatch.Draw(introBackground, btnLevel1, Color.White);
                _spriteBatch.DrawString(textChooseLevel, "1", new Vector2(140, 150), Color.Black);
                _spriteBatch.DrawString(textChooseLevel, "EASY", new Vector2(100, 300), Color.Black);


                _spriteBatch.Draw(introBackground, btnLevel2, Color.White);
                _spriteBatch.DrawString(textChooseLevel, "2", new Vector2(390, 150), Color.Black);
                _spriteBatch.DrawString(textChooseLevel, "MEDIUM", new Vector2(322, 300), Color.Black);


                _spriteBatch.Draw(introBackground, btnLevel3, Color.White);
                _spriteBatch.DrawString(textChooseLevel, "3", new Vector2(640, 150), Color.Black);
                _spriteBatch.DrawString(textChooseLevel, "HARD", new Vector2(600, 300), Color.Black);


            }
            else if (screen == Screen.Instructions)
            {
                _spriteBatch.Draw(level1Background, window, Color.White);
                _spriteBatch.DrawString(textInstructions,"INSTRUCTIONS",new Vector2(0,0), Color.Black);
            }
            else if (screen == Screen.Credits)
            {
                _spriteBatch.Draw(level1Background, window, Color.White);
                _spriteBatch.DrawString(textInstructions, "CREDIT", new Vector2(0, 0), Color.Black);

            }

            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}