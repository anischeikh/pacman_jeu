using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using pacman;
using System;
using System.Xml;
namespace BasicMonoGame
{
    public class Creature : Sprite
    {
        private int windowWidth;
        private int windowHeight;
        private Map map;
        private Vector2 ghostDirection; // Variable globale pour stocker la direction
        private float ghostSpeed;     
        string xmlPath = "src/data/xml/Init_Game.xml";
        //private Vector2 direction = new Vector2(1, 0); // Par défaut, le fantôme se déplace vers la droite
        public Creature(Texture2D texture, Vector2 position, int windowWidth, int windowHeight, int size = 30)
            : base(texture, position, size)
        {
            this.texture = texture;
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;

            rectangle = new Rectangle((int)position.X, (int)position.Y, size, size);
            recupGhoste(xmlPath);

        }
       
       


        public void recupGhoste(string xmlPath)
        {
            XmlReader reader = XmlReader.Create(xmlPath); // Initialiser le XmlReader

            while (reader.Read())
            {
                if (reader.IsStartElement() && reader.Name == "ghosts")
                {
                    Vector2 direction = Vector2.Zero;
                    float speed = 0f;

                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            switch (reader.Name)
                            {
                                case "x":
                                    direction.X = reader.ReadElementContentAsInt();
                                    break;
                                case "y":
                                    direction.Y = reader.ReadElementContentAsInt();
                                    break;
                                case "speed":
                                    speed = reader.ReadElementContentAsFloat();
                                    break;
                            }
                        }

                        if (reader.Name == "ghosts" && reader.NodeType == XmlNodeType.EndElement)
                        {
                            // Affecter les variables globales
                            ghostDirection = direction;
                            ghostSpeed = speed;
                            Console.WriteLine($"Direction: {ghostDirection}, Speed: {ghostSpeed}");

                            // Sortir du parsing de ce fantôme
                            break;
                        }
                    }
                }
            }

            // Fermer manuellement le XmlReader
            reader.Close();
        }

        public void SetCarte(Map map)
        {
            this.map = map;
        }

        public virtual void Update(GameTime gameTime)
        {
            // Calculer la prochaine position
            Vector2 nextPosition = position + ghostDirection * ghostSpeed;

            // Vérifier si le mouvement est valide
            if (map != null && map.EstDeplacementValide(nextPosition, texture))
            {
                position = nextPosition;
               
            }
            else
            {
                // Changer de direction si le mouvement est bloqué
                ChangerDirection();
            }

            // Contraindre la position à la fenêtre
            position.X = MathHelper.Clamp(position.X, 0, windowWidth - texture.Width);
            position.Y = MathHelper.Clamp(position.Y, 0, windowHeight - texture.Height);
        }

        private void ChangerDirection()
        {
            
            Vector2[] directionsPossibles = {
                new Vector2(1, 0),  // Droite
                new Vector2(-1, 0), // Gauche
                new Vector2(0, 1),  // Bas
                new Vector2(0, -1)  // Haut
            };

           
            foreach (Vector2 nouvelleDirection in directionsPossibles)
            {
                Vector2 tentativePosition = position + nouvelleDirection * ghostSpeed;
                if (map != null && map.EstDeplacementValide(tentativePosition, texture))
                {
                    ghostDirection = nouvelleDirection;
                    return;
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
