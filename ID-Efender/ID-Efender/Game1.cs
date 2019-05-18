using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace ID_Efender
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Utility
        GamePadState pad1;
        GamePadState oldPad;
        public static readonly Random RNG = new Random();

        //Menu
        enum GameState
        {
            Main,
            Game,
            Score
        }
        GameState gameState;
        MenuText play, exit, studio, select, shoot, move;
        string playString = "PLAY GAME";
        string exitString = "EXIT";
        string studioString = "Green Carnation Games";
        string selectString = "Select";
        string shootString = "Shoot";
        string moveString = "Move";
        SpriteFont MainMenuBigFont;
        SpriteFont MainMenuSmallFont;
        Cursor cursor;
        Logo logo;
        const float LOGOFPS = 3;
        MenuImage aButton, xButton, dPad;

        //Score
        int score;
        int invasionno;
        const int MAXINVASION = 100;
        const int ADDSCORE = 500;
        const int HUMANABDUCTED = 10;
        const int SHIPESCAPED = 20;

        //Game UI
        SpriteFont GameFont;
        MenuText scoretext, invasion;
        string scoretextString = "SCORE";
        string invasionString = "INVASION";
        MenuImage invasionborder;
        InvasionMeter invasionMeter;
        List<ScorePopUp> scorePopUps;
        SpriteFont scorePopUpFont;
        const float scorePopUpSpeed = 1f;

        //Score screen
        MenuText gameover, yourscore, scoreexit;
        string gameoverString = "GAME OVER";
        string yourscoreString = "YOUR SCORE:";
        SpriteFont ScoreScreenFont;
        MenuImage backButton;

        //Player Ship
        PlayerShip playerShip;
        static float PLAYERFPS = 4;
        static float PLAYERSPEED = 10f;

        //Bullet
        List<Bullet> bullets;
        const float BULLETSPEED = 10f;
        Texture2D bulletTxr;

        //Abduction Ship
        List<AbductionShip> abductionShips;
        const float ABDUCTIONSPEED = 3f;
        Texture2D abductionTxr;
        const int BASESPAWNRATE = 5;
        float spawnRate, timeTillSpawn;
        int shipsPerSpawn;

        //Explosion
        List<Explosion> explosions;
        Texture2D explosiontxr;
        const float EXPLOSIONFPS = 11;
        List<Particles> particles;
        Texture2D particletxr;
        const int PARTICLES = 100;
        SoundEffect explosionSound;

        //Humans
        List<Humans> humans;
        const int HUMANS = 5;
        Texture2D human0;
        Texture2D human1;

        //Audio
        SoundEffectInstance bgSoundGameInstance;
        SoundEffect bgSoundGame;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
        }

        protected override void Initialize()
        {
            gameState = GameState.Main;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Loading in playerShip
            playerShip = new PlayerShip(Content.Load<Texture2D>("Player//spaceshipspritesheet"), PLAYERFPS, 0, graphics.PreferredBackBufferHeight / 2, PLAYERSPEED);

            //Main Menu
            MainMenuBigFont = Content.Load<SpriteFont>("Fonts//MainMenuBig");
            MainMenuSmallFont = Content.Load<SpriteFont>("Fonts//MainMenuSmall");
            play = new MenuText(playString, MainMenuBigFont, 241, 398, Color.Red);
            exit = new MenuText(exitString, MainMenuBigFont, 332, 504, Color.Red);
            studio = new MenuText(studioString, MainMenuSmallFont, 281, 7, Color.Green);
            select = new MenuText(selectString, MainMenuSmallFont, 380, 103, Color.Red);
            shoot = new MenuText(shootString, MainMenuSmallFont, 380, 135, Color.Red);
            move = new MenuText(moveString, MainMenuSmallFont, 380, 167, Color.Red);
            cursor = new Cursor(Content.Load<Texture2D>("Main Menu//Cursor"), play.pos, exit.pos);
            logo = new Logo(Content.Load<Texture2D>("Main Menu//logospritesheet"), 114, 252, LOGOFPS);
            aButton = new MenuImage(Content.Load<Texture2D>("Main Menu//Button A"), 328, 97);
            xButton = new MenuImage(Content.Load<Texture2D>("Main Menu//Button X"), 328, 129);
            dPad = new MenuImage(Content.Load<Texture2D>("Main Menu//DPad"), 328, 161);

            //Game UI
            GameFont = Content.Load<SpriteFont>("Fonts//GameFont");
            scoretext = new MenuText(scoretextString, GameFont, 10, 10, Color.Red);
            invasion = new MenuText(invasionString, GameFont, 595, 10, Color.Red);
            invasionborder = new MenuImage(Content.Load<Texture2D>("Game UI//Invasion Meter Border"), 590, 51);
            invasionMeter = new InvasionMeter(Content.Load<Texture2D>("Game UI//Invasion Meter Bar"), Content.Load<Texture2D>("Game UI//Invasion Meter Bar Near Full"),595, 56);
            scorePopUpFont = Content.Load<SpriteFont>("Fonts//ScorePopUp");

            //Score screen
            ScoreScreenFont = Content.Load<SpriteFont>("Fonts//ScoreScreenFont");
            gameover = new MenuText(gameoverString, ScoreScreenFont, 97, 80, Color.Red);
            yourscore = new MenuText(yourscoreString, ScoreScreenFont, 38, 250, Color.Red);
            scoreexit = new MenuText(exitString, GameFont, 38, 569, Color.Red);
            backButton = new MenuImage(Content.Load<Texture2D>("Button Back"), 2, 569);

            //Texture load
            abductionTxr = Content.Load<Texture2D>("Enemy//Abduction Ship Base");
            explosiontxr = Content.Load<Texture2D>("Enemy//explosionspritesheet");
            particletxr = Content.Load<Texture2D>("Enemy//particle");
            human0 = Content.Load<Texture2D>("Humans//Human0");
            human1 = Content.Load<Texture2D>("Humans//Human1");
            bulletTxr = Content.Load<Texture2D>("Player//projectile-blue");

            //Sound Load
            explosionSound = Content.Load<SoundEffect>("Audio//explosion");
            bgSoundGame = Content.Load<SoundEffect>("Audio//game loop");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //Utility stuff
            pad1 = GamePad.GetState(PlayerIndex.One);

            switch (gameState)
            {
                case GameState.Main:
                    //Updates
                    cursor.UpdateMe(pad1, oldPad);
                    if(pad1.Buttons.A == ButtonState.Pressed)
                    {
                        if (cursor.currState == Cursor.CursorState.Play)
                        {
                            //Setup
                            abductionShips = new List<AbductionShip>();
                            shipsPerSpawn = 1;
                            spawnRate = BASESPAWNRATE;
                            timeTillSpawn = BASESPAWNRATE;

                            humans = new List<Humans>();

                            bullets = new List<Bullet>();

                            explosions = new List<Explosion>();
                            particles = new List<Particles>();

                            scorePopUps = new List<ScorePopUp>();

                            bgSoundGameInstance = null;

                            score = 0;
                            invasionno = 0;

                            gameState = GameState.Game;
                        }
                        else
                        {
                            Exit();
                        }
                    }

                    break;
                case GameState.Game:
                    //Background Sound
                    if (bgSoundGameInstance == null)
                    {
                        bgSoundGameInstance = bgSoundGame.CreateInstance();
                        bgSoundGameInstance.IsLooped = true;
                        bgSoundGameInstance.Play();
                    }

                    //Updates
                    playerShip.UpdateMe(pad1);
                    invasionMeter.UpdateMe(invasionno);

                    for (int i = 0; i < abductionShips.Count; i++)
                    {
                        abductionShips[i].UpdateMe();
                        if (abductionShips[i].currstate == AbductionShip.ShipState.Left)
                        {
                            abductionShips.RemoveAt(i);
                            invasionno += SHIPESCAPED;
                        }
                    }

                    for (int i = 0; i < humans.Count; i++)
                    {
                        humans[i].UpdateMe();
                    }

                    for (int i = 0; i < bullets.Count; i++)
                    {
                        bullets[i].UpdateMe();
                        if (bullets[i].currState == Bullet.BulletState.Inactive)
                        {
                            bullets.RemoveAt(i);
                        }
                    }

                    for (int i = 0; i < explosions.Count; i++)
                    {
                        if (explosions[i].currstate == Explosion.ExplosionState.Inactive)
                        {
                            explosions.RemoveAt(i);
                        }
                    }

                    for (int i = 0; i < particles.Count; i++)
                    {
                        particles[i].UpdateMe();
                        if (particles[i].currstate == Particles.ParticleState.Inactive)
                        {
                            particles.RemoveAt(i);
                        }
                    }

                    for (int i = 0; i < scorePopUps.Count; i++)
                    {
                        scorePopUps[i].UpdateMe();
                        if (scorePopUps[i].currState == ScorePopUp.ScoreState.Inactive)
                        {
                            scorePopUps.RemoveAt(i);
                        }
                    }

                    //Spawn
                    if (timeTillSpawn < 0)
                    {
                        for (int i = 0; i < shipsPerSpawn; i++)
                        {
                            abductionShips.Add(new AbductionShip(abductionTxr, RNG, GraphicsDevice.Viewport.Bounds, ABDUCTIONSPEED));
                        }
                        if (spawnRate < BASESPAWNRATE / 2)
                        {
                            spawnRate = BASESPAWNRATE;
                            shipsPerSpawn++;
                        }
                        else
                        {
                            spawnRate -= 0.1f;
                        }

                        timeTillSpawn = spawnRate;
                    }
                    else
                    {
                        timeTillSpawn -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }

                    if (humans.Count < HUMANS)
                    {
                        humans.Add(new Humans(human0, human1, RNG, GraphicsDevice.Viewport.Bounds));
                    }

                    if (pad1.Buttons.X == ButtonState.Pressed && oldPad.Buttons.X == ButtonState.Released)
                    {
                        bullets.Add(new Bullet(bulletTxr, playerShip.collisionrect, playerShip.currState, BULLETSPEED));
                    }

                    //Collision
                    for (int a = 0; a < abductionShips.Count; a++)
                    {
                        if (abductionShips[a].currstate == AbductionShip.ShipState.Landed)
                        {
                            for (int h = 0; h < humans.Count; h++)
                            {
                                if (humans[h].collisionrect.Intersects(abductionShips[a].abductionrect))
                                {
                                    abductionShips[a].currstate = AbductionShip.ShipState.Ascending;
                                    invasionno += HUMANABDUCTED;
                                    humans.RemoveAt(h);
                                }
                            }
                        }
                    }

                    for (int b = 0; b < bullets.Count; b++)
                    {
                        for (int a = 0; a < abductionShips.Count; a++)
                        {
                            if (bullets[b].collisionRect.Intersects(abductionShips[a].collisionrect))
                            {
                                bullets[b].currState = Bullet.BulletState.Inactive;
                                explosions.Add(new Explosion(explosiontxr, abductionShips[a].collisionrect, EXPLOSIONFPS));
                                scorePopUps.Add(new ScorePopUp("" + ADDSCORE, scorePopUpFont, abductionShips[a].collisionrect.X, abductionShips[a].collisionrect.Y, Color.Red, scorePopUpSpeed));
                                for (int i = 0; i < PARTICLES; i++)
                                {
                                    particles.Add(new Particles(particletxr, abductionShips[a].collisionrect, RNG));
                                }
                                score += ADDSCORE;
                                abductionShips.RemoveAt(a);
                                explosionSound.Play();
                            }
                        }
                    }

                    //Fail state
                    if (invasionno >= MAXINVASION)
                    {
                        gameState = GameState.Score;
                        bgSoundGameInstance.Stop();
                    }

                    break;
                case GameState.Score:

                    //Return to main menu
                    if (pad1.Buttons.Back == ButtonState.Pressed)
                    {
                        gameState = GameState.Main;
                    }

                    break;
            }

            //Setting the last button pressed
            oldPad = pad1;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            switch (gameState)
            {
                case GameState.Main:
                    //Main menu stuff
                    spriteBatch.Begin();

                    play.DrawMe(spriteBatch);
                    exit.DrawMe(spriteBatch);
                    studio.DrawMe(spriteBatch);
                    select.DrawMe(spriteBatch);
                    shoot.DrawMe(spriteBatch);
                    move.DrawMe(spriteBatch);
                    cursor.DrawMe(spriteBatch);
                    logo.DrawMe(spriteBatch, gameTime);
                    aButton.DrawMe(spriteBatch);
                    xButton.DrawMe(spriteBatch);
                    dPad.DrawMe(spriteBatch);

                    spriteBatch.End();
                    break;
                case GameState.Game:
                    //Game stuff
                    spriteBatch.Begin();

                    //Actors draw
                    playerShip.DrawMe(spriteBatch, gameTime);
                    
                    for (int i = 0; i < abductionShips.Count; i++)
                    {
                        abductionShips[i].DrawMe(spriteBatch);
                    }
                    
                    for (int i = 0; i < humans.Count; i++)
                    {
                        humans[i].DrawMe(spriteBatch);
                    }

                    for (int i = 0; i < bullets.Count; i++)
                    {
                        bullets[i].DrawMe(spriteBatch);
                    }

                    for (int i = 0; i < explosions.Count; i++)
                    {
                        explosions[i].DrawMe(spriteBatch, gameTime);
                    }

                    for (int i = 0; i < particles.Count; i++)
                    {
                        particles[i].DrawMe(spriteBatch);
                    }

                    for (int i = 0; i < scorePopUps.Count; i++)
                    {
                        scorePopUps[i].DrawMe(spriteBatch);
                    }

                    //UI
                    scoretext.DrawMe(spriteBatch);
                    spriteBatch.DrawString(GameFont, "" + score, new Vector2(10, 60), Color.Red);
                    invasion.DrawMe(spriteBatch);
                    invasionborder.DrawMe(spriteBatch);
                    invasionMeter.DrawMe(spriteBatch);

                    spriteBatch.End();
                    break;
                case GameState.Score:
                    //Score screen stuff
                    spriteBatch.Begin();

                    gameover.DrawMe(spriteBatch);
                    yourscore.DrawMe(spriteBatch);
                    spriteBatch.DrawString(ScoreScreenFont, "" + score, new Vector2(266, 420), Color.Red);
                    scoreexit.DrawMe(spriteBatch);
                    backButton.DrawMe(spriteBatch);

                    spriteBatch.End();
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
