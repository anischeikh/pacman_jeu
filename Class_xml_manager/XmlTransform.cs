using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using pacman;

namespace Pacman.Class_xml_manager
{
    public class XmlTransform
    {
        /// <summary>
        /// Transforme un fichier XML en un fichier HTML en utilisant une feuille de style XSLT.
        /// </summary>
        
        public void TransformXmlToHtml(string xmlFilePath, string xsltFilePath, string htmlOutputFilePath)
        {
            try
            {
                // Vérifier que les fichiers XML et XSLT existent
                if (!File.Exists(xmlFilePath))
                {
                    throw new FileNotFoundException($"Le fichier XML spécifié n'existe pas : {xmlFilePath}");
                }

                if (!File.Exists(xsltFilePath))
                {
                    throw new FileNotFoundException($"Le fichier XSLT spécifié n'existe pas : {xsltFilePath}");
                }

                // Charger le fichier XML
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);

                // Charger le fichier XSLT
                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load(xsltFilePath);

                // Créer le fichier HTML en sortie
                using (XmlWriter writer = XmlWriter.Create(htmlOutputFilePath, new XmlWriterSettings { Indent = true }))
                {
                    xslt.Transform(xmlDoc, writer);
                }

                Console.WriteLine($"HTML généré avec succès : {htmlOutputFilePath}");

                // Ouvrir le fichier HTML généré dans le navigateur par défaut
                Process.Start(new ProcessStartInfo(htmlOutputFilePath) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la transformation XML -> HTML : {ex.Message}");
            }
        }
    }
}