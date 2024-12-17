using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Xml;
using BasicMonoGame;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;
namespace pacman
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SoundEffect coinSound;
        private const int windowWidth = 650;
        private const int windowHeight = 700;
        private Player pacman;
        private List<Creature> ghosts;
        private List<Point> points;
        private Map map;
        private int[,] grille;
        private int tailleCase;
        private Texture2D gameOverImage;
        private Texture2D winImage;
        private Texture2D DebutImage;
        private TimeSpan deathSoundDuration = TimeSpan.FromSeconds(4);
        private Timer deathSoundTimer;
        private SoundEffect menuSound; 
        private bool menuSoundPlayed = false; 
        
  
        public Game1()
        { 
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            
            _graphics.PreferredBackBufferWidth = windowWidth;
            _graphics.PreferredBackBufferHeight = windowHeight;

        
            _graphics.IsFullScreen = false;

           
            _graphics.SynchronizeWithVerticalRetrace = true;

         
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 60.0); 

            
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;

          
            _graphics.ApplyChanges();
        }


        public enum etatJeu
        {
            Menu,
            Playing,
            GameOver,
            WIN,
            EXIT
        }

        private etatJeu currentGameState;
        private SpriteFont menuFont;
        private int selectedOption = 0; 
        private int gameOverOption = 0; 
        private KeyboardState previousKeyboardState;

        protected override void Initialize()
        {
         
            points = new List<Point>();
            ghosts = new List<Creature>();

         
            currentGameState = etatJeu.Menu;

          

      
            base.Initialize();
        }


        protected override void LoadContent()
        {
        
    
            _spriteBatch = new SpriteBatch(GraphicsDevice);
       
            Texture2D textureMur = Content.Load<Texture2D>("image/wall");
            Texture2D textureOuverte = Content.Load<Texture2D>("image/pacman");
            Texture2D textureFermee = Content.Load<Texture2D>("image/lock-open");
            Texture2D texturePoint = Content.Load<Texture2D>("image/coin");
            Texture2D textureGhost = Content.Load<Texture2D>("image/creature");
            gameOverImage = Content.Load<Texture2D>("image/finish");
            winImage = Content.Load<Texture2D>("image/win");
            DebutImage = Content.Load<Texture2D>("image/image");
            menuSound = Content.Load<SoundEffect>("sound/menuSound");
           
            string xmlPath = "src/data/xml/Init_Game.xml";
            map = RecupCarte(xmlPath, textureMur);

          
            points = GenererPoints(texturePoint);

           
            (pacman, ghosts) = InitialiserJoueurs(textureOuverte, textureFermee, textureGhost);
            
       
            menuFont = Content.Load<SpriteFont>("menu");
        }

        public Map RecupCarte(string xmlPath, Texture2D textureMur)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            XmlNodeList lignes = xmlDoc.SelectNodes("//map/ligne");
            Console.WriteLine("map du jeu: \n");
            foreach (XmlNode ligne in lignes)
            {   
               
                Console.WriteLine(ligne.InnerText);
            }

            XmlNode root = xmlDoc.DocumentElement;
            XmlElement rootElement = root as XmlElement;

            XmlNodeList noeudsLignes = rootElement.GetElementsByTagName("ligne");

            int nombreDeLignes = noeudsLignes.Count;
            int nombreDeColonnes = noeudsLignes[0].InnerText.Trim().Split(' ').Length;

            grille = new int[nombreDeLignes, nombreDeColonnes];
            for (int i = 0; i < nombreDeLignes; i++)
            {
                string ligneTexte = noeudsLignes[i].InnerText.Trim();
                string[] valeurs = ligneTexte.Split(' ');

                for (int j = 0; j < valeurs.Length -1 ; j++)
                {
                    grille[i, j] = Convert.ToInt32(valeurs[j]);
                }
            }

            XmlNode tailleNode = rootElement.GetElementsByTagName("taille").Item(0);
            tailleCase = Convert.ToInt32(tailleNode.InnerText);

            return new Map(grille, textureMur, tailleCase);
        }

        private List<Point> GenererPoints(Texture2D texturePoint)
        {
            List<Point> points = new List<Point>();

            for (int i = 0; i < grille.GetLength(0); i++)
            {
                for (int j = 0; j < grille.GetLength(1); j++)
                {
                    if (grille[i, j] == 1)
                    {
                        float positionX = j * tailleCase + (tailleCase - 15) / 2f;
                        float positionY = i * tailleCase + (tailleCase - 15) / 2f;
                        points.Add(new Point(texturePoint, new Vector2(positionX, positionY), 15));
                    }
                }
            }

            return points;
        }
    
        private (Player, List<Creature>) InitialiserJoueurs(Texture2D textureOuverte, Texture2D textureFermee,
            Texture2D textureGhost)
        {
            Vector2 pacmanPosition = Vector2.Zero;
            List<Creature> ghostsList = new List<Creature>();
            int caseValide = 0;

            int[] ghostPositions = {15,30 ,45, 60 ,75};


            for (int i = 0; i < grille.GetLength(0); i++) 
            {
                for (int j = 0; j < grille.GetLength(1); j++) 
                {
                    switch (grille[i, j])
                    {
                        case 1:
                            caseValide++;
                            if (pacmanPosition == Vector2.Zero)
                            {
                                pacmanPosition = new Vector2(j * tailleCase, i * tailleCase);
                            }

                            bool trouve = false;
                            

                            // Vérification si une position de fantôme doit être ajoutée
                            for (int k = 0; k < ghostPositions.Length; k++)
                            {
                                switch (ghostPositions[k])
                                {
                                    case var pos when pos == caseValide:
                                        trouve = true;
                                        break;
                                }
                            }

                            if (trouve)
                            {
                                ghostsList.Add(new Creature(textureGhost, new Vector2(j * tailleCase, i * tailleCase),
                                    windowWidth, windowHeight, 49));
                            }
                            break;
                    }
                }
            }

          
            var pacman = new Player(textureOuverte, textureFermee, pacmanPosition, tailleCase, windowWidth,
                windowHeight);
            pacman.SetCarte(map);

            // Initialisation des fantômes
            for (int i = 0; i < ghostsList.Count; i++)
            {
                ghostsList[i].SetCarte(map);
            }

            return (pacman, ghostsList);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (currentGameState)
            {
                case etatJeu.Menu:
                    UpdateMenu();
                    break;

                case etatJeu.Playing:
                    UpdatePlaying(gameTime);
                    break;

                case etatJeu.GameOver:
                    
                   
                    
                    UpdateGameOver();
                    break;
                case etatJeu.WIN:
                    UpdateGameOver();
                    break;

                case etatJeu.EXIT:
                    Exit();
                    break;
            }

            base.Update(gameTime);
        }

        private void UpdatePlaying(GameTime gameTime)
        {
            coinSound = Content.Load<SoundEffect>("sound/pacman_eatCoin");
            pacman.Update(gameTime);

           
            for (int i = 0; i < ghosts.Count; i++)
            {
                ghosts[i].Update(gameTime);
            }

            
            for (int i = points.Count - 1; i >= 0; i--)
            {
                switch (true)
                {
                    case var _ when pacman.rectangle.Intersects(points[i].rectangle):
                        points.RemoveAt(i);
                        coinSound.Play();
                        break;
                }
            }

            
            Rectangle pacmanRectangle = new Rectangle((int)pacman.position.X, (int)pacman.position.Y, 35,35);
            for (int i = 0; i < ghosts.Count; i++)
            {
                Rectangle ghostRectangle = new Rectangle((int)ghosts[i].position.X, (int)ghosts[i].position.Y, 35, 35);
                switch (true)
                {
                    case var _ when pacmanRectangle.Intersects(ghostRectangle):
                        
                        currentGameState = etatJeu.GameOver;
                       
                        return; 
                }
            }

      
            switch (points.Count)
            {
                case 0:
                    currentGameState = etatJeu.WIN;
                    break;
            }
        }


        private void UpdateMenu()
        {
            KeyboardState state = Keyboard.GetState();
            if (!menuSoundPlayed)
            {
                menuSound.Play(); // Joue le son du menu
                menuSoundPlayed = true; // Empêche de rejouer en boucle
            }
          
            switch (true)
            {
                case var _ when state.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up):
                    selectedOption = (selectedOption +1) % 2;
                    break;

                case var _ when state.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down):
                    selectedOption = (selectedOption + 1) % 2;
                    break;
            }

           
            switch (true)
            {
                case var _ when state.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter):
                    switch (selectedOption)
                    {
                        case 0:
                           
                                currentGameState = etatJeu.Playing;
                                menuSound = Content.Load<SoundEffect>("sound/pacman_eatCoin");
                            

                            break;
                        case 1: 
                            currentGameState = etatJeu.EXIT;
                            break;
                    }
                    break;
            }

        
            previousKeyboardState = state;
        }


        private void UpdateGameOver()
        {
        
            KeyboardState state = Keyboard.GetState();
           
            


         
            if (state.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up))
            {
                gameOverOption = (gameOverOption +1) % 2;
            }
            else if (state.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down))
            {
                gameOverOption = (gameOverOption + 1) % 2; 
            }

           
            if (state.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter))
            {
                switch (gameOverOption)
                {
                    case 0:
                        
                        Initialize();
                        LoadContent();
                        currentGameState = etatJeu.Playing;
                        break;

                    case 1: 
                        currentGameState = etatJeu.EXIT;
                        break;

                    default:
                      
                        break;
                }
            }

            
            previousKeyboardState = state;
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            switch (currentGameState)
            {
                case etatJeu.Menu:
                    DrawMenu(_spriteBatch);
                    break;

                case etatJeu.Playing:
                    DrawPlaying(_spriteBatch);
                    break;

                case etatJeu.GameOver:
                    DrawGameOver(_spriteBatch);
                    break;
                case etatJeu.WIN:
                    DrawWIN(_spriteBatch);
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawMenu(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(DebutImage, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);


            int baseY = 140;
            int spacing = 50; 

            
            Color selectedColor = Color.Cyan; 
            Color defaultColor = Color.White; 

          
            spriteBatch.DrawString(menuFont, "PLAY", new Vector2(250, baseY),
                selectedOption == 0 ? selectedColor : defaultColor);

           
            spriteBatch.DrawString(menuFont, "EXIT", new Vector2(250, baseY + spacing),
                selectedOption == 1 ? selectedColor : defaultColor); 

           
            if (selectedOption == 0)
            {
                spriteBatch.DrawString(menuFont, "PLAY", new Vector2(250, baseY - 2),
                    selectedColor); 
                spriteBatch.DrawString(menuFont, "PLAY", new Vector2(250, baseY - 2), selectedColor, 0f,
                    new Vector2(0, 0), 1.1f, SpriteEffects.None, 0f);
            }
            else if (selectedOption == 1)
            {
                spriteBatch.DrawString(menuFont, "EXIT", new Vector2(250, baseY + spacing - 5),
                    selectedColor); 
                spriteBatch.DrawString(menuFont, "EXIT", new Vector2(250, baseY + spacing - 5), selectedColor, 0f,
                    new Vector2(0, 0), 1.1f, SpriteEffects.None, 0f);
            }
        }

        private void DrawGameOver(SpriteBatch spriteBatch)
        {
            string option1 = "Again";
            string option2 = "Exit";

            
            spriteBatch.Draw(gameOverImage, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);

            
            int baseY = 140; 
            int spacing = 60; 

        
            Color selectedColor = Color.Cyan; 
            Color defaultColor = Color.White; 

          
            spriteBatch.DrawString(menuFont, option1, new Vector2(250, baseY),
                gameOverOption == 0 ? selectedColor : defaultColor);

          
            spriteBatch.DrawString(menuFont, option2, new Vector2(250, baseY + spacing),
                gameOverOption == 1 ? selectedColor : defaultColor);

         
            if (gameOverOption == 0)
            {
                spriteBatch.DrawString(menuFont, option1, new Vector2(250, baseY - 5),
                    selectedColor); 
                
                spriteBatch.DrawString(menuFont, option1, new Vector2(250, baseY - 5), selectedColor, 0f,
                    new Vector2(0, 0), 1.1f, SpriteEffects.None, 0f);
            }
            else if (gameOverOption == 1)
            {
                spriteBatch.DrawString(menuFont, option2, new Vector2(250, baseY + spacing - 5),
                    selectedColor); 
               
                spriteBatch.DrawString(menuFont, option2, new Vector2(250, baseY + spacing - 5), selectedColor, 0f,
                    new Vector2(0, 0), 1.1f, SpriteEffects.None, 0f);
            }
        }


        private void DrawPlaying(SpriteBatch spriteBatch)
        {
           
            map.Draw(spriteBatch);

         
            pacman.Draw(spriteBatch);

           
            for (int i = 0; i < points.Count; i++)
            {
                points[i].Draw(spriteBatch);
            }

          
            for (int i = 0; i < ghosts.Count; i++)
            {
                ghosts[i].Draw(spriteBatch);
            }
        }



        private void DrawWIN(SpriteBatch spriteBatch)
        {
            string option1 = "Again";
            string option2 = "Exit";

        
            spriteBatch.Draw(winImage, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);

         
            int baseY = 140; 
            int spacing = 50; 

     
            Color selectedColor = Color.Blue; 
            Color defaultColor = Color.Black; 

           
            Color rejouerColor =
                (gameOverOption == 0)
                    ? selectedColor
                    : defaultColor; 
            spriteBatch.DrawString(menuFont, option1, new Vector2(250, baseY), rejouerColor); 

           
            Color EXITterColor =(gameOverOption == 1)
                    ? selectedColor
                    : defaultColor; // 
            spriteBatch.DrawString(menuFont, option2, new Vector2(250, baseY + spacing), EXITterColor); 
        }
    }

} 