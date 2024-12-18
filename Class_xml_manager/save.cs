using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace GameSaveLoad
{
    // Classe principale correspondant à <Game1>
    [XmlRoot("Game1")]
    public class Game
    {
        [XmlElement("pacman")]
        public Pacman Pacman { get; set; }

        [XmlElement("player_name")]
        public PlayerName PlayerName { get; set; }

        [XmlElement("player_scores")]
        public PlayerScores PlayerScores { get; set; }

        [XmlElement("carte")]
        public Carte Carte { get; set; }

        [XmlArray("ghosts")]
        [XmlArrayItem("ghost")]
        public List<Ghost> Ghosts { get; set; } = new List<Ghost>();

        [XmlArray("points")]
        [XmlArrayItem("point")]
        public List<Point> Points { get; set; } = new List<Point>();

        [XmlElement("grille")]
        public Grille Grille { get; set; }

        [XmlElement("taille")]
        public int Taille { get; set; }

        [XmlElement("state")]
        public string State { get; set; }

        [XmlElement("SelectOption")]
        public int SelectOption { get; set; }

        [XmlElement("gameOverOption")]
        public int GameOverOption { get; set; }
    }

    // Classe pour Pacman
    public class Pacman
    {
        [XmlElement("rotation")]
        public float Rotation { get; set; }

        [XmlElement("animationInterval")]
        public float AnimationInterval { get; set; }
    }

    // Classe pour PlayerName
    public class PlayerName
    {
        [XmlElement("first_name")]
        public string FirstName { get; set; }

        [XmlElement("last_name")]
        public string LastName { get; set; }

        [XmlElement("email")]
        public string Email { get; set; }
    }

    // Classe pour les scores
    public class PlayerScores
    {
        [XmlElement("tot_pellets_eaten")]
        public int TotalPelletsEaten { get; set; }

        [XmlElement("tot_games_played")]
        public int TotalGamesPlayed { get; set; }

        [XmlElement("tot_games_won")]
        public int TotalGamesWon { get; set; }

        [XmlElement("tot_games_lost")]
        public int TotalGamesLost { get; set; }
    }

    // Classe pour la carte
    public class Carte
    {
        [XmlArray("map")]
        [XmlArrayItem("ligne")]
        public List<string> Map { get; set; } = new List<string>();

        [XmlElement("taille")]
        public int Taille { get; set; }
    }

    // Classe pour les fantômes
    public class Ghost
    {
        [XmlElement("direction")]
        public Direction Direction { get; set; }

        [XmlElement("speed")]
        public float Speed { get; set; }
    }

    // Classe pour un point
    public class Point
    {
        [XmlElement("position")]
        public Position Position { get; set; }

        [XmlElement("size")]
        public int Size { get; set; }
    }

    // Classe pour une grille
    public class Grille
    {
        [XmlArray("ligne")]
        [XmlArrayItem("value")]
        public List<int> Ligne { get; set; } = new List<int>();
    }

    // Classe pour un vecteur 2D
    public class Direction
    {
        [XmlElement("x")]
        public int X { get; set; }

        [XmlElement("y")]
        public int Y { get; set; }
    }

    // Classe pour une position
    public class Position
    {
        [XmlElement("x")]
        public int X { get; set; }

        [XmlElement("y")]
        public int Y { get; set; }
    }

    public static class SaveManager
    {
        // Méthode pour sauvegarder le jeu
        public static void SaveGame(string filePath, Game game)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Game));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, game);
            }
            Console.WriteLine("Jeu sauvegardé avec succès !");
        }

        // Méthode pour charger le jeu
        public static Game LoadGame(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Game));
            using (StreamReader reader = new StreamReader(filePath))
            {
                return (Game)serializer.Deserialize(reader);
            }
        }
    }
}
