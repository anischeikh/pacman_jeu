using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;



namespace Pacman
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Chemin relatif au projet (à partir du répertoire bin)
            string xmlFile = Path.Combine("src", "data","xml","Init_Game.xml");
            string xsdFile = Path.Combine("src", "data","xsd","Init_Game.xsd");

            // Obtenir le répertoire courant du projet pour le chargement de fichiers
            string currentDirectory = Directory.GetCurrentDirectory();

            // Créer les chemins absolus
            xmlFile = Path.Combine(currentDirectory, xmlFile);
            xsdFile = Path.Combine(currentDirectory, xsdFile);

            bool isValid = XmlValidator.ValidateXml(xmlFile, xsdFile);

            if (isValid)
            {
                Console.WriteLine("Le fichier XML est valide.");
            }
            else
            {
                Console.WriteLine("Le fichier XML n'est pas valide.");
            }

            using var game = new pacman.Game1();
            game.Run();
        }
    }
}
