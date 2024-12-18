using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
namespace pacman
{
    internal class Player : Sprite
    {
        
        private int windowWidth;
        private int windowHeight;

        private Texture2D textureOuverte;
        private Texture2D textureFermee;
        private float rotation;

        private float animationTimer;
        private float PacmanAnimation= 0.07f; 
        private bool OpenMouth = true; 
        private bool Mouvement;
        
        private Map carte;

        public void SetCarte(Map carte)
        {
            this.carte = carte;
        }
        

        public Player(Texture2D textureOuverte, Texture2D textureFermee, Vector2 position, int size, int windowWidth, int windowHeight)
            : base(textureOuverte, position, size)
        {
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
            this.textureOuverte = textureOuverte;
            this.textureFermee = textureFermee;
            rotation = 0f;
        }

        public virtual void Update(GameTime gameTime)
        {
            Mouvement = false;
            KeyboardState state = Keyboard.GetState();

            Vector2 nextPosition = position;

            if (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.Q)) //deplacement de pacman 
            {
                nextPosition.X -= 5;
                rotation = MathHelper.Pi;
                Mouvement = true;
            }
            if (state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D))
            {
                nextPosition.X += 5;
                rotation = 0f;
                Mouvement = true;
            }
            if (state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.Z))
            {
                nextPosition.Y -= 5;
                rotation = -MathHelper.PiOver2;
                Mouvement = true;
            }
            if (state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.S))
            {
                nextPosition.Y += 5;
                rotation = MathHelper.PiOver2;
                Mouvement = true;
            }
            
            if (carte != null && carte.EstDeplacementValide(nextPosition, textureOuverte)) 
            {
                position = nextPosition; 
                carte.MangerPoint(position); 
            }
            
            position.X = MathHelper.Clamp(position.X, 0, windowWidth - textureOuverte.Width);
            position.Y = MathHelper.Clamp(position.Y, 0, windowHeight - textureOuverte.Height);
            rectangle.Location = position.ToPoint();

       
            if (Mouvement)
            {
                animationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (animationTimer >= PacmanAnimation)
                {
                    OpenMouth = !OpenMouth;

                    if (OpenMouth)
                    {
                        texture = textureOuverte; 
                    }
                    else
                    {
                        texture = textureFermee; 
                    }

                    animationTimer = 0f; 
                }
            }
            else
            {
                texture = textureOuverte; 
                OpenMouth = true;
                animationTimer = 0f; 
            }
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
           
            Vector2 origin = new Vector2(rectangle.Width / 2f, rectangle.Height / 2f);
            spriteBatch.Draw(texture, position + origin, null, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0f);
        }
    }
}
