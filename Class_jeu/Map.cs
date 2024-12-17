namespace pacman;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
public class Map
{
    private int[,] grille;
    private Texture2D textureMur;
    private int tailleCase; 

   
    public Map(int[,] grille, Texture2D textureMur, int tailleCase)
    {
        this.grille = grille;
        this.textureMur = textureMur;
        this.tailleCase = tailleCase; 
    }
   

    public bool EstDeplacementValide(Vector2 position, Texture2D texture)
    {
     
        int colonneGauche = (int)(position.X / tailleCase);
        int ligneHaut = (int)(position.Y / tailleCase);
        int colonneDroite = (int)((position.X + texture.Width) / tailleCase); 
        int ligneBas = (int)((position.Y + texture.Height) / tailleCase);  

       
        if (colonneGauche < 0 || colonneDroite >= grille.GetLength(1) || ligneHaut < 0 || ligneBas >= grille.GetLength(0))
        {
            return false; 
        }

        
        for (int i = ligneHaut; i <= ligneBas; i++)
        {
            for (int j = colonneGauche; j <= colonneDroite; j++)
            {
                if (grille[i, j] == 0) 
                {
                    return false;
                }
            }
        }

        return true; 
    }


    public void Draw(SpriteBatch spriteBatch)
    {
       
        for (int i = 0; i < grille.GetLength(0); i++)
        {
            for (int j = 0; j < grille.GetLength(1); j++)
            {
                Vector2 position = new Vector2(j * tailleCase, i * tailleCase);

                if (grille[i, j] == 0) 
                {
                    spriteBatch.Draw(textureMur, new Rectangle((int)position.X, (int)position.Y, tailleCase, tailleCase), Color.White);
                }
                
            }
        }
    }


    
    public void MangerPoint(Vector2 position)
    {
        int colonne = (int)position.X / tailleCase;
        int ligne = (int)position.Y / tailleCase;

        if (grille[ligne, colonne] == 2)  
        {
            grille[ligne, colonne] = 1;  
        }
    }
}
