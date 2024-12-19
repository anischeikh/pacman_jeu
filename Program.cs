using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Pacman
{
    // Classe représentant une partie
   

    // Classe représentant la collection de parties
   
    class Program
    {
        public static void Main(string[] args)
        {
            // Chemin relatif au projet (à partir du répertoire bin)
            string xmlFile = Path.Combine("..","..","..","Content","src", "data", "xml", "Init_Game.xml");
            string xslFile = Path.Combine("..","..","..","Content","src", "data", "xslt", "Init_Game.xslt"); // Assurez-vous que vous avez le fichier XSLT dans le bon emplacement

            // Obtenir le répertoire courant du projet pour le chargement de fichiers
            string currentDirectory = Directory.GetCurrentDirectory();

            // Créer les chemins absolus
            xmlFile = Path.Combine(currentDirectory, xmlFile);
            xslFile = Path.Combine(currentDirectory, xslFile);

           
            string xsdFile = Path.Combine("..","..","..","Content","src", "data", "xsd", "Init_Game.xsd");
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
            
            using (var game = new pacman.Class_jeu.Game1())
            {
                game.Run();
                
            }
        }

      
        
    }
}
