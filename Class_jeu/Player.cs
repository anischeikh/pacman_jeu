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
        private Texture2D OpenTxt;
        private Texture2D ClosedTxt;
        private float rotation;
        private float Calculateur;
        private float PacmanAnimation= 0.07f; 
        private bool OpenMouth = true; 
        private bool Mouvement;
        
        private Map carte;

        public void SetCarte(Map carte)
        {
            this.carte = carte;
        }
        public Player(Texture2D OpenTxt, Texture2D ClosedTxt, Vector2 position, int size, int windowWidth, int windowHeight)
            : base(OpenTxt, position, size)
        {
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
            this.OpenTxt = OpenTxt;
            this.ClosedTxt = ClosedTxt;
            rotation = 0f;
        }

       public virtual void Update(GameTime gameTime)
{
    Mouvement = false;
    KeyboardState state = Keyboard.GetState();

    Vector2 nextPosition = position;
    Keys? keyPressed = null;

    // Identifier la touche pressée
    if (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.Q))
        keyPressed = Keys.Left;
    else if (state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D))
        keyPressed = Keys.Right;
    else if (state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.Z))
        keyPressed = Keys.Up;
    else if (state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.S))
        keyPressed = Keys.Down;

    // Gestion des déplacements avec un switch
    switch (keyPressed)
    {
        case Keys.Left:
            nextPosition.X -= 4;
            rotation = MathHelper.Pi;
            Mouvement = true;
            break;

        case Keys.Right:
            nextPosition.X += 4;
            rotation = 0f;
            Mouvement = true;
            break;

        case Keys.Up:
            nextPosition.Y -= 4;
            rotation = -MathHelper.PiOver2;
            Mouvement = true;
            break;

        case Keys.Down:
            nextPosition.Y += 4;
            rotation = MathHelper.PiOver2;
            Mouvement = true;
            break;
    }

   
    if (carte != null && carte.EstDeplacementValide(nextPosition, OpenTxt))
    {
        position = nextPosition;
        carte.MangerPoint(position);
    }

  
    position.X = MathHelper.Clamp(position.X, 0, windowWidth - OpenTxt.Width);
    position.Y = MathHelper.Clamp(position.Y, 0, windowHeight - OpenTxt.Height);
    rectangle.Location = position.ToPoint();

    if (Mouvement)
    {
        Calculateur += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (Calculateur >= PacmanAnimation)
        {
            OpenMouth = !OpenMouth;
            texture = OpenMouth ? OpenTxt : ClosedTxt;
            Calculateur = 0f;
        }
    }
    else
    {
        texture = OpenTxt;
        OpenMouth = true;
        Calculateur = 0f;
    }
}

        
        public void Draw(SpriteBatch spriteBatch)
        {
           
            Vector2 origin = new Vector2(rectangle.Width / 2f, rectangle.Height / 2f);
            spriteBatch.Draw(texture, position + origin, null, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0f);
        }
    }
}
