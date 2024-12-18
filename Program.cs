using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Diagnostics;

namespace Pacman
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Chemin relatif au projet (à partir du répertoire bin)
            string xmlFile = Path.Combine("src", "data", "xml", "Init_Game.xml");
            string xslFile = Path.Combine("src", "data", "xslt", "Init_Game.xslt"); // Assurez-vous que vous avez le fichier XSLT dans le bon emplacement

            // Obtenir le répertoire courant du projet pour le chargement de fichiers
            string currentDirectory = Directory.GetCurrentDirectory();

            // Créer les chemins absolus
            xmlFile = Path.Combine(currentDirectory, xmlFile);
            xslFile = Path.Combine(currentDirectory, xslFile);

            // Valider le fichier XML avec son XSD
            string xsdFile = Path.Combine("src", "data", "xsd", "Init_Game.xsd");
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

            // Charger le fichier XML
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFile);

            // Charger le fichier XSLT
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(xslFile);

            // Spécifier le fichier de sortie HTML
            string htmlOutputFile = Path.Combine(currentDirectory, "Init_Game.html");

            // Appliquer la transformation XSLT et générer le HTML
            using (XmlWriter writer = XmlWriter.Create(htmlOutputFile, new XmlWriterSettings { Indent = true }))
            {
                xslt.Transform(xmlDoc, writer);
            }

            // Ouvrir le fichier HTML généré dans le navigateur par défaut
            Process.Start(new ProcessStartInfo(htmlOutputFile) { UseShellExecute = true });

            // Optionnel : démarrer le jeu
            using (var game = new pacman.Game1())
            {
                game.Run();
            }
        }
    }
}
