using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq.Expressions;

namespace FInal_Summative
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Player player;
        Texture2D barrierTexture,lavaTexture, coinTexture,gamePic,
            teleporterTexture,fireboySpritesheet, introBackground, level1Background, textureDoor;
        List<Rectangle> barriers;
        List<Rectangle> coins;
        List<Rectangle> lava;
        List<Rectangle> teleporters;
        List<Rectangle> door;
        List<Texture2D> boyTexture;
        SoundEffect introSong, fire, collectCoin,jump;
        SoundEffectInstance introSongInstance;
        Rectangle window, playerr, btnInstructions, btnLevelChoose, btnLevel1,btnLevel2,btnLevel3, btnCredits,btnExit,gameRect;
        SpriteFont titleIntro, textChooseLevel, textLevel1, textCredits, textInstructions;
        MouseState mouseState,prevMouseState;
        bool removedBarrier;

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
            removedBarrier = false;

            btnInstructions = new Rectangle(540, 380, 200, 50);
            btnCredits = new Rectangle(540, 300, 200, 50);
            btnLevelChoose = new Rectangle(40, 300, 200, 130);
            btnLevel1 = new Rectangle(50, 100, 200, 300);
            btnLevel2 = new Rectangle(300, 100, 200, 300);
            btnLevel3 = new Rectangle(550, 100, 200, 300);
            btnExit = new Rectangle(10, 10, 40, 20);
            gameRect = new Rectangle(450, 150, 300, 300);




            boyTexture = new List<Texture2D>();
            base.Initialize();

            lava = new List<Rectangle>();

            coins = new List<Rectangle>();

            barriers = new List<Rectangle>();

            teleporters = new List<Rectangle>();

            door = new List<Rectangle>();   
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
            textCredits = Content.Load<SpriteFont>("textCredits");
            lavaTexture = Content.Load<Texture2D>("lava");
            teleporterTexture = Content.Load<Texture2D>("purpleButton");
            introSong = Content.Load<SoundEffect>("introSong");
            introSongInstance = introSong.CreateInstance();
            fire = Content.Load<SoundEffect>("touchingLava");
            collectCoin = Content.Load<SoundEffect>("collectCoin");
            jump = Content.Load<SoundEffect>("jump");
            gamePic = Content.Load<Texture2D>("fireBoy");


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

            this.Window.Title = "BoxBoy & BoxGirl" + coins.Count;

            prevMouseState = mouseState;
            mouseState = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (screen == Screen.Intro)
            {
                introSongInstance.Play();
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
                introSongInstance.Play();
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    if (btnLevel1.Contains(mouseState.X, mouseState.Y))
                    {
                        screen = Screen.Level1;
                        ClearBarriers();
                        barriersLevel1();
                        lavaLevel1();
                        coinsLevel1();
                        doorLevel1();
                        player.SetLocation(0, 400);

                    }
                    else if (btnLevel2.Contains(mouseState.X, mouseState.Y))
                    {
                        screen = Screen.Level2;
                        ClearBarriers();
                        barriersLevel2();
                        lavaLevel2();
                        coinsLevel2();
                        teleporter2();
                        doorLevel2();
                        player.SetLocation(0, 150);



                    }
                    else if (btnLevel3.Contains(mouseState.X, mouseState.Y))
                    {
                        screen = Screen.Level3;
                    }
                    else if (btnExit.Contains(mouseState.X, mouseState.Y))
                    {
                        screen = Screen.Intro;
                    }
                }
            }

            else if (screen == Screen.Level1)
            {
                introSongInstance.Pause();
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    if (btnExit.Contains(mouseState.X, mouseState.Y))
                    {
                        screen = Screen.Intro;
                    }
                }

                player.Update(gameTime,barriers,coins,door,lava,teleporters,fire,jump,collectCoin);

                if (coins.Count == 0)
                {
                    foreach (Rectangle door in door)
                    {
                        if (door.Contains(player.HitBox))
                        {
                            screen = Screen.ChooseLevel;
                            player.SetLocation(0, 100);
                        }
                    }
                }
                



            }
            else if (screen == Screen.Level2)
            {
                introSongInstance.Pause();
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    if (btnExit.Contains(mouseState.X, mouseState.Y))
                    {
                        screen = Screen.Intro;
                    }
                }

                player.Update(gameTime, barriers, coins, door, lava, teleporters,fire,jump,collectCoin);

                if (coins.Count == 0)
                {
                    if (removedBarrier == false)
                    {
                        barriers.RemoveAt(6);
                        barriers.Add(new Rectangle(420, 0, 10, 110));
                        barriers.Add(new Rectangle(430,100,20,10));
                        barriers.Add(new Rectangle(430, 180, 20, 10));
                        barriers.Add(new Rectangle(350,130,20,10));

                        barriers.Add(new Rectangle(230, 80, 20, 10));

                        barriers.Add(new Rectangle(90, 80, 60, 10));


                        removedBarrier = true;
                    }

                    foreach(Rectangle door in door)
                    {
                        if (door.Contains(player.HitBox))
                        {
                            screen = Screen.ChooseLevel;
                        }
                    }

                }

                if (teleporters[0].Intersects(player.HitBox))
                {
                    player.SetLocation(500,0);
                }

                if (teleporters[1].Intersects(player.HitBox))
                {
                    player.SetLocation(500, 0);
                }

                if (player.HitBox.Contains(0, 400))
                {
                    player.SetLocation(0, 150);
                }
            }
            else if (screen == Screen.Level3)
            {
                introSongInstance.Pause();
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    if (btnExit.Contains(mouseState.X, mouseState.Y))
                    {
                        screen = Screen.Intro;
                    }
                }
            }
            else if (screen == Screen.Instructions)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    if (btnExit.Contains(mouseState.X, mouseState.Y))
                    {
                        screen = Screen.Intro;
                    }
                }
            }
            else if (screen == Screen.Credits) 
            {
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    if (btnExit.Contains(mouseState.X, mouseState.Y))
                    {
                        screen = Screen.Intro;
                    }
                }
            }




            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        public void doorLevel1()
        {
            door.Add(new Rectangle(50, 130, 50, 50));
        }
        public void coinsLevel1()
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

        public void doorLevel2()
        {
            door.Add(new Rectangle(90, 30, 50, 50));
        }
        public void teleporter2()
        {
            teleporters.Add(new Rectangle(495, 400, 10, 50));
            teleporters.Add(new Rectangle(645, 400, 10, 50));

        }

        public void coinsLevel2()
        {
            coins.Add(new Rectangle(750, 410, 20, 20));
        }
        public void lavaLevel2()
        {
            lava.Add(new Rectangle(650, 120, 40, 40));
            lava.Add(new Rectangle(730, 120, 70, 40));

            lava.Add(new Rectangle(650, 220, 20, 40));
            lava.Add(new Rectangle(710, 220, 90, 40));

            lava.Add(new Rectangle(650, 300, 30, 40));
            lava.Add(new Rectangle(720, 300, 80, 40));

            lava.Add(new Rectangle(150, 450, 350, 10));

        }

        public void barriersLevel2()
        {

            //Parkour
            barriers.Add(new Rectangle(150, 420, 10, 10));//1
            barriers.Add(new Rectangle(200,360,10,10));//2
            barriers.Add(new Rectangle(300, 325, 10, 10));//3
            barriers.Add(new Rectangle(480, 340, 20, 10));//4

            barriers.Add(new Rectangle(0, 200, 150, 250));
            barriers.Add(new Rectangle(500, 50, 150, 400));
            barriers.Add(new Rectangle(200, 0, 100, 260));

            //Outline
            barriers.Add(new Rectangle(0, 450, 800, 10));//Bottom
            barriers.Add(new Rectangle(0, 0, 0, 460));//LeftSide
            barriers.Add(new Rectangle(800, 0, 0, 460));//RightSide
            barriers.Add(new Rectangle(0, 0, 800, 0));//Top
        }
        public void ClearBarriers()
        {
            barriers.Clear();
            coins.Clear();
            lava.Clear();
            teleporters.Clear();
            door.Clear();
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


                _spriteBatch.DrawString(textChooseLevel,"C R E D I T S",new Vector2(542,300),Color.Black);
                _spriteBatch.DrawString(textChooseLevel, "R U L E S", new Vector2(565, 380), Color.Black);
                _spriteBatch.DrawString(textChooseLevel, "C H O O S E", new Vector2(52, 320), Color.Black);
                _spriteBatch.DrawString(textChooseLevel, "L E V E L", new Vector2(70, 360), Color.Black);
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
                    foreach (Rectangle door in door)
                    _spriteBatch.Draw(textureDoor, door, Color.White);
                }

                _spriteBatch.Draw(level1Background, btnExit, Color.Red);
            }
            else if (screen == Screen.Level2)
            {
                _spriteBatch.Draw(level1Background, window, Color.White);
                player.Draw(_spriteBatch);

                foreach (Rectangle telporter in teleporters)
                    _spriteBatch.Draw(teleporterTexture, telporter, Color.White);
                foreach (Rectangle barrier in barriers)
                    _spriteBatch.Draw(barrierTexture, barrier, Color.Black);

                foreach (Rectangle lavaRect in lava)
                    _spriteBatch.Draw(lavaTexture, lavaRect, Color.White);

                foreach (Rectangle coin in coins)
                    _spriteBatch.Draw(coinTexture, coin, Color.Purple);
                if (coins.Count == 0)
                {   
                    foreach (Rectangle door in door)
                    _spriteBatch.Draw(textureDoor, door, Color.White);
                }
                _spriteBatch.Draw(level1Background, btnExit, Color.Red);
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

                _spriteBatch.Draw(level1Background, btnExit, Color.Red);
            }
            else if (screen == Screen.Instructions)
            {
                _spriteBatch.Draw(level1Background, window, Color.White);
                _spriteBatch.DrawString(titleIntro, "INSTRUCTIONS", new Vector2(160, 0), Color.Black);
                _spriteBatch.DrawString(textInstructions, "- ARROW KEYS to move",new Vector2(5,100), Color.Black);
                _spriteBatch.DrawString(textInstructions, "- Collect all coins to complete level", new Vector2(5, 150), Color.Black);
                _spriteBatch.DrawString(textInstructions, "- Door will open after all coins are collected", new Vector2(5, 200), Color.Black);
                _spriteBatch.DrawString(textInstructions, "- Purple button is a teleporter", new Vector2(5, 250), Color.Black);
                _spriteBatch.DrawString(textInstructions, "- Touching Lava will KILL you", new Vector2(5, 300), Color.Black);
                _spriteBatch.DrawString(textInstructions, "- Click Red button to return to home", new Vector2(5, 350), Color.Black);
                _spriteBatch.DrawString(textInstructions, "- RUNNING OFF A PLATFORM WILL KEEP YOUR JUMP IN THE AIR", new Vector2(5, 400), Color.Black);

                _spriteBatch.Draw(level1Background, btnExit, Color.Red);
            }
            else if (screen == Screen.Credits)
            {
                _spriteBatch.Draw(level1Background, window, Color.White);
                _spriteBatch.DrawString(titleIntro, "CREDITS", new Vector2(250, 0), Color.Black);
                _spriteBatch.DrawString(textCredits, "Inspired by Fireboy & Watergirl", new Vector2(5, 100), Color.Black);
                _spriteBatch.DrawString(textCredits, "Created by Naqi Master", new Vector2(5, 150), Color.Black);
                _spriteBatch.Draw(gamePic,gameRect, Color.White);

                _spriteBatch.Draw(level1Background, btnExit, Color.Red);
            }

            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}