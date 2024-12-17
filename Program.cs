using System;
using System.IO;

//test github this is test

namespace Pacman
{
    class Program
    {
        static void Main(string[] args)
        {
            // Création et lancement du jeu
             string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
             string xmlFile = Path.Combine("src", "data", "xml", "Init_Game.xml");
             string xsdFile = Path.Combine("src", "data", "xsd", "Init_Game.xsd");
            Console.WriteLine($"Chemin XML : {xmlFile}");
            Console.WriteLine($"Chemin XSD : {xsdFile}");


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