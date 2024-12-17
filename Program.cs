using System;
//test github this is test

namespace Pacman
{
    class Program
    {
        static void Main(string[] args)
        {
            // Création et lancement du jeu
            string xmlFile = "src/data/xml/Init_Game.xml";
            string xsdFile = "src/data/xsd/Init_Game.xsd";

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